using System;
using WalkingPeople.Scripts.Core.MVC.ObserverLogic;

namespace WalkingPeople.Scripts.Core.MVC
{
    public class GameModel :  Observer
    {
        public GameEndResult GameEnd { get; private set; }
        public int CountUnits { get; private set; }
        public int Score { get; private set; }
        public int NotMissingUnits { get; private set; }
        public float RightBorder { get; private set; }
        public float TopBorder { get; private set; }
        public float Scater { get; private set; }

        private static GameModel _instance;

        public static GameModel Instance()
        {
            if (_instance == null)
            {
                _instance = new GameModel();
            }
        
            return _instance;
        }

        public static void ClearInstance()
        {
            _instance = null;
        }

        public void EndGame(bool winner)
        {
            GameEnd = winner ? GameEndResult.Winner : GameEndResult.Looser;
        }

        public void SetCountUnits(int count)
        {
            if (count == 0) throw  new NotImplementedException("[Game Model] Counts of unit can't be zero'");
            CountUnits = count;
        }
   
        public void AddScore()
        {
            Score++;
        }

        public void SetBorder(float right, float top)
        {
            RightBorder = right;
            TopBorder = top;
        }

        public void SetPermissiveMisUnits(int count)
        {
            NotMissingUnits = count;
        }

        public void MissedUnit()
        {
            NotMissingUnits--;
        }

        public void SetScatter(float count)
        {
            Scater = count;
        }
    }
}