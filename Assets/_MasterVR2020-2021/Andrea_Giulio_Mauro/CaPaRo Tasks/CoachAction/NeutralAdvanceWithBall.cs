using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Coach;
using UnityEngine;

public class NeutralAdvanceWithBall : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
