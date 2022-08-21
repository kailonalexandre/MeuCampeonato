using System;

namespace Domain.Models
{
    public class GamesModel : BaseModel
    {

        private string _firstTeam;

        public string FirstTeam
        {
            get { return _firstTeam; }
            set { _firstTeam = value; }
        } 
        private string _secondTeam;

        public string SecondTeam
        {
            get { return _secondTeam; }
            set { _secondTeam = value; }
        }

        private int _goalsFirstTeam;

        public int GoalsFirstTeam
        {
            get { return _goalsFirstTeam; }
            set { _goalsFirstTeam = value; }
        } 
        private int _goalsSecondTeam;

        public int GoalsSecond
        {
            get { return _goalsSecondTeam; }
            set { _goalsSecondTeam = value; }
        }

        private int _teamKey;

        public int TeamKey
        {
            get { return _teamKey; }
            set { _teamKey = value; }
        }

    }
}
