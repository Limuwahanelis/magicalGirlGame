using UnityEngine;

public abstract class PlayerStateMain
{
    protected InputCommand _inputCommand;
    public abstract void Update();
    public virtual void FixedUpdate() { }
    public virtual void Move(Vector2 direction) { }
    public virtual void Jump() { }
    public virtual void Attack(PlayerCombat.AttackModifiers attackModifier = PlayerCombat.AttackModifiers.NONE) { }
    public virtual void Push(PushInfo pushInfo) { /*ChangeState(PlayerPushedState.StateType);*/}
    public abstract void InterruptState();

    public virtual void UndoComand() { }
    public void SetInputCommand(ref InputCommand command)
    {
        _inputCommand = command;
    }
    protected bool PerformInputCommand()
    {
        if (_inputCommand == null) return false;
        _inputCommand.Execute();
        _inputCommand = null;
        return true;
    }
}
