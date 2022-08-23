
using Domain.Utils;
using System.Collections.Generic;

namespace MeuCampeonato.Domain.Entities
{
    public class MatchEntity : BaseEntity
    {
        public string TeamA { get; set; }
        public string TeamB { get; set; }
        public int ScoreTeamA { get; set; }
        public int ScoreTeamB { get; set; }
        public BracketEnum Bracket { get; set; }

    }
}
