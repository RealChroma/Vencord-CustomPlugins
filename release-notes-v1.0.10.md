# Custom Vencord v1.0.10

- Updates Vencord to **1.15.19** at upstream commit `d086992fb674299a2fa05ac6432edc3a777389f3`. The update includes Vencord tray controls and fixes for screen sharing, voice invite clicks, and FullSearchContext.
- Bundles **OrionQuests 4.11.5** at `b27f0802d1c62e50db97212dae3b3641fd55a91e`, the current commit on Orion’s default branch.
- Includes NitroSniper, runtime JavaScript plugin loading, and the custom plugin folder shortcut.
- Custom Vencord Manager **v0.1.8** users can select **Check for updates**.

## Verification

- All 143 Orion regression tests passed. TypeScript, Vencord source lint, and the standalone Windows build passed.
- Both ZIPs passed validation. Install, verification, and uninstall passed with isolated Discord Stable, PTB, and Canary fixtures. Live Discord account behavior remains untested.

The source ZIP corresponds to customized Vencord commit `dd49c54f27b45cca168d533706b89cde336f004d`.
