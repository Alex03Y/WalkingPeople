using UnityEngine;
using WalkingPeople.Scripts.StatesLogic;

namespace WalkingPeople.Scripts.Controllers
{
    public class DeadZoneController : MonoBehaviour
    {
        // If player missed unit
        private void OnTriggerEnter2D(Collider2D other)
        {
            //// todo: "create dictionary for storing objects and search by hash code for a constant time.
            //// todo: make СacheComponent<> in PoolObj, for opportunity don't used GetComponent<>."
            if (other != null) other.GetComponent<StateController>().OutOfScreen();
        }
    }
}
