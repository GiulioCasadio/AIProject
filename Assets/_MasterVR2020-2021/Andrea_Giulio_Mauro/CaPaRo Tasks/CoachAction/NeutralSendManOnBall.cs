using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.Player;
using Coach;
using UnityEngine;

public class NeutralSendManOnBall : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        CoachPlayerCommunication nearestPlayerToEnemyWithBall = GetMostFreePlayerNearBall();
        nearestPlayerToEnemyWithBall.SetState(PlayerFocus.PlayerStateFocus.CHASEBALL, true);

        if (m_sharedCoachVariables.Value.m_behavior == CoachVariables.TeamBehavior.AGGRESSIVE)
        {
            CoachPlayerCommunication secondNearestPlayerToEnemyWithBall = GetMostFreePlayerNearBall();
            secondNearestPlayerToEnemyWithBall.SetState(PlayerFocus.PlayerStateFocus.CHASEBALL, false);
        }
        
        return TaskStatus.Success;
    }
}
