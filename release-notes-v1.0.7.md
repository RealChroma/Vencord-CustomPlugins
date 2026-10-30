# Custom Vencord v1.0.7

- Updates Vencord from **1.15.11 to 1.15.14**, upstream `644434941b515a2626a2161e219653cb36e723e7`.
- Syncs OrionQuests to upstream `7b21ac365dd38686d25933dc84d46e2bdb2c2b2a`. Its upstream version remains **4.11.5**.
- Adds Orion's explanation when multiple enrolled quests share the same game and progress together; includes five new regression tests.
- Preserves the runtime custom-plugin loader, desktop/native hooks, Orion integration, and NitroSniper redemption/webhook behavior.
- Adapts the custom-plugin folder button and NitroSniper settings button to Vencord's current UI components.
- Keeps **Custom Vencord Manager v0.1.8** unchanged. Existing manager users can use **Check for updates** to obtain this distribution.

## Validation

- **139 Orion regression tests passed.**
- TypeScript, ESLint, style lint, plugin metadata generation, and the standalone updater-disabled desktop build passed.
- Exact release ZIP validated, including required manager payload files and bundled Orion/NitroSniper/loader markers.
- Source ZIP includes the modified Vencord source, upstream provenance, regression tests, vendored gifenc, and licenses; excludes dependencies/build outputs/recovery files.
- Isolated package extraction, injection, verification, cross-client rejection, uninstall restoration, and plugin preservation passed for Stable, PTB, and Canary using the manager's patch services and synthetic Discord files.
- No real Discord installation or account was modified during validation. Live logged-in Discord behavior has not been tested.

Modified Vencord source commit: `b57d883d606d18ad43440320b8343d2dbcaff8df`.

## Downloads

- `VencordCustomManager-v0.1.8.exe`: recommended installer/updater, same verified manager executable as v1.0.6.
- `Vencord-CustomPlugins-release.zip`: manual-install distribution.
- `Vencord-CustomPlugins-source.zip`: complete modified Vencord source corresponding to the build (use this instead of GitHub's automatic release-repository source archive).
- `SHA256SUMS.txt`: SHA-256 checksums.

Client modifications, automated quest completion, and automated gift redemption can violate Discord's Terms of Service and put an account at risk.
