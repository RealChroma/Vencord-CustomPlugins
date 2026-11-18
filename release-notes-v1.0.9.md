# Custom Vencord v1.0.9

- Updates Vencord to **1.15.18** at upstream commit `e75ac9ace41f6212cfaa2d5f3670830be392da4e`. This includes fixes for web screen sharing, toasts, typing indicators, and chat mentions.
- Bundles **OrionQuests 4.11.5** at `b27f0802d1c62e50db97212dae3b3641fd55a91e`, the current commit on Orion’s default branch.
- Includes NitroSniper, runtime JavaScript plugin loading, and the custom plugin folder shortcut.
- Custom Vencord Manager **v0.1.8** users can select **Check for updates**.

## Verification

- All 143 Orion regression tests passed. TypeScript, Vencord source lint, and the standalone Windows build passed.
- Both ZIPs passed validation. Install, verification, and uninstall passed with isolated Discord Stable, PTB, and Canary fixtures. Live Discord account behavior remains untested.

The source ZIP corresponds to customized Vencord commit `cd216df17e1b20796f30f0bd34222a6cabe7723e`.
