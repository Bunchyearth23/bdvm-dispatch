# BDVM - Dispatch

Dispatcher web module and optional Remote Dispatch integration.

## Dependency status

This module is not a standalone web server. It requires `BDVM.Common` and a compatible `BDVM.Web` module host. The currently usable in-game HTTP transport and browser frontend are provided by the [BDVM Remote Dispatch fork](https://github.com/Bunchyearth23/dv-remote-dispatch), branch `bdvm-integration`.

Remote Dispatch and its DLL are not bundled here. Without that fork, the Dispatch contracts can still be built and registered, but the current browser map and Remote Dispatch routes are unavailable. A future standalone `BDVM.Web` host may replace this transport dependency without changing the Dispatch business capability.

## Upstream and provenance

BDVM fork: https://github.com/Bunchyearth23/dv-remote-dispatch, branch `bdvm-integration`. Upstream: https://github.com/mspielberg/dv-remote-dispatch at commit `0e032da48550e09e405b4b164f31136f2e925ebf` (MIT). Any copied code must retain its copyright and MIT notice; the current bridge is maintained as an integration boundary.

## License

BDVM code is licensed under the Apache License, Version 2.0. See `LICENSE`. Any incorporated MIT-licensed upstream code remains subject to its original notice.
