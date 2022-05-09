using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class C_IsThrowPossible : C_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        // calcola direzione tiro
        Vector2 targetDirection = ((myPosition - targetPosition) * -1).normalized;
        Vector2 ballDirection = ((myPosition - ballPosition) * -1).normalized;

        // controlla ostacoli, direzione e se raggiungibile

        return TaskStatus.Failure;

    }
}

