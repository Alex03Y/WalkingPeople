using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieFriend : IState
{
    private StateController _state;
    private GameModel _gameModel;

    public DieFriend(StateController state)
    {
        _state = state;
        _gameModel = ServiceLocator.ServiceLocator.Resolve<GameModel>();
    }

    public void DoIt()
    {
        throw new System.NotImplementedException();
    }
}
