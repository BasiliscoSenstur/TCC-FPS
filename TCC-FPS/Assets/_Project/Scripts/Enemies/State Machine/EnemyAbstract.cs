using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyAbstract
{
    public abstract void EnterState(EnemyController enemy);
    public abstract void LogicsUpdate(EnemyController enemy);
    public abstract void ExitState(EnemyController enemy);
}
