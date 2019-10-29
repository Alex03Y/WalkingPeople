using TMPro;
using UnityEngine;
using WalkingPeople.Scripts.MVC;
using WalkingPeople.Scripts.MVC.Observer;

namespace WalkingPeople.Scripts.GUI
{
    public class ScoreView : MonoBehaviour, IObservable
    {
        [SerializeField] private TextMeshProUGUI Score;
        private GameModel _gameModel;

        private void Start()
        {
            _gameModel = GameModel.Instance();
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
