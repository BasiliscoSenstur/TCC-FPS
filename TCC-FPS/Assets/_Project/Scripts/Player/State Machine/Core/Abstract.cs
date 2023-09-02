using UnityEngine;

public abstract class Abstract
{
    public abstract void EnterState(PlayerController player);
    public abstract void LogicsUpdateState(PlayerController player);
    public abstract void PhysicsUpdateState(PlayerController player);
    public abstract void ExitState(PlayerController player);
}
