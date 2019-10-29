using UnityEngine;

namespace WalkingPeople.Scripts.Units.States
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
