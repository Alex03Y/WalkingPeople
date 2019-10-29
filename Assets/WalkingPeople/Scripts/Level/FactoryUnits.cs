using UnityEngine;
using WalkingPeople.Scripts.Pool;

namespace WalkingPeople.Scripts.Level
{
    public class FactoryUnits : MonoBehaviour
    {
        [SerializeField] private GameObject _unitForvard, _unitDiagonal, _unitFriend;
        [SerializeField] private int _countUnitForward, _countUnitDiagonal, _countUnitFriend;

        private PoolManager _poolManager;
    
        private void Start()
        {
            _poolManager = PoolManager._instance;
            _poolManager.CreatePool(_unitForvard, _countUnitForward);
            _poolManager.CreatePool(_unitDiagonal, _countUnitDiagonal);
            _poolManager.CreatePool(_unitFriend, _countUnitFriend);
        }

        public enum TypeUnit
        {
            MoveForward = 1,
            MoveForwardAndDiagonal = 2,
            Friendly = 3
        }

        private TypeUnit _typeUnit = TypeUnit.Friendly;

        public PoolObject GetUnit (TypeUnit type, Vector3 position)
        {
            PoolObject unit = null;
            switch (type)
            {
                case TypeUnit.MoveForward :
                    unit = _poolManager.InstantiateFromPool(_unitForvard, position);
                    break;
                case TypeUnit.MoveForwardAndDiagonal:
                    unit = _poolManager.InstantiateFromPool(_unitDiagonal, position);
                    break;
                case TypeUnit.Friendly :
                    unit = _poolManager.InstantiateFromPool(_unitFriend, position);
                    break;
            }

            return unit;
        }
    
    }
}
