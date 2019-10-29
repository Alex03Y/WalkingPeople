using System.Collections;
using UnityEngine;
using WalkingPeople.Scripts.Core.MVC;
using WalkingPeople.Scripts.Core.MVC.ObserverLogic;
using Random = UnityEngine.Random;

namespace WalkingPeople.Scripts.Level
{
    public class LevelBuilder : MonoBehaviour, IObservable
    {
        [SerializeField] private int CountUnits;
        [SerializeField] private float SpawnRate = 1f;
        [SerializeField] private FactoryUnits Factory;
        [SerializeField] private int AllowedMissUnits = 3;

        private float _leftPoint, _rightPoint, _topPoint, _scatter;
        private GameModel _gameModel;
        private IEnumerator _coroutineLoop;

        private void Awake()
        {
            _gameModel = GameModel.Instance();
            _gameModel.AddObserver(this);
        }

        private void Start()
        {
            _scatter = _gameModel.Scater;
            
            _rightPoint = _gameModel.RightBorder - _scatter;
            _topPoint = _gameModel.TopBorder + _scatter;
            _leftPoint = _rightPoint * -1;
            
            _gameModel.SetCountUnits(CountUnits);
            _gameModel.SetPermissiveMisUnits(AllowedMissUnits);

            StartCoroutine(_coroutineLoop = LoopSpawnUnits());
        }

        private IEnumerator LoopSpawnUnits()
        {
            for (var i = 0; i < CountUnits; i++)
            {
                var randomPosition = new Vector3(Random.Range(_leftPoint, _rightPoint), _topPoint, 0f);
                
                // I don't know, but if set Range (1, 3), number 3 - never appeared. If maxCount = x, that real maxCount = x-1;
                int rnd = Random.Range(1, 4);
                Factory.GetUnit((FactoryUnits.TypeUnit) rnd, randomPosition);
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
