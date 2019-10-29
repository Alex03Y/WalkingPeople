using UnityEngine;
using WalkingPeople.Scripts.Core.MVC;
using WalkingPeople.Scripts.Core.Pool;
using WalkingPeople.Scripts.StatesLogic;
using WalkingPeople.Scripts.Utilits;

namespace WalkingPeople.Scripts.Units
{
    public class UnitEnemyForward : StateController, IPoolObject
    {
        [SerializeField] private float Speed;
        [SerializeField] private Vector3 Direction = new Vector3(0f, -1f, 0f); 
        [SerializeField] private SpriteSheetPlayer PlayerAnimation;
       
        private GameModel _gameModel;
        private PoolObject _poolObject;
        
        public void OnAwake(PoolObject poolObject)
        {
            _gameModel = GameModel.Instance();
            _state = State.Move;
            _poolObject = poolObject;
        }

        private void Update()
        {
            switch (_state)
            {
                case State.Move :
                    MoveForward();
                    break;
                
                case State.OnClick :
                    _gameModel.AddScore();
                    _gameModel.SetChanged();
                    OnDisposeObject();
                    break;
                
                case State.OutOfScreen :
                    _gameModel.MissedUnit();
                    _gameModel.SetChanged();
                    OnDisposeObject();
                    break;
            }
        }

        private void MoveForward()
        {
            var currentPosition = transform.position;
            var nextPosition = new Vector3(currentPosition.x, currentPosition.y, 0f);
            
            nextPosition += (Time.deltaTime * Speed) * Direction;
            transform.position = nextPosition;
        }
        
        public void OnReuseObject()
        {
            Life();
            PlayerAnimation.StartAnimation();
        }

        public void OnDisposeObject()
        {
            PlayerAnimation.StopAnimation();
            PoolManager.instance.DisposePoolObject(_poolObject);
        }
    }
}
