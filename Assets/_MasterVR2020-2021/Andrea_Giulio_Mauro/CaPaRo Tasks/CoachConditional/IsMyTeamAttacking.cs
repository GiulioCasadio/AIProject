using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Coach;
using UnityEngine;

public class IsMyTeamAttacking : CoachBaseConditional
{
    public override TaskStatus OnUpdate()
    {
        if (m_sharedCoachVariables.Value.m_fieldStatus == CoachVariables.TeamFieldStatus.ATTACKING)
            return TaskStatus.Success;
        return TaskStatus.Failure;
    }
}
