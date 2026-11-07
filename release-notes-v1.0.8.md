# Custom Vencord v1.0.8

- Updates Vencord to **1.15.16**, upstream `383409e6a6ec9008ee8a7496500f1e75f17824d1`, including fixes for Discord's new rspack module loader.
- Syncs OrionQuests to upstream `b27f0802d1c62e50db97212dae3b3641fd55a91e`. Upstream still reports **4.11.5**.
- Integrates the shared quest-expiry helper and userscript/plugin parity tests. The userscript fixture is test-only, not bundled or executed in Discord.
- Preserves runtime custom-plugin loading, the custom folder button, native integrations, and NitroSniper behavior.
- Keeps **Custom Vencord Manager v0.1.8** unchanged. Existing users can select **Check for updates** in the manager.

## Validation

- **143 Orion regression tests passed**, including expiry/parity coverage.
- TypeScript, ESLint, style lint, and standalone updater-disabled desktop build passed.
- Exact release/source ZIPs validated; source contains custom modifications, tests, provenance, vendored gifenc and licenses, without installed dependencies, build outputs or recovery files.
- Isolated extraction, injection, verification, cross-client rejection, uninstall restoration and plugin preservation passed for Stable/PTB/Canary using synthetic Discord files and the manager's patch services.
- Real Discord installations and accounts were not modified or used for testing. Live logged-in behavior remains untested.

Modified Vencord source commit: `6542e5c002cf06c68edc0408b6ae07ccbe3a86a1`.

## Downloads

- `VencordCustomManager-v0.1.8.exe`: recommended installer/updater; unchanged verified executable.
- `Vencord-CustomPlugins-release.zip`: manual-install distribution.
- `Vencord-CustomPlugins-source.zip`: modified Vencord source corresponding to this build. Use this instead of GitHub's automatic release-repository source archive.
- `SHA256SUMS.txt`: SHA-256 checksums.

Client modifications, automated quests and gift redemption may violate Discord's Terms of Service and put accounts at risk.
