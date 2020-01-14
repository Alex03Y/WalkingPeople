using System;
using UnityEngine;
using WalkingPeople.Scripts.Core.MVC;
using WalkingPeople.Scripts.Core.Service;

public class DeadZoneCreator : MonoBehaviour
{
    [SerializeField] private EdgeCollider2D _zoneCollider2D;
    [SerializeField] private Vector2 _offsetDeadzone;
    [SerializeField] private Camera _camera;
    
    [SerializeField] private bool ToFitSpriteSize = false;
    [SerializeField] private Transform _sprite;
    

    private GameModel _gameModel;
    private float _indentFromEdges;
    private Func<Vector2[]> _createDeadZone;

    private void Awake()
    {
        _gameModel = ServiceLocator.Resolve<GameModel>();
        _indentFromEdges = _gameModel.IndentFromEdges;
        if (ToFitSpriteSize) _createDeadZone = CreateDeadZoneToFitSprite;
        else _createDeadZone = CreateDeadZoneToFitScreen;
        var points = _createDeadZone?.Invoke();
        _zoneCollider2D.points = new[] {points[0] + _offsetDeadzone, points[1] + _offsetDeadzone};
        _gameModel.SetBorder(points[1].x, points[2].y);
    }
    
    private Vector2[] CreateDeadZoneToFitScreen()
    {
        GetPointsScreen(out var points);
        ConversionPointsToWorld(ref points);
        return points;
    }

    private Vector2[] CreateDeadZoneToFitSprite()
    {
        var spriteSize = _sprite.GetComponent<SpriteRenderer>().sprite.bounds.extents;
        var spriteScale = _sprite.lossyScale;
        var halfWidth = spriteSize.x * spriteScale.x;
        var halfHeight = spriteSize.y * spriteScale.y;
        var rotation = _sprite.rotation;
        
        if (rotation.z > 0)
        {
            var z = Mathf.Abs(rotation.z / 90f);
            Debug.Log("ritation > 0");
            if (z % 2 <= 0.01f)
            {
//                Debug.Log("ritation sdsd");

                var value = halfHeight;
                halfHeight = halfWidth;
                halfWidth = value;
            }
        }
        
        var startPoint = new Vector2(_sprite.position.x, _sprite.position.y);
        GetPointsSprite(out Vector2[] points, startPoint, halfWidth, halfHeight);
        return points;
    }
    
    private void GetPointsScreen(out Vector2[] points)
    {
        points = new Vector2[3];
        points[0] = new Vector2(0, 0f); 
        points[1] = new Vector2(Screen.width, 0f);
        points[2] = new Vector2(0, Screen.height);
    }

    private void GetPointsSprite(out Vector2[] points, Vector2 startPoint, float width, float height)
    {
        points = new Vector2[3];
        points[0] = new Vector2(startPoint.x - width, startPoint.y - height);
        points[1] = new Vector2(startPoint.x + width, startPoint.y - height);
        points[2] = new Vector2(startPoint.x - width, startPoint.y + height);
    }

    private void ConversionPointsToWorld(ref Vector2[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = _camera.ScreenToWorldPoint(new Vector3(points[i].x, points[i].y, 0));
        }
    }
}
