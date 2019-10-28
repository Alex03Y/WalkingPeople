using System;
using System.Collections;
using System.Collections.Generic;
using PoolManager;
using UnityEngine;

public class UnitFriend : StateController, IPoolObject
{
    public override void CreateState()
    {
        LifeState = new LifeMove(this);
        DieState = new DieFriend(this);
        KillState = new KillFriend(this);
        CurrentState = LifeState;
    }

    private void Update()
    {
        throw new NotImplementedException();
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
