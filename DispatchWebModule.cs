using BDVM.Common;

namespace BDVM.Dispatch;

public sealed class DispatchWebModule : IBdvmWebModule
{
    public BdvmWebModuleManifest Manifest { get; } = new BdvmWebModuleManifest
    {
        Id = "BDVM.Dispatch",
        DisplayName = "BDVM - Dispatch",
        ModuleVersion = "0.1.0",
        RequiredWebApi = new BdvmApiRange(new BdvmApiVersion(1, 0), new BdvmApiVersion(1, 0)),
        RouteNamespace = "/api/modules/bdvm.dispatch",
        AssetNamespace = "modules/bdvm.dispatch",
        Capabilities = new[] { "bdvm.dispatch.network-view.v1" },
        Permissions = new[] { "dispatch.read", "dispatch.control" }
    };

    public void Register(IBdvmWebRegistrar registrar)
    {
        registrar.AddNavigation(new BdvmWebNavigationItem { Id = "bdvm.dispatch.network", Label = "Dispatch", Path = "/dispatch", Order = 100 });
        registrar.AddRoute(new BdvmWebRoute { Method = "GET", Path = "/api/modules/bdvm.dispatch/snapshot", Permission = "dispatch.read" });
        registrar.AddRoute(new BdvmWebRoute { Method = "POST", Path = "/api/modules/bdvm.dispatch/intent", Permission = "dispatch.control", IntentType = "bdvm.dispatch.intent.v1" });
        registrar.AddAsset(new BdvmWebAsset { Key = "modules/bdvm.dispatch/app.js", ContentType = "text/javascript" });
        registrar.AddSubscription(new BdvmRealtimeSubscription { Topic = "bdvm.dispatch.snapshot.v1", Permission = "dispatch.read" });
    }
}
