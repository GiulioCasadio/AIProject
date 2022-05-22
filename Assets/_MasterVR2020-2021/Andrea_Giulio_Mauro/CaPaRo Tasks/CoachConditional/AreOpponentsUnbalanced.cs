using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class AreOpponentsUnbalanced : CoachBaseConditional
{
    public override TaskStatus OnUpdate()
    {
        if(m_sharedCoachVariables.Value.OpponentTeamCenterGravity.x * shared.Value.opponentGoal.position.x < 0) //altra squadra ha il baricentro molto alto
            return TaskStatus.Success;
        return TaskStatus.Failure;
    }
}
