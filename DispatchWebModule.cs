using BDVM.Common;

namespace BDVM.Dispatch;

public sealed class DispatchWebModule : IBdvmWebModule
{
    public BdvmWebModuleManifest Manifest { get; } = new BdvmWebModuleManifest
    {
        Id = "BDVM.Dispatch",
        DisplayName = "BDVM - Dispatch",
        ModuleVersion = "1.1.1",
        RequiredWebApi = new BdvmApiRange(new BdvmApiVersion(1, 0), new BdvmApiVersion(1, 0)),
        RouteNamespace = "/api/modules/bdvm.dispatch",
        AssetNamespace = "modules/bdvm.dispatch",
        Capabilities = new[] { "bdvm.dispatch.network-view.v1", "bdvm.dispatch.route-control.v1", "bdvm.dispatch.realtime.v1" },
        Permissions = new[] { "dispatch.read", "dispatch.junction.control", "dispatch.route.control" }
    };

    public void Register(IBdvmWebRegistrar registrar)
    {
        registrar.AddNavigation(new BdvmWebNavigationItem { Id = "bdvm.dispatch.network", Label = "Dispatch", Path = "/dispatch", Order = 100 });
        registrar.AddRoute(new BdvmWebRoute { Method = "GET", Path = "/api/modules/bdvm.dispatch/snapshot", Permission = "dispatch.read" });
        registrar.AddRoute(new BdvmWebRoute { Method = "GET", Path = "/api/modules/bdvm.dispatch/map", Permission = "dispatch.read" });
        registrar.AddRoute(new BdvmWebRoute { Method = "GET", Path = "/api/modules/bdvm.dispatch/trains", Permission = "dispatch.read" });
        registrar.AddRoute(new BdvmWebRoute { Method = "GET", Path = "/api/modules/bdvm.dispatch/tracks", Permission = "dispatch.read" });
        registrar.AddRoute(new BdvmWebRoute { Method = "GET", Path = "/api/modules/bdvm.dispatch/junctions", Permission = "dispatch.read" });
        registrar.AddRoute(new BdvmWebRoute { Method = "GET", Path = "/api/modules/bdvm.dispatch/signals", Permission = "dispatch.read" });
        registrar.AddRoute(new BdvmWebRoute { Method = "GET", Path = "/api/modules/bdvm.dispatch/occupations", Permission = "dispatch.read" });
        registrar.AddRoute(new BdvmWebRoute { Method = "POST", Path = "/api/modules/bdvm.dispatch/junction/control", Permission = "dispatch.junction.control", IntentType = "bdvm.dispatch.junction-control.v1" });
        registrar.AddRoute(new BdvmWebRoute { Method = "POST", Path = "/api/modules/bdvm.dispatch/route/control", Permission = "dispatch.route.control", IntentType = "bdvm.dispatch.route-control.v1" });
        registrar.AddAsset(new BdvmWebAsset { Key = "modules/bdvm.dispatch/app.js", ContentType = "text/javascript" });
        registrar.AddAsset(new BdvmWebAsset { Key = "modules/bdvm.dispatch/app.css", ContentType = "text/css" });
        registrar.AddSubscription(new BdvmRealtimeSubscription { Topic = "bdvm.dispatch.snapshot.v1", Permission = "dispatch.read" });
        registrar.AddSubscription(new BdvmRealtimeSubscription { Topic = "bdvm.dispatch.intent-result.v1", Permission = "dispatch.read" });
    }
}
