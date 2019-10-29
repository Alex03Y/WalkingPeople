using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WalkingPeople.Scripts.MVC;
using WalkingPeople.Scripts.MVC.Observer;

namespace WalkingPeople.Scripts.GUI
{
    public class EndGamePopupController : MonoBehaviour, IObservable
    {
        [SerializeField] private Button Replay;
        [SerializeField] private TextMeshProUGUI Score;
        [SerializeField] private float Duration = 0.5f;

        private GameModel _gameModel;

        private void Awake()
        {
            Time.timeScale = 1f;
            transform.localScale = Vector3.zero;
        }

        private void Start()
        {
            _gameModel = GameModel.Instance();
            _gameModel.AddObserver(this);
            Replay.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            });
        }

        private void ShowPopup()
        {
            Score.text = "Score: " + _gameModel.Score;
            transform.DOScale(Vector3.one, Duration);
            Time.timeScale = 0f;
        }

        private void OnDestroy()
        {
            _gameModel.RemoveObservable(this);

        }

        public void OnObjectChanged(IObserver observer)
        {
            if (_gameModel.Score == _gameModel.CountUnits) _gameModel.EndGame(true);
            if (_gameModel.NotMissingUnits == 0) _gameModel.EndGame(false);
            if (_gameModel.GameEnd != GameEndResult.NotEnded) ShowPopup();
        }
    }
}
