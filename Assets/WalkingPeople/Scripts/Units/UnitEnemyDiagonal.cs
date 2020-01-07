using System.Collections;
using UnityEngine;
using WalkingPeople.Scripts.Core.MVC;
using WalkingPeople.Scripts.Core.Pool;
using WalkingPeople.Scripts.Core.Service;
using WalkingPeople.Scripts.StatesLogic;
using WalkingPeople.Scripts.Utilits;
using Random = UnityEngine.Random;

namespace WalkingPeople.Scripts.Units
{
    public class UnitEnemyDiagonal : StateController, IPoolObject
    {
        [SerializeField] protected float Speed;
        [SerializeField] protected float TimeChangeDirection = 1f;
        [SerializeField] protected SpriteSheetPlayer PlayerAnimation;
        [SerializeField] protected Vector3[] Directions =
            {new Vector3(1f, -1f, 0f),new Vector3(0f, -1f, 0f), new Vector3(-1f, -1f, 0f)};

        protected GameModel _gameModel;
        protected PoolObject _poolObject;
        protected Vector3 _currentDirection;
        protected IEnumerator _choiceDirection;
        protected float _scatter, _borderPosition, _interpolatedOffset;


        public void OnAwake(PoolObject poolObject)
        {
            _gameModel = ServiceLocator.Resolve<GameModel>();
            _poolObject = poolObject;
            _scatter = _gameModel.IndentFromEdges;
            _borderPosition = _gameModel.RightBorder - _scatter;
            _interpolatedOffset = Directions[0].normalized.x * Speed * TimeChangeDirection;
        }

        protected virtual void Update()
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

        protected void MoveToDirection()
        {
            var currentPosition = transform.position;
            var nextPosition = new Vector3(currentPosition.x, currentPosition.y, 0f);
            nextPosition += Time.deltaTime * Speed * _currentDirection;
            transform.position = nextPosition;
        }

        protected IEnumerator NextDirection()
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
