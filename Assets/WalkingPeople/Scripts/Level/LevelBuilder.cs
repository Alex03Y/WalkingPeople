using System.Collections;
using UnityEngine;
using WalkingPeople.Scripts.MVC;
using WalkingPeople.Scripts.MVC.Observer;
using Random = UnityEngine.Random;

namespace WalkingPeople.Scripts.Level
{
    public class LevelBuilder : MonoBehaviour, IObservable
    {
        [SerializeField] private float Scatter = 1f;
        [SerializeField] private int CountUnits;
        [SerializeField] private float SpawnRate = 1f;
        [SerializeField] private FactoryUnits _factory;
        [SerializeField] private int AllowedMissUnits = 3;
        private float _leftPoint, _rightPoint, _topPoint;
        private GameModel _gameModel;
        private IEnumerator _coroutineLoop;

        private void Awake()
        {
            _gameModel = GameModel.Instance();
            _gameModel.AddObserver(this);
        }

        private void Start()
        {
            _rightPoint = _gameModel.RightBorder - Scatter;
            _topPoint = _gameModel.TopBorder + Scatter;
            _leftPoint = _rightPoint * -1;
            _gameModel.SetCountUnits(CountUnits);
            StartCoroutine(_coroutineLoop = LoopSpawnUnits());
            _gameModel.SetPermissiveMisUnits(AllowedMissUnits);
        }

        private IEnumerator LoopSpawnUnits()
        {
            for (var i = 0; i < CountUnits; i++)
            {
                var RandomPosition = new Vector3(UnityEngine.Random.Range(_leftPoint, _rightPoint), _topPoint, 0f);
                var rnd = Random.Range(1, 4);
                _factory.GetUnit((FactoryUnits.TypeUnit) rnd, RandomPosition);
                yield return new WaitForSeconds(SpawnRate);
            }
        }

        private void OnDestroy()
        {
            StopCoroutine(_coroutineLoop);
            _gameModel.RemoveObservable(this);
            GameModel.ClearInstance();
        }

        public void OnObjectChanged(IObserver observer)
        {
            if (_gameModel.GameEnd != GameEndResult.NotEnded)
            {
                StopCoroutine(_coroutineLoop);
            }
        }
    }
}
