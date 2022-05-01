using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Coach;
using UnityEngine;


public class UpdateOtherTeamScore : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        m_sharedCoachVariables.Value.otherTeamScore++;
        
        return TaskStatus.Success;
    }
}
