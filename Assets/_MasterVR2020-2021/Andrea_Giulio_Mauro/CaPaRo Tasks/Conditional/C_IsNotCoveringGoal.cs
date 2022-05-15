using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class C_IsNotCoveringGoal : C_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();
        if (!IsCoveringView(ballPosition, targetPosition, myPosition))
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
