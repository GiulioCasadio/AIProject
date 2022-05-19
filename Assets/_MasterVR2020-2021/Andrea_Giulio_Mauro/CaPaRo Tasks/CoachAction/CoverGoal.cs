using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.Player;
using UnityEngine;
using Coach;


[TaskCategory("Coach")]
public class CoverGoal : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        CoachPlayerCommunication mostNearGoalPlayer = GetMostFreePlayerNearMyGoal();
        mostNearGoalPlayer.m_playerFocus.m_state = PlayerFocus.PlayerStateFocus.COVERGOAL;
        mostNearGoalPlayer.m_playerFocus.m_hurry = true;
        mostNearGoalPlayer.m_focusGiven = true;
        

        if (m_sharedCoachVariables.Value.m_behavior == CoachVariables.TeamBehavior.DEFENSIVE || m_sharedCoachVariables.Value.m_behavior == CoachVariables.TeamBehavior.NEUTRAL)
        {
            float distanceMyGoalOpponentsCenterGravity =  Mathf.Abs(m_sharedCoachVariables.Value.OpponentTeamCenterGravity.x - shared.Value.myGoal.position.x);
            if (distanceMyGoalOpponentsCenterGravity < shared.Value.halfFieldWidth)
            {
                CoachPlayerCommunication otherMostNearGoalPlayer = GetMostFreePlayerNearMyGoal();
                if(otherMostNearGoalPlayer != null)
                {
                    otherMostNearGoalPlayer.m_playerFocus.m_state = PlayerFocus.PlayerStateFocus.COVERGOAL;
                    otherMostNearGoalPlayer.m_playerFocus.m_hurry = true;
                    otherMostNearGoalPlayer.m_focusGiven = true;
                }
            }
        }
        
        return TaskStatus.Success;
    }
}
