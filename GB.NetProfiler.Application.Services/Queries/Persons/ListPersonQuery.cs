using GB.NetProfiler.Application.Services.DTOs;
using GB.NetProfiler.Application.Services.Interfaces.Queries;
using System.Collections.Generic;

namespace GB.NetProfiler.Application.Services.Queries.Persons
{
    /// <summary>
    /// Represents a query which retrieves all stored <see cref="PersonDto"/>
    /// </summary>
    public sealed record ListPersonQuery : IQuery<IEnumerable<PersonDto>> { }
}
