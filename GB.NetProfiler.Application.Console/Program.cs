using GB.NetProfiler.Application.Services.Handlers;
using GB.NetProfiler.Application.Services.Profilers;
using GB.NetProfiler.Application.Services.Queries.Persons;
using GB.NetProfiler.Domain.Models.Enums;
using GB.NetProfiler.Domain.Models.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace GB.NetProfiler.Application.Console
{
    public static class Program
    {
        public static async Task Main()
        {
            var provider = GetServiceProvider();
            var mediator = provider.GetService<IMediator>();

            using var profiler = provider.GetService<IProfiler>();
            using (profiler.AddStep(EProfilerStepType.Start, $"Starting execution at {DateTime.UtcNow.ToLongTimeString()}"))
            {
                _ = await mediator.Send(new ListPersonQuery()).ConfigureAwait(true);
                _ = await mediator.Send(new ListPersonQuery()).ConfigureAwait(true);
                _ = await mediator.Send(new ListPersonQuery()).ConfigureAwait(true);
                _ = await mediator.Send(new ListPersonQuery()).ConfigureAwait(true);
            }
            profiler.AddLeaf(EProfilerStepType.Stop, $"Execution complete at {DateTime.UtcNow.ToLongTimeString()}");
        }

        #region Fields

        private static IServiceProvider GetServiceProvider()
        {
            return new ServiceCollection()
                .AddSingleton<IProfiler, StepProfiler>()
                .AddMediatR(typeof(BaseQueryHandler<,>))
                .BuildServiceProvider();
        }

        #endregion
    }
}
