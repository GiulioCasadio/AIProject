using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coach;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.Player;

public class DefenseSendManOnBall : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        CoachPlayerCommunication nearestPlayerBall = GetMostFreePlayerNearBall();

        nearestPlayerBall.SetState(PlayerFocus.PlayerStateFocus.CHASEBALL, true);

        if (m_sharedCoachVariables.Value.m_behavior == CoachVariables.TeamBehavior.DEFENSIVE)
        {
            float distanceMyGoalOpponentsCenterGravity =  Mathf.Abs(m_sharedCoachVariables.Value.OpponentTeamCenterGravity.x - shared.Value.myGoal.position.x);
            if (distanceMyGoalOpponentsCenterGravity > shared.Value.fieldWidth * 0.8) //Valore molto arbitrario
            {
                //Se la squadra avversaria ha un baricentro molto basso, aggrediamoli!
                
                CoachPlayerCommunication nearestOtherPlayerBall = GetMostFreePlayerNearBall();

                nearestOtherPlayerBall.SetState(PlayerFocus.PlayerStateFocus.CHASEBALL, true);
            }
        }
        
        return TaskStatus.Success;
    }
}
