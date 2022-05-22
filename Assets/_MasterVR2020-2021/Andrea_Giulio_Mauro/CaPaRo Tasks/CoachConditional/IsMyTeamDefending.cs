using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Coach;
using UnityEngine;

public class IsMyTeamDefending : CoachBaseConditional
{
    public override TaskStatus OnUpdate()
    {
        if (m_sharedCoachVariables.Value.m_fieldStatus == CoachVariables.TeamFieldStatus.DEFENDING)
            return TaskStatus.Success;
        return TaskStatus.Failure;
    }
}
