using System;

namespace GB.NetProfiler.Application.Services.DTOs
{
    /// <summary>
    /// Represents a person with firstname, lastname and birthdate
    /// </summary>
    [Serializable]
    public sealed record PersonDto
    {
        public int ID { get; init; }

        public string Firstname { get; init; }

        public string Lastname { get; init; }

        public DateTime Birthdate { get; init; }
    }
}
