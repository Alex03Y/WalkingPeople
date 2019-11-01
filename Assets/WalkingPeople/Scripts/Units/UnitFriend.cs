using WalkingPeople.Scripts.StatesLogic;

namespace WalkingPeople.Scripts.Units
{
    public class UnitFriend : UnitEnemyDiagonal
    {
        protected override void Update()
        {
            switch (_currentState)
            {
                case State.Move :
                    MoveToDirection();
                    break;
                
                case State.OnClick :
                    _gameModel.EndGame(false);
                    _gameModel.SetChanged();
                    OnDisposeObject();
                    break;
                
                case State.OutOfScreen :
                    _gameModel.AddScore();
                    OnDisposeObject();
                    break;
            }
        }
    }
}
