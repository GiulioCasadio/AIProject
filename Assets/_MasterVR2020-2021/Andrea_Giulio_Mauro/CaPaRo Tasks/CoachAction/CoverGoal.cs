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
        mostNearGoalPlayer.SetState(PlayerFocus.PlayerStateFocus.COVERGOAL, true);


        if (m_sharedCoachVariables.Value.m_behavior == CoachVariables.TeamBehavior.DEFENSIVE)
        {
            float distanceMyGoalOpponentsCenterGravity =  Mathf.Abs(m_sharedCoachVariables.Value.OpponentTeamCenterGravity.x - shared.Value.myGoal.position.x);
            if (distanceMyGoalOpponentsCenterGravity < shared.Value.halfFieldWidth)
            {
                CoachPlayerCommunication otherMostNearGoalPlayer = GetMostFreePlayerNearMyGoal();
                if(otherMostNearGoalPlayer != null)
                {
                    otherMostNearGoalPlayer.SetState(PlayerFocus.PlayerStateFocus.COVERGOAL, true);
                }
            }
        }
        
        return TaskStatus.Success;
    }
}
