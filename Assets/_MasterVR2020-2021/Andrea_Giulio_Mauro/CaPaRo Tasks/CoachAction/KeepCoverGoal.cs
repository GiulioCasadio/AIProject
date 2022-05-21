using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coach;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.Player;


[TaskCategory("Coach")]
public class KeepCoverGoal : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        int playerCoveringGoal = 0;

        foreach (CoachPlayerCommunication cpc  in m_sharedCoachVariables.Value.playersCommunications)
        {
            if (cpc.m_playerFocus.m_state == PlayerFocus.PlayerStateFocus.COVERGOAL)
            {
                cpc.m_playerFocus.m_hurry = false;
                playerCoveringGoal++;
            }
        }

        if(playerCoveringGoal > 1)
            return TaskStatus.Success;

        if (playerCoveringGoal == 1 && m_sharedCoachVariables.Value.m_behavior != CoachVariables.TeamBehavior.DEFENSIVE)
            return TaskStatus.Success;

        CoachPlayerCommunication mostNearGoalPlayer = GetMostFreePlayerNearMyGoal();
        mostNearGoalPlayer.m_playerFocus.m_state = PlayerFocus.PlayerStateFocus.COVERGOAL;
        mostNearGoalPlayer.m_playerFocus.m_hurry = true;

        if (m_sharedCoachVariables.Value.m_behavior == CoachVariables.TeamBehavior.DEFENSIVE)
        {
            float distanceMyGoalOpponentsCenterGravity =  Mathf.Abs(m_sharedCoachVariables.Value.OpponentTeamCenterGravity.x - shared.Value.myGoal.position.x); 
            if (distanceMyGoalOpponentsCenterGravity < shared.Value.halfFieldWidth)
            {
                CoachPlayerCommunication otherMostNearGoalPlayer = GetMostFreePlayerNearMyGoal();
                if(otherMostNearGoalPlayer != null)
                {
                    otherMostNearGoalPlayer.m_playerFocus.m_state = PlayerFocus.PlayerStateFocus.COVERGOAL;
                    otherMostNearGoalPlayer.m_playerFocus.m_hurry = true;
                }
            }
        }
        
        return TaskStatus.Success;
    }
}
