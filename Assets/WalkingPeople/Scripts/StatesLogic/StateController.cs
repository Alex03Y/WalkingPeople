using UnityEngine;

namespace WalkingPeople.Scripts.StatesLogic
{
   public class StateController : MonoBehaviour
   {
      private protected State _currentState;
      public void Life()
      {
         _currentState = State.Move;
      }

      public void OnClick()
      {
         _currentState = State.OnClick;
      }

      public void OutOfScreen()
      {
         _currentState = State.OutOfScreen;
      }
   }
}
