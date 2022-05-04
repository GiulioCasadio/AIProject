using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Coach;
using UnityEngine;


public class UpdateOpponentTeamScore : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        m_sharedCoachVariables.Value.opponentTeamScore++;
        
        return TaskStatus.Success;
    }
}
