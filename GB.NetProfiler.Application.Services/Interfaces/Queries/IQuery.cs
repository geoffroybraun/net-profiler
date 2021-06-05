using MediatR;

namespace GB.NetProfiler.Application.Services.Interfaces.Queries
{
    /// <summary>
    /// Represents a query which returns a <see cref="TResult"/> when handled
    /// </summary>
    /// <typeparam name="TResult">The result type when handled</typeparam>
    public interface IQuery<TResult> : IRequest<TResult> { }
}
