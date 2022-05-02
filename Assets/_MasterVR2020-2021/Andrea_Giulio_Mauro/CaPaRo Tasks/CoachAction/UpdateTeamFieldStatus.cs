using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Coach;
using UnityEngine;

public class UpdateTeamFieldStatus : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        Vector2 myTeamGravityCenter = m_sharedCoachVariables.Value.MyTeamCenterGravity;
        Vector2 opponentGravityCenter = m_sharedCoachVariables.Value.OpponentTeamCenterGravity;

        Vector2 ballPosition = shared.Value.ballPosition;

        Vector2 myGoalPosition = shared.Value.myGoal.position;
        
        
        
        return TaskStatus.Success;
    }
}
