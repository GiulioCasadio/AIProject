﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coach;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.Player;


[TaskCategory("Coach")]
public class KeepManOnBall : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        int playersOnBall = 0;

        foreach (CoachPlayerCommunication cpc  in m_sharedCoachVariables.Value.playersCommunications)
        {
            if (cpc.m_playerFocus.m_state == PlayerFocus.PlayerStateFocus.CHASEBALL)
            {
                cpc.m_focusGiven = true;
                playersOnBall++;
            }
        }
        
        if(playersOnBall > 1)
            return TaskStatus.Success;

        if (playersOnBall == 1 && m_sharedCoachVariables.Value.m_behavior != CoachVariables.TeamBehavior.DEFENSIVE)
            return TaskStatus.Success;
        
        CoachPlayerCommunication nearestPlayerBall = GetMostFreePlayerNearBall();

        nearestPlayerBall.m_playerFocus.m_state = PlayerFocus.PlayerStateFocus.CHASEBALL;
        nearestPlayerBall.m_focusGiven = true;
        nearestPlayerBall.m_playerFocus.m_hurry = true;

        if (m_sharedCoachVariables.Value.m_behavior == CoachVariables.TeamBehavior.DEFENSIVE)
        {
            float distanceMyGoalOpponentsCenterGravity =  Mathf.Abs(m_sharedCoachVariables.Value.OpponentTeamCenterGravity.x - shared.Value.myGoal.position.x);
            if (distanceMyGoalOpponentsCenterGravity > shared.Value.fieldWidth * 0.8) //Valore molto arbitrario
            {
                //Se la squadra avversaria ha un baricentro molto basso, aggrediamoli!
                
                CoachPlayerCommunication nearestOtherPlayerBall = GetMostFreePlayerNearBall();

                nearestOtherPlayerBall.m_playerFocus.m_state = PlayerFocus.PlayerStateFocus.CHASEBALL;
                nearestOtherPlayerBall.m_focusGiven = true;
                nearestOtherPlayerBall.m_playerFocus.m_hurry = true;
            }
        }
        
        return TaskStatus.Success;
    }
}
