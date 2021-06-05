using GB.NetProfiler.Domain.Models.Entities;
using GB.NetProfiler.Domain.Models.Enums;
using GB.NetProfiler.Domain.Models.Interfaces.Services;
using GB.NetProfiler.Domain.Services.MessageBuilders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GB.NetProfiler.Application.Services.Profilers
{
    public sealed class StepProfiler : IProfiler
    {
        #region Fields

        private readonly List<BaseProfilerStep> RootSteps = new();
        private bool HasDisposed = false;
        private BaseProfilerStep CurrentStep;

        #endregion

        ~StepProfiler() => Dispose(false);

        public void AddLeaf(EProfilerStepType type, string message)
        {
            var step = new ProfilerStepLeaf()
            {
                Message = message,
                Type = type
            };
            AddChildToCurrentStep(step);
        }

        public IDisposable AddStep(EProfilerStepType type, string message)
        {
            var step = new ProfilerStepComposite()
            {
                Children = new List<BaseProfilerStep>(),
                Message = message,
                Parent = CurrentStep is ProfilerStepComposite composite ? composite : default,
                Type = type
            };
            step.RetrieveParent += SetCurrentStep;
            AddChildToCurrentStep(step);
            SetCurrentStep(step);

            return step;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        public void Dispose(bool isDisposing)
        {
            if (!isDisposing || HasDisposed)
                return;

            if (RootSteps is not null && RootSteps.Any())
                Console.WriteLine(ProfilerStepMessageBuilder.Build(RootSteps));

            RootSteps.Clear();
            HasDisposed = true;
        }

        #region Private methods

        private void AddChildToCurrentStep(BaseProfilerStep step)
        {
            if (CurrentStep is ProfilerStepComposite composite)
            {
                composite.Children.Add(step);

                return;
            }

            RootSteps.Add(step);
        }

        private void SetCurrentStep(ProfilerStepComposite step) => CurrentStep = step;

        #endregion
    }
}
