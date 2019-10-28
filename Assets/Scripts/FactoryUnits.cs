using System;
using System.Collections;
using System.Collections.Generic;
using PoolManager;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class FactoryUnits : MonoBehaviour
{
    private PoolManager.PoolManager _poolManager;
    private int _countUnitForward, _countUnitDiagonal, _countUnitFriend;
    private GameObject _unitForvard, _unitDiagonal, _unitFriend;
    
    private void Awake()
    {
        _poolManager = ServiceLocator.ServiceLocator.Resolve<PoolManager.PoolManager>();
        _poolManager.CreatePool(_unitForvard, _countUnitForward);
        _poolManager.CreatePool(_unitDiagonal, _countUnitDiagonal);
        _poolManager.CreatePool(_unitFriend, _countUnitFriend);
    }

    public enum TypeUnit
    {
        MoveForward,
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
                UnityEngine.Debug.Log("MoveForward");
                unit = _poolManager.InstantiateFromPool(_unitForvard, position);
                break;
            case TypeUnit.MoveForwardAndDiagonal:
                UnityEngine.Debug.Log("MoveDiagonal");
                unit = _poolManager.InstantiateFromPool(_unitDiagonal, position);
                break;
            case TypeUnit.Friendly :
                UnityEngine.Debug.Log("Friend"); 
                unit = _poolManager.InstantiateFromPool(_unitFriend, position);
                break;
        }

        return unit;
    }
}
