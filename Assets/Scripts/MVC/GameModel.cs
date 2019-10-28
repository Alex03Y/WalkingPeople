using System;
using UnityEngine;

public class GameModel : Observer
{
    [RuntimeInitializeOnLoadMethod]
    public static void Register()
    {
        ServiceLocator.ServiceLocator.Register(typeof(GameModel), new GameModel());
    }

    public enum GameEndResult
    {
        NotEnded,
        Winner,
        Looser
    }

    public GameEndResult GameEnd { get; private set; }

    public void EndGame(bool winner)
    {
        GameEnd = winner ? GameEndResult.Winner : GameEndResult.Looser;
    }
    
    public int CountUnits { get; private set; }

    public void SetCountUnits(int count)
    {
        if (count == 0) throw  new NotImplementedException("[Game Model] Counts of unit can't be zero'");
        CountUnits = count;
    }
    
    public int Score { get; private set; }

    public void AddScore()
    {
        Score++;
    }
    
    public float RightBorder { get; private set; }
    public float TopBorder { get; private set; }

    public void SetBorder(float right, float top)
    {
        RightBorder = right;
        TopBorder = top;
    }
    
}
