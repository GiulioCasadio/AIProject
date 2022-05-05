using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Coach;
using UnityEngine;

public class IsMyTeamNeutral : CoachBaseConditional
{
    public override TaskStatus OnUpdate()
    {
        if (m_sharedCoachVariables.Value.m_fieldStatus == CoachVariables.TeamFieldStatus.NEUTRAL)
            return TaskStatus.Success;
        return TaskStatus.Failure;
    }
}
