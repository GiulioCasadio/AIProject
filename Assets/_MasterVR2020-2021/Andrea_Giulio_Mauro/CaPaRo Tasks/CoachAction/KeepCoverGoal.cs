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
                cpc.m_focusGiven = true;
                playerCoveringGoal++;
            }
        }

        if(playerCoveringGoal > 1)
            return TaskStatus.Success;

        if (playerCoveringGoal == 1 && m_sharedCoachVariables.Value.m_behavior != CoachVariables.TeamBehavior.DEFENSIVE)
            return TaskStatus.Success;

        
        
        return TaskStatus.Success;
    }
}
