using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.Player;
using Coach;
using UnityEngine;

public class AttackSendManOnBall : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        CoachPlayerCommunication mostNearBall = GetMostFreePlayerNearBall();
        
        mostNearBall.SetState(PlayerFocus.PlayerStateFocus.CHASEBALL, true);

        if (m_sharedCoachVariables.Value.m_behavior == CoachVariables.TeamBehavior.AGGRESSIVE)
        {
            Transform mostNearBallOpponent = GetMostOpponentNearBall();
            CoachPlayerCommunication mostPlayerNearOpponentBall = GetMostFreePlayerNearOpponent(mostNearBallOpponent);

            if (mostPlayerNearOpponentBall != null)
            {
                mostPlayerNearOpponentBall.SetState(PlayerFocus.PlayerStateFocus.KNOCKS, true, mostNearBallOpponent);
            }
        }

        return TaskStatus.Success;
    }
}
