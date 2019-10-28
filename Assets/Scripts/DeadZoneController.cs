using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    [SerializeField] private EdgeCollider2D ZoneCollider2D;
    [SerializeField] private Camera Camera;
    private GameModel _gameModel;
    

    //Paint a collider for dead zone;
    private void Awake()
    {
        _gameModel = ServiceLocator.ServiceLocator.Resolve<GameModel>();
        GetPointsScreen(out var screenPoints);
        ConversionPointsToWorld(ref screenPoints);
        ZoneCollider2D.points = new Vector2[] {screenPoints[0], screenPoints[1]};
        _gameModel.SetBorder(screenPoints[1].x, screenPoints[2].y);
    }
        
    // If player lose
    private void OnTriggerEnter2D(Collider2D other)
    {
        var unit = other.GetComponent<StateChange>();
        unit.GetDieState();
//        var ball = other.gameObject;
//        ball.SetActive(false);
//        Destroy(ball);

//        GameModel.Instance().EndGame(false);
//        GameModel.Instance().SetChanged();
    }
        
    private void GetPointsScreen(out Vector2[] points)
    {
        points = new Vector2[3];
        points[0] = new Vector2(0, 0); 
        points[1] = new Vector2(Screen.width, 0);
        points[2] = new Vector2(0, Screen.height);
    }

    private void ConversionPointsToWorld(ref Vector2[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = Camera.ScreenToWorldPoint(new Vector3(points[i].x, points[i].y, 0));
        }
    }

}
