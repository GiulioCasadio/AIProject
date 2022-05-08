using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class C_IsThrowPossible : C_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        return TaskStatus.Failure;

    }
}

