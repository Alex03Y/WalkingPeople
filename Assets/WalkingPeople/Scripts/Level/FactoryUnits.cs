using UnityEngine;
using WalkingPeople.Scripts.Core.MVC;
using WalkingPeople.Scripts.Core.Pool;
using WalkingPeople.Scripts.Core.Service;

namespace WalkingPeople.Scripts.Level
{
    public class FactoryUnits : MonoBehaviour
    {
        [SerializeField] private GameObject UnitForvard, UnitDiagonal, UnitFriend;
        [SerializeField] private int CountUnitForward, CountUnitDiagonal, CountUnitFriend;
        [SerializeField] private float IndentFromEdges = 0.5f;

        private PoolManager _poolManager;
        private GameModel _gameModel;

        private void Awake()
        {
            _gameModel = ServiceLocator.Resolve<GameModel>();
            _gameModel.SetScatter(IndentFromEdges);
        }

        private void Start()
        {
            _poolManager = ServiceLocator.Resolve<PoolManager>();
            _poolManager.CreatePool(UnitForvard, CountUnitForward);
            _poolManager.CreatePool(UnitDiagonal, CountUnitDiagonal);
            _poolManager.CreatePool(UnitFriend, CountUnitFriend);
        }

        public enum TypeUnit
        {
            MoveForward = 1,
            MoveForwardAndDiagonal,
            Friendly
        }

        private TypeUnit _typeUnit = TypeUnit.Friendly;

        public PoolObject GetUnit (TypeUnit type, Vector3 position)
        {
            PoolObject unit = null;
            switch (type)
            {
                case TypeUnit.MoveForward :
                    unit = _poolManager.InstantiateFromPool(UnitForvard, position);
                    break;
                case TypeUnit.MoveForwardAndDiagonal:
                    unit = _poolManager.InstantiateFromPool(UnitDiagonal, position);
                    break;
                case TypeUnit.Friendly :
                    unit = _poolManager.InstantiateFromPool(UnitFriend, position);
                    break;
            }

            return unit;
        }
    
    }
}
