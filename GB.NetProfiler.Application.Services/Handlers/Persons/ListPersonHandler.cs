using GB.NetProfiler.Application.Services.DTOs;
using GB.NetProfiler.Application.Services.Queries.Persons;
using GB.NetProfiler.Domain.Models.Enums;
using GB.NetProfiler.Domain.Models.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GB.NetProfiler.Application.Services.Handlers.Persons
{
    /// <summary>
    /// Represents a handler which executes an <see cref="ListPersonQuery"/> query
    /// </summary>
    public sealed class ListPersonHandler : BaseQueryHandler<ListPersonQuery, IEnumerable<PersonDto>>
    {
        #region Fields

        private readonly IProfiler Profiler;

        #endregion

        public ListPersonHandler(IProfiler profiler) => Profiler = profiler ?? throw new ArgumentNullException(nameof(profiler));

        public override Task<IEnumerable<PersonDto>> ExecuteAsync(ListPersonQuery query)
        {
            using (Profiler.AddStep(EProfilerStepType.CallMethod, $"'{nameof(GetPersons)}' of class '{nameof(ListPersonHandler)}'"))
            {
                var persons = GetPersons();
                Profiler.AddLeaf(EProfilerStepType.Info, $"{persons.Count()} persons found");

                return Task.FromResult(persons);
            }
        }

        #region Private methods

        private static IEnumerable<PersonDto> GetPersons()
        {
            Thread.Sleep(GetRandom());
            var birthYear = DateTime.UtcNow.Year;

            return new[]
            {
                new PersonDto() { Birthdate = new(birthYear, 10, 19), Firstname = "Stan", ID = 1, Lastname = "Marsh" },
                new PersonDto() { Birthdate = new(birthYear, 5, 26), Firstname = "Kyle", ID = 2, Lastname = "Broflovski" },
                new PersonDto() { Birthdate = new(birthYear, 3, 22), Firstname = "Kenny", ID = 3, Lastname = "MacKormick" },
                new PersonDto() { Birthdate = new(birthYear, 6, 1), Firstname = "Eric", ID = 4, Lastname = "Cartman" },
            };
        }

        private static int GetRandom() => new Random().Next(100, 1000);

        #endregion
    }
}
