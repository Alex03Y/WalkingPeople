using System;
using UnityEngine;
using WalkingPeople.Scripts.Core.MVC;
using WalkingPeople.Scripts.Core.Service;

namespace WalkingPeople.Scripts.Utilits
{
    public class BorderCreator : MonoBehaviour
    {
        [SerializeField] private EdgeCollider2D EdgeCollider;

        [Header("Size in pixel.")] 
        [SerializeField] private float Indent;
        [SerializeField] private int _widthScreenDefault = 1080, _heightScreenDefault = 1920;
        [SerializeField] private Vector2 _offset;

        [Header("Select reference object")] [Tooltip("The reference point when you create a frame-collider.")] 
        [SerializeField] private bool ScreenSize;
        [SerializeField] private Camera MainCamera;
        
        [Tooltip("The reference point when you create a frame-collider.")] 
        [SerializeField] private bool BackGroundSize;
        [SerializeField] private Transform Sprite;

        
        private GameModel _gameModel;
        private float _indent = 0f;
        private Action _createBorder;
        
        /*
         Resize a border depending on the aspect ratio.
         And paint a collider. 
        */

        private void Awake()
        {
            _gameModel = ServiceLocator.Resolve<GameModel>();
            _gameModel.SetIndentFromEdges(Indent);

            if (ScreenSize)
                _createBorder = CreateBorderToFitScreen;
            else 
            if (BackGroundSize)
                _createBorder = CreateBorderToFitSprite;
        }
        
        private void Start()
        {
            if(Mathf.Abs(Indent) >= 0.0001f)
                _indent = Screen.width <= _widthScreenDefault 
                    ? Screen.width * (Indent / _widthScreenDefault) 
                    : Screen.height * (Indent / _heightScreenDefault);
            _createBorder?.Invoke();
        }

        private void CreateBorderToFitScreen()
        {
            GetScreenPointsForBorder(out var screenPoints, Screen.width, Screen.height);
            ConvertScreenPointsToWorldPoints(ref screenPoints, MainCamera);
            EdgeCollider.points = screenPoints;
        }

        private void CreateBorderToFitSprite()
        {
            float screenWidth = Screen.width - _indent * 2f;
            screenWidth /= 2f;
            var scatter = MainCamera.ScreenToWorldPoint(new Vector3(Mathf.Abs(screenWidth), 0f, 0f)).x; 
            var sizeSprite = Sprite.GetComponent<SpriteRenderer>().sprite.bounds.size;
            var scaleSprite = Sprite.lossyScale;
            var width = sizeSprite.x * scaleSprite.x;
            var height = sizeSprite.y * scaleSprite.y;
            var startPoint = new Vector2(Sprite.position.x - width/2, Sprite.position.y - height/2);
            
            GetSpritePointsForBorder(out Vector2[] spritePoints, startPoint, width, height, scatter);
            EdgeCollider.points = spritePoints;
        }
        
        private void GetScreenPointsForBorder(out Vector2[] points, float width, float height)
        {
            points = new Vector2[5];
            points[0] = new Vector2(_indent, _indent);
            points[1] = new Vector2(_indent, height - _indent);
            points[2] = new Vector2(width - _indent, height - _indent);
            points[3] = new Vector2(width - _indent, _indent);
            points[4] = points[0]; 
        }

        private void GetSpritePointsForBorder(out Vector2[] points, Vector2 startPoint, float width, float height, float scatter)
        {
            points = new Vector2[5];
            points[0] = new Vector2(startPoint.x - scatter, startPoint.y - scatter);
            points[1] = new Vector2(points[0].x, startPoint.y + height + scatter);
            points[2] = new Vector2(startPoint.x + width + scatter, points[1].y);
            points[3] = new Vector2(points[2].x, points[0].y);
            points[4] = points[0];
        }
        
        private void ConvertScreenPointsToWorldPoints(ref Vector2[] screenPoints, Camera mainCamera)
        {
            for (int i = 0; i < screenPoints.Length; i++)
            {
                screenPoints[i] = mainCamera.ScreenToWorldPoint(screenPoints[i]);
            }
        }
    }
}