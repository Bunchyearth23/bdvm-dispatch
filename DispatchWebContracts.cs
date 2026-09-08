using System;
using System.Collections.Generic;

namespace BDVM.Dispatch;

public sealed class DispatchWebSnapshot
{
    public int SchemaVersion { get; set; } = 1;
    public long Version { get; set; }
    public string CorrelationId { get; set; } = "";
    public IReadOnlyList<DispatchFeature> Tracks { get; set; } = Array.Empty<DispatchFeature>();
    public IReadOnlyList<DispatchFeature> Junctions { get; set; } = Array.Empty<DispatchFeature>();
    public IReadOnlyList<DispatchFeature> Signals { get; set; } = Array.Empty<DispatchFeature>();
    public IReadOnlyList<DispatchFeature> Occupations { get; set; } = Array.Empty<DispatchFeature>();
    public IReadOnlyList<DispatchFeature> Trains { get; set; } = Array.Empty<DispatchFeature>();
    public IReadOnlyList<DispatchActionDescriptor> Actions { get; set; } = Array.Empty<DispatchActionDescriptor>();
}

public sealed class DispatchFeature
{
    public string Id { get; set; } = "";
    public string Label { get; set; } = "";
    public string State { get; set; } = "";
    public double X { get; set; }
    public double Y { get; set; }
}

public sealed class DispatchActionDescriptor
{
    public string Label { get; set; } = "";
    public string IntentType { get; set; } = "";
    public IReadOnlyDictionary<string, object> Payload { get; set; } = new Dictionary<string, object>();
    public string Confirmation { get; set; } = "";
}
