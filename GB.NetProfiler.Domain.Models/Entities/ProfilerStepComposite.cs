using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GB.NetProfiler.Domain.Models.Entities
{
    /// <summary>
    /// Represents a composite <see cref="BaseProfilerStep"/> implementation which can belong to a parent step and have optional children
    /// </summary>
    public sealed record ProfilerStepComposite : BaseProfilerStep, IDisposable
    {
        #region Fields

        private readonly Stopwatch Watch = new();
        private bool HasDisposed = false;

        #endregion

        #region Properties

        /// <summary>
        /// The parent step for composite-relationship
        /// </summary>
        public ProfilerStepComposite Parent { get; init; }

        /// <summary>
        /// The execution time it took from the current step instanciation to its destruction
        /// </summary>
        public TimeSpan ExecutionTime { get; private set; }

        /// <summary>
        /// The optional children for composite-relationship
        /// </summary>
        public IList<BaseProfilerStep> Children { get; init; } = new List<BaseProfilerStep>();

        #endregion

        #region Events

        /// <summary>
        /// The event to invoke when disposing the current step to retrieve its parent for composite-relationship
        /// </summary>
        public event Action<ProfilerStepComposite> RetrieveParent = delegate { };

        #endregion

        public ProfilerStepComposite() => Watch.Start();

        ~ProfilerStepComposite() => Dispose(false);

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        public void Dispose(bool isDisposing)
        {
            if (!isDisposing || HasDisposed)
                return;

            Watch.Stop();
            ExecutionTime = Watch.Elapsed;
            RetrieveParent(Parent);
            HasDisposed = true;
        }
    }
}
