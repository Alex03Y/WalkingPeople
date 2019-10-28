using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeMove : IState
{
    private StateController _state;

    public LifeMove(StateController state)
    {
        _state = state;
    }
    
    public void DoIt()
    {
        
    }

   
}
