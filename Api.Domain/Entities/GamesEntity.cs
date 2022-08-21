
using System.Collections.Generic;

namespace Api.Domain.Entities
{
    public class GamesEntity : BaseEntity
    {
        public string FirstTeam { get; set; }
        public string SecondTeam { get; set; }
        public int GoalsFistTeam { get; set; }
        public int GoalsSecondTeam { get; set; }
        public int TeamKey { get; set; }

    }
}
