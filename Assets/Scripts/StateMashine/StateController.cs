using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateController : MonoBehaviour
{
    protected IState LifeState;
    protected IState DieState;
    protected IState KillState;

    protected IState CurrentState;

    public abstract void CreateState();
    
    public virtual void getLifeState()
    {
        CurrentState = LifeState;
    }

    public virtual void getDieState()
    {
        CurrentState = DieState;
    }

    public virtual void getKillState()
    {
        CurrentState = KillState;
    }
}
