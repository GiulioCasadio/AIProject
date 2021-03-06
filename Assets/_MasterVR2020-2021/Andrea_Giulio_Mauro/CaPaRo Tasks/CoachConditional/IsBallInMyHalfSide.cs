using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class IsBallInMyHalfSide : CoachBaseConditional
{
    public override TaskStatus OnUpdate()
    {
        if (shared.Value.ballPosition.x * shared.Value.myGoal.transform.position.x > 0)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}