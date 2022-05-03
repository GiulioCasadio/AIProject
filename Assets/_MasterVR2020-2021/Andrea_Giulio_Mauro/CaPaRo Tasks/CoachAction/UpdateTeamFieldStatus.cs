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
        Vector2 opponentGoalPosition = shared.Value.opponentGoal.position;
        
        float myGoalGravityXDistance = Mathf.Abs(opponentGoalPosition.x - myTeamGravityCenter.x); //distanza dal mio baricentro alla porta avversaria
        float opponentGoalGravityXDistance = Mathf.Abs(myGoalPosition.x - opponentGravityCenter.x);
        
        float myGoalBallXDistance = Mathf.Abs(myGoalPosition.x - ballPosition.x);
        float opponentGoalBallXDistance = Mathf.Abs(opponentGoalPosition.x - ballPosition.x);

        float fieldWidth = shared.Value.fieldWidth;
        
        float ballRadius = shared.Value.ballRadius;

        float ballDistanceNearTreshold = ballRadius * 3;
        
        int myPlayersNearBall = 0;

        foreach (CoachPlayerCommunication player in m_sharedCoachVariables.Value.playersCommunications)
        {
            if (player.m_sharedInput.ballDistance < ballDistanceNearTreshold)
            {
                myPlayersNearBall++;
            }
        }

        int opponentsNearBall = 0;
        
        foreach (Transform opponent in shared.Value.m_Opponents)
        {
            float distanceBall = Vector2.Distance(opponent.position, ballPosition);
            if (distanceBall < ballDistanceNearTreshold)
            {
                opponentsNearBall++;
            }
            
        }

        /*
         * I valori di questo algoritmo sono arbitrari, sviluppando un sistema di raccolta dati si potrebbe tranquillamente utilizzare
         * un banale algoritmo di machine learning come il decision tree per autotunnare questi valori 
         */
        
        
        //check se stiamo attaccando
        float myScore = 0f;

        myScore += myPlayersNearBall >= 1 ? 12 : 0;

        myScore += 10 - (opponentGoalBallXDistance / fieldWidth * 10);
        
        myScore += 8 - (myGoalGravityXDistance / fieldWidth * 8);

        myScore += opponentGoalGravityXDistance / fieldWidth * 8;

        if (myScore > 25)
        {
            m_sharedCoachVariables.Value.m_fieldStatus = CoachVariables.TeamFieldStatus.ATTACKING;
            return TaskStatus.Success;
        }

        float opponentScore = 0f;
        
        opponentScore += opponentsNearBall >= 1 ? 12 : 0;
        
        opponentScore += 10 - (myGoalBallXDistance / fieldWidth * 10);
        
        opponentScore += 8 - (opponentGoalGravityXDistance / fieldWidth * 8);

        opponentScore += myGoalGravityXDistance / fieldWidth * 8;
        
        if (opponentScore > 25)
        {
            m_sharedCoachVariables.Value.m_fieldStatus = CoachVariables.TeamFieldStatus.DEFENDING;
            return TaskStatus.Success;
        }
        
        
        m_sharedCoachVariables.Value.m_fieldStatus = CoachVariables.TeamFieldStatus.NEUTRAL;
        return TaskStatus.Success;
    }
}
