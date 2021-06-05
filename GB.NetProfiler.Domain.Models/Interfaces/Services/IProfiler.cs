using GB.NetProfiler.Domain.Models.Enums;
using System;

namespace GB.NetProfiler.Domain.Models.Interfaces.Services
{
    /// <summary>
    /// Represents a profiler which registers steps
    /// </summary>
    public interface IProfiler : IDisposable
    {
        /// <summary>
        /// Register a new step and retrieve it for composite-relationship
        /// </summary>
        /// <param name="type">The step type</param>
        /// <param name="message">The formatted step message</param>
        /// <returns>The registered step for composite-relationship</returns>
        IDisposable AddStep(EProfilerStepType type, string message);

        /// <summary>
        /// Register a new step without retrieving it
        /// </summary>
        /// <param name="type">The step type</param>
        /// <param name="message">The formatted step message</param>
        void AddLeaf(EProfilerStepType type, string message);
    }
}
