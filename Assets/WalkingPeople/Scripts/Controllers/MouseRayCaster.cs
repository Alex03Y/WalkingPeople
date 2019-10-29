using UnityEngine;
using UnityEngine.EventSystems;
using WalkingPeople.Scripts.StatesLogic;

namespace WalkingPeople.Scripts.Controllers
{
    public class MouseRayCaster : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _layer;


        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                var worldPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
                var hitInfo = Physics2D.Raycast(worldPoint, Vector2.zero, 2f, _layer);

                if (hitInfo)
                {
                    hitInfo.collider.GetComponent<StateChange>().OnClick();
                }
            }

//            if (Input.GetMouseButtonDown(0))
//            {
//                var worldPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
//                if(Physics2D.Raycast(worldPoint, Vector2.zero, 2f, _layer)){}
//            }
        }

        
            
    }
}
