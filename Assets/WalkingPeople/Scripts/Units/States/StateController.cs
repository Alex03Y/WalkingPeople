using UnityEngine;

namespace WalkingPeople.Scripts.Units.States
{
   public abstract class StateController : MonoBehaviour
   {
      protected State _state;
      public virtual void Life()
      {
         _state = State.Move;
      }

      public virtual void OnClick()
      {
         _state = State.OnClick;
      }

      public virtual void OutOfScreen()
      {
         _state = State.OutOfScreen;
      }
   }
}
