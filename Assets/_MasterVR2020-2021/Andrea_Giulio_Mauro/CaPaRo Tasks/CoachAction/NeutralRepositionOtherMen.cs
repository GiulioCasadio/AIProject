using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.Player;
using Coach;
using UnityEngine;

public class NeutralRepositionOtherMen : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        List<CoachPlayerCommunication> freePlayers = GetFreePlayers();

        if (freePlayers.Count == 0)
        {
            return TaskStatus.Success;
        }

        /*switch (m_sharedCoachVariables.Value.m_behavior)
        {
            case CoachVariables.TeamBehavior.NEUTRAL:
                CoachPlayerCommunication lastMan = GetMostFreePlayerNearBall();
                lastMan.m_playerFocus.m_state = PlayerFocus.PlayerStateFocus.KNOCKS;
                
                foreach (CoachPlayerCommunication cpc in freePlayers)
                {
                    
                }
                
        }*/

        return TaskStatus.Success;
    }
}
