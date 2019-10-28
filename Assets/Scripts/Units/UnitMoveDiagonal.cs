using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using PoolManager;
using UnityEngine;

public class UnitMoveDiagonal : StateController, IPoolObject
{
    public override void CreateState()
    {
        LifeState = new LifeMove(this);
        DieState = new DieEnemy(this);
        KillState = new KillEnemy(this);
        CurrentState = LifeState;
    }

    private void Update()
    {
        
    }

    public void OnAwake(PoolObject poolObject)
    {
        CreateState();
    }

    public void OnReuseObject()
    {
        throw new NotImplementedException();
    }

    public void OnDisposeObject()
    {
        throw new NotImplementedException();
    }
}
