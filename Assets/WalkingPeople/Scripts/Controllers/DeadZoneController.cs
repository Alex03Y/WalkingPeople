using UnityEngine;
using WalkingPeople.Scripts.Core.MVC;
using WalkingPeople.Scripts.Core.Service;
using WalkingPeople.Scripts.StatesLogic;

namespace WalkingPeople.Scripts.Controllers
{
    public class DeadZoneController : MonoBehaviour
    {
        [SerializeField] private EdgeCollider2D ZoneCollider2D;
        [SerializeField] private Camera Camera;
        
        private GameModel _gameModel;
        private float _scatter;
        
        //Paint a collider for dead zone;
        private void Awake()
        {
            _gameModel = ServiceLocator.Resolve<GameModel>();
            _scatter = _gameModel.Scater;
            
            GetPointsScreen(out var screenPoints);
            ConversionPointsToWorld(ref screenPoints);
            var offset = new Vector2(0f, -2 - _scatter);
            ZoneCollider2D.points = new[] {screenPoints[0] + offset, screenPoints[1] + offset};
            _gameModel.SetBorder(screenPoints[1].x, screenPoints[2].y);
        }
        
        // If player missed unit
        private void OnTriggerEnter2D(Collider2D other)
        {
            //// todo: "create dictionary for storing objects and search by hash code for a constant time.
            //// todo: make СacheComponent<> in PoolObj, for opportunity don't used GetComponent<>."
            if (other != null) other.GetComponent<StateController>().OutOfScreen();
        }
        
        private void GetPointsScreen(out Vector2[] points)
        {
            points = new Vector2[3];
            points[0] = new Vector2(0, 0f); 
            points[1] = new Vector2(Screen.width, 0f);
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
}
