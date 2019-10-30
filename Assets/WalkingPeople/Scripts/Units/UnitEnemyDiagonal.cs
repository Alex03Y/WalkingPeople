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
            {new Vector3(1f, -1f, 0f),new Vector3(0f, -1f, 0f), new Vector3(-1f, -1f, 0f)};

        private GameModel _gameModel;
        private PoolObject _poolObject;
        private Vector3 _currentDirection;
        private IEnumerator _choiceDirection;
        private float _scatter, _borderPosition, _interpolatedOffset;


        public void OnAwake(PoolObject poolObject)
        {
            _gameModel = GameModel.Instance();
            _poolObject = poolObject;
            _scatter = _gameModel.Scater;
            _borderPosition = _gameModel.RightBorder - _scatter;
            _interpolatedOffset = Directions[0].normalized.x * Speed * TimeChangeDirection;
        }

        private void Update()
        {
            switch (_currentState)
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
            transform.position = nextPosition;
        }

        private IEnumerator NextDirection()
        {
            while (true)
            {
                yield return new WaitForSeconds(TimeChangeDirection);
                
                var position = transform.position;
                int rnd;
                if (position.x >= _borderPosition - _interpolatedOffset)
                {
                    rnd = Random.Range(1, 3);
                } 
                else if (position.x <= -_borderPosition + _interpolatedOffset)
                {
                    rnd = Random.Range(0, 2);
                }
                else rnd = Random.Range(0, 3);
                _currentDirection = Directions[rnd].normalized;
                PlayerAnimation.SetState(rnd);
            }
        }

      
        public void OnReuseObject()
        {
            Life();
            _currentDirection = Directions[1];
            StartCoroutine(_choiceDirection = NextDirection());
            PlayerAnimation.StartAnimation();
        }

        public void OnDisposeObject()
        {
            StopCoroutine(_choiceDirection);
            PlayerAnimation.StopAnimation();
            _poolObject.Destroy();
        }
    }
}
