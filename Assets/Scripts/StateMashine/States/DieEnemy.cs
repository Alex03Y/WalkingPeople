using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEnemy : IState
{
    private StateController _state;
    private GameModel _gameModel;

    public DieEnemy(StateController state)
    {
        _state = state;
        _gameModel = ServiceLocator.ServiceLocator.Resolve<GameModel>();
    }
    

    public void DoIt()
    {
        throw new System.NotImplementedException();
    }
}
