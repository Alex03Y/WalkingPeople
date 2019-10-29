using UnityEngine;

namespace WalkingPeople.Scripts.StatesLogic
{
    public class StateChange : MonoBehaviour
    {
        [SerializeField] private StateController state;

        public void Life()
        {
            state.Life();
        }

        public void OnClick()
        {
            state.OnClick();
        }

        public void OutOfScreen()
        {
            state.OutOfScreen();
        }
    }
}
