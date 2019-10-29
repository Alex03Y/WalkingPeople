using System;
using System.Collections;
using UnityEngine;
using WalkingPeople.Scripts.Core.MVC;
using WalkingPeople.Scripts.Core.Pool;
using WalkingPeople.Scripts.StatesLogic;
using WalkingPeople.Scripts.Utilits;
using Random = UnityEngine.Random;

namespace WalkingPeople.Scripts.Units
{
    public class UnitEnemyDiagonal : StateController, IPoolObject
    {
        [SerializeField] private float Speed;
        [SerializeField] private float TimeChangeDirection = 1f;
        [SerializeField] private SpriteSheetPlayer PlayerAnimation;
        [SerializeField] private Vector3[] Directions =
            {new Vector3(0f, -1f, 0f), new Vector3(1f, -1f, 0f), new Vector3(-1f, -1f, 0f)};

        private GameModel _gameModel;
        private PoolObject _poolObject;
        private Vector3 _currentDirection;
        private IEnumerator _choiceDirection;
        private float _scatter, _borderPosition;


        public void OnAwake(PoolObject poolObject)
        {
            _gameModel = GameModel.Instance();
            _state = State.Move;
            _poolObject = poolObject;
            _currentDirection = Directions[0];
            _choiceDirection = NextDirection();
        }

        private void Start()
        {
            _scatter = _gameModel.Scater;
            _borderPosition = _gameModel.RightBorder - _scatter;
        }

        private void Update()
        {
            switch (_state)
            {
                case State.Move:
                    MoveToDirection();
                    break;
                
                case State.OnClick:
                    _gameModel.AddScore();
                    _gameModel.SetChanged();
                    OnDisposeObject();
                    break;
                
                case State.OutOfScreen:
                    _gameModel.MissedUnit();
                    _gameModel.SetChanged();
                    OnDisposeObject();
                    break;
            }
        }

        private void MoveToDirection()
        {
            var currentPosition = transform.position;
            var nextPosition = new Vector3(currentPosition.x, currentPosition.y, 0f);
            
            nextPosition += Time.deltaTime * Speed * _currentDirection;
            nextPosition = new Vector3(Mathf.Clamp(nextPosition.x, -_borderPosition, _borderPosition), nextPosition.y,0f); 
            
            transform.position = nextPosition;
        }

        private IEnumerator NextDirection()
        {
            while (true)
            {
                yield return new WaitForSeconds(TimeChangeDirection);
                
                // I don't know, but if set Range (1, 3), number 3 - never appeared. If maxCount = x, that real maxCount = x-1;
                var rnd = Random.Range(0, 3);
                _currentDirection = Directions[rnd].normalized;
                PlayerAnimation.SetState(rnd);
            }
        }
        
        public void OnReuseObject()
        {
            Life();
            StartCoroutine(_choiceDirection);
            PlayerAnimation.StartAnimation();
        }

        public void OnDisposeObject()
        {
            PlayerAnimation.StopAnimation();
            StopCoroutine(_choiceDirection);
            PoolManager.instance.DisposePoolObject(_poolObject);
        }
    }
}
