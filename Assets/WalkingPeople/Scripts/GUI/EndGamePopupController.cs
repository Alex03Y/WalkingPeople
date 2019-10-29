using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WalkingPeople.Scripts.Core.MVC;
using WalkingPeople.Scripts.Core.MVC.ObserverLogic;

namespace WalkingPeople.Scripts.GUI
{
    public class EndGamePopupController : MonoBehaviour, IObservable
    {
        [SerializeField] private Button Replay;
        [SerializeField] private TextMeshProUGUI Score;
        [SerializeField] private Image Vignette;
        [SerializeField] private float DurationForPopup = 0.5f, DurationForVignette = 0.35f;
        [SerializeField] private AnimationCurve CurveForPopup;

        private GameModel _gameModel;

        private void Awake()
        {
            Time.timeScale = 1f;
            transform.localScale = Vector3.zero;
            Vignette.gameObject.SetActive(false);
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

        private void ShowVignette()
        {
            var color = Color.black;
            color.a *= 0f;
            Vignette.color = color;
            Vignette.gameObject.SetActive(true);
        
            DOTween.ToAlpha(() => Vignette.color, x => Vignette.color = x, 0.5f, DurationForVignette)
                .OnComplete(() => ShowPopup());
            Time.timeScale = 0f;
        }
        private void ShowPopup()
        {
            Score.text = "Score: " + _gameModel.Score;
            transform.DOScale(Vector3.one, DurationForPopup).SetEase(CurveForPopup);
        }

        private void OnDestroy()
        {
            _gameModel.RemoveObservable(this);

        }

        public void OnObjectChanged(IObserver observer)
        {
            if (_gameModel.Score == _gameModel.CountUnits) _gameModel.EndGame(true);
            if (_gameModel.NotMissingUnits == 0) _gameModel.EndGame(false);
            if (_gameModel.GameEnd != GameEndResult.NotEnded) ShowVignette();
        }
    }
}
