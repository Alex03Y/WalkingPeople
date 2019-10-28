using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFriend : IState
{
    private StateController _unit;
    private GameModel _gameModel;

    public KillFriend(StateController unit)
    {
        _unit = unit;
        _gameModel = ServiceLocator.ServiceLocator.Resolve<GameModel>();

    }
    public void DoIt()
    {
        throw new System.NotImplementedException();
    }
}
