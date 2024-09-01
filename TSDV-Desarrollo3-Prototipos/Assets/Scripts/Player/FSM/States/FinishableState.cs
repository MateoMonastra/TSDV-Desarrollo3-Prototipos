using System;

namespace Player.FSM.States
{
    public abstract class FinishableState : State
    {
    public event Action OnFinish;
    
    public virtual void Finish()
        => OnFinish.Invoke();
    }
}