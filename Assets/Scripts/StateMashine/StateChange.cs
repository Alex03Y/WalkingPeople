using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChange : MonoBehaviour
{
    private StateController _state;

    public void GetLifeState()
    {
        _state.getLifeState();
    }

    public void GetDieState()
    {
        _state.getDieState();
    }
}
