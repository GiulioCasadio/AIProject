using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Coach;
using UnityEngine;

public class UpdateMyTeamScore : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        m_sharedCoachVariables.Value.myTeamScore++;
        
        return TaskStatus.Success;
    }
}
