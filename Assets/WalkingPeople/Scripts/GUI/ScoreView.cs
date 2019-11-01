using TMPro;
using UnityEngine;
using WalkingPeople.Scripts.Core.MVC;
using WalkingPeople.Scripts.Core.MVC.ObserverLogic;
using WalkingPeople.Scripts.Core.Service;

namespace WalkingPeople.Scripts.GUI
{
    public class ScoreView : MonoBehaviour, IObservable
    {
        [SerializeField] private TextMeshProUGUI Score;
        
        private GameModel _gameModel;

        private void Start()
        {
            _gameModel = ServiceLocator.Resolve<GameModel>();
            _gameModel.AddObserver(this);
        }

        public void OnObjectChanged(IObserver observer)
        {
            Score.text =  "Score: " + _gameModel.Score;
        }

        private void OnDestroy()
        {
            _gameModel.RemoveObservable(this);

        }
    }
}
