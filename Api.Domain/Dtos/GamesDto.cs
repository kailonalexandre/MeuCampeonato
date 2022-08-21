using System;

namespace Domain.Dtos
{
    public class GamesDto : BaseDto
    {
        public string FirstTeam { get; set; }
        public string SecondTeam { get; set; }
        public int GoalsFistTeam { get; set; }
        public int GoalsSecondTeam { get; set; }
        public int TeamKey { get; set; }

    }
}
