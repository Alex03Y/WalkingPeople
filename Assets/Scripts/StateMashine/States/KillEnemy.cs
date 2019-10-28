using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : IState
{
    private StateController _unit;
    private GameModel _gameModel;
    public KillEnemy(StateController unit)
    {
        _unit = unit;
        _gameModel = ServiceLocator.ServiceLocator.Resolve<GameModel>();
    }
    public void DoIt()
    {
        throw new System.NotImplementedException();
    }
}
