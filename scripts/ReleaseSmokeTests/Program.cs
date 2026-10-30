global using Environment = ReleaseSmoke.IsolatedEnvironment;

using System.IO.Compression;
using System.Security.Cryptography;
using VencordCustomManager;

namespace ReleaseSmoke;

public static class IsolatedEnvironment
{
    public enum SpecialFolder { LocalApplicationData, ApplicationData }
    public static string Root { get; set; } = string.Empty;

    public static string GetFolderPath(SpecialFolder folder)
    {
        if (string.IsNullOrWhiteSpace(Root)) throw new InvalidOperationException("Isolation root not set.");
        return Path.Combine(Root, folder.ToString());
    }
}

internal static class Program
{
    private static void Require(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
    }

    public static void Main(string[] args)
    {
        Require(args.Length == 2, "Usage: ReleaseSmokeTests <release.zip> <new-isolation-directory>");
        var zip = Path.GetFullPath(args[0]);
        var root = Path.GetFullPath(args[1]);
        Require(!Directory.Exists(root), "Isolation directory must not already exist.");
        IsolatedEnvironment.Root = root;
        ManagerPaths.EnsureCreated();
        var plugins = Path.Combine(ManagerPaths.PluginsDirectory, "preserve.js");
        File.WriteAllText(plugins, "plugin-preservation-sentinel");
        var variants = new[] { ("stable", "Discord"), ("ptb", "DiscordPTB"), ("canary", "DiscordCanary") };
        var resources = new Dictionary<string, string>();
        var originals = new Dictionary<string, byte[]>();
        foreach (var (branch, folder) in variants)
        {
            var directory = Path.Combine(IsolatedEnvironment.GetFolderPath(IsolatedEnvironment.SpecialFolder.LocalApplicationData),
                folder, "app-1.0.0", "resources");
            Directory.CreateDirectory(directory);
            resources[branch] = directory;
            originals[branch] = System.Text.Encoding.UTF8.GetBytes($"original-asar-{branch}");
            File.WriteAllBytes(Path.Combine(directory, "app.asar"), originals[branch]);
        }

        var patcher = new DiscordPatchService();
        foreach (var (branch, _) in variants)
        {
            var payload = ManagerPaths.GetInstallDirectory(branch);
            ZipFile.ExtractToDirectory(zip, payload);
            var payloadHashes = Directory.GetFiles(payload, "*", SearchOption.AllDirectories)
                .ToDictionary(file => file, file => Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(file))));
            patcher.EnsureManagedPatchAtResources(branch, resources[branch]);
            var probe = patcher.VerifyManagedPatchAtResources(branch, resources[branch]);
            Require(probe.IsBranchScopedManagedPatch, $"{branch}: branch-scoped injection not verified.");
            Require(File.ReadAllBytes(probe.BackupAsarPath).SequenceEqual(originals[branch]), $"{branch}: original backup changed.");
            foreach (var (other, _) in variants.Where(v => v.Item1 != branch))
            {
                Require(File.ReadAllBytes(Path.Combine(resources[other], "app.asar")).SequenceEqual(originals[other]),
                    $"{branch}: modified another client's app.asar.");
                Require(!File.Exists(Path.Combine(resources[other], "_app.asar")), $"{branch}: patched another client.");
            }
            foreach (var pair in payloadHashes)
                Require(Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(pair.Key))) == pair.Value,
                    $"{branch}: payload changed during injection.");
            var blocked = false;
            try
            {
                var other = variants.First(v => v.Item1 != branch).Item1;
                patcher.EnsureManagedPatchAtResources(branch, resources[other]);
            }
            catch (InvalidOperationException) { blocked = true; }
            Require(blocked, $"{branch}: cross-client path accepted.");
            patcher.EnsureUnpatchedAtResources(branch, resources[branch]);
            Require(File.ReadAllBytes(Path.Combine(resources[branch], "app.asar")).SequenceEqual(originals[branch]),
                $"{branch}: uninstall did not restore the original exactly.");
            Require(!File.Exists(Path.Combine(resources[branch], "_app.asar")), $"{branch}: uninstall left a backup.");
            Require(File.ReadAllText(plugins) == "plugin-preservation-sentinel", "Runtime plugins were changed.");
            Console.WriteLine($"{branch}: package extraction / injection / verify / isolated uninstall PASS");
        }
        Console.WriteLine("PACKAGE_SMOKE_TEST=PASS (synthetic Discord files; no real installation or account used)");
    }
}
