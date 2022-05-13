using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class C_IsCoveringPosition : C_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        if (Vector2.Distance(targetPosition, myPosition) < radiusTrashold)
        {
            return TaskStatus.Failure;
        }

        return TaskStatus.Success;

    }
}
