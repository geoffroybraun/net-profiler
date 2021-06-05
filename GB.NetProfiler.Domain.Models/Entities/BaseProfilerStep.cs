using GB.NetProfiler.Domain.Models.Enums;

namespace GB.NetProfiler.Domain.Models.Entities
{
    /// <summary>
    /// Represents an abstract profile step with type, message and optional parameters
    /// </summary>
    public abstract record BaseProfilerStep
    {
        public EProfilerStepType Type { get; init; }

        public string Message { get; init; }
    }
}
