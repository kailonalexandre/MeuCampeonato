using Domain.Utils;
using System;

namespace Domain.Dtos
{
    public class MatchDto : BaseDto
    {
        public string TeamA { get; set; }
        public string TeamB { get; set; }
        public int ScoreTeamA { get; set; }
        public int ScoreTeamB { get; set; }
        public BracketEnum Bracket { get; set; }
        public bool Draw { get; set; }

    }
}
