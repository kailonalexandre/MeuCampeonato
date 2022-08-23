using Domain.Utils;
using System;

namespace Domain.Models
{
    public class MatchsModel : BaseModel
    {

        private string _teamA;

        public string TeamA
        {
            get { return _teamA; }
            set { _teamA = value; }
        } 
        private string _teamB;

        public string TeamB
        {
            get { return _teamB; }
            set { _teamB = value; }
        }

        private int _scoreTeamA;

        public int ScoreTeamA
        {
            get { return _scoreTeamA; }
            set { _scoreTeamA = value; }
        } 
        private int _scoreTeamB;

        public int ScoreTeamB
        {
            get { return _scoreTeamB; }
            set { _scoreTeamB = value; }
        }

        private BracketEnum _bracket;

        public BracketEnum Bracket
        {
            get { return _bracket; }
            set { _bracket = value; }
        }

    }
}
