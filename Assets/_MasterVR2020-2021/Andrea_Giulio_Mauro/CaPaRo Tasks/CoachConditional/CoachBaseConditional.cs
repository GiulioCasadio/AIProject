﻿using UnityEngine;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

using Ca_Pa_Ro.CaPaRo_SharedVariables;
using Coach;

[TaskCategory("CoachConditional")]
public class CoachBaseConditional : Conditional
{
    #region conditional task setup
    protected Task m_task;
    protected Behavior m_owner => m_task.Owner;

    public SharedAIInputData shared;
    public SharedCoachVariables m_sharedCoachVariables;
    
    protected float behindBallTreshold = 1f;   // treshold intercettazione

    public override void OnAwake()
    {
        m_task = this;
    }

    public override void OnStart()
    {
        shared = m_owner.GetVariable("Shared") as SharedAIInputData;
        m_sharedCoachVariables = m_owner.GetVariable("m_coachVariables") as SharedCoachVariables;
    }
    #endregion
    
    protected bool betweenRange(float value, float start, float end)
    {
        float internalStart = start;
        float internalEnd = end;

        if (start > end)
        {
            internalStart = end;
            internalEnd = start;
        }
        
        return value >= internalStart && value <= internalEnd;
    }

    protected bool checkPlayerBehindBall(CoachPlayerCommunication player)
    {
        float extremeBallPoint = shared.Value.ballPosition.x + shared.Value.ballRadius;
        
        
        bool myGoalLeft = shared.Value.myGoal.position.x < 0;

        if (myGoalLeft)
            extremeBallPoint = shared.Value.ballPosition.x - shared.Value.ballRadius;

        return betweenRange(player.m_sharedInput.myPosition.x, shared.Value.myGoal.position.x, extremeBallPoint);

    }

    protected bool checkManNearBall(CoachPlayerCommunication cpc)
    {
        return checkManNearBall(cpc.m_sharedInput.myPosition);
    }


    protected bool checkManNearBall(Vector2 player)
    {
        return Vector2.Distance(player, shared.Value.ballPosition) < shared.Value.ballRadius * 10;
    }
    
    protected bool checkManControlBall(CoachPlayerCommunication cpc)
    {
        return checkManControlBall(cpc.m_sharedInput.myPosition);
    }
    protected bool checkManControlBall(Vector2 player)
    {
        return Vector2.Distance(player, shared.Value.ballPosition) < shared.Value.ballRadius * 3;
    }


    protected bool BallCanReachTarget()
    {
        float ballGoalDistance = Vector2.Distance(shared.Value.ballPosition, shared.Value.opponentGoal.GetPositionXY());

        if (ballGoalDistance > 4f)
            return false;
        
        bool IsReachable = this.IsReachable(shared.Value.ballPosition, shared.Value.opponentGoal.GetPositionXY());

        if (!IsReachable)
            return false;

        return true;
    }
    
    private bool IsReachable(Vector2 pointA, Vector2 pointB)
    {
        // ciclo ogni giocatore (escluso chi ha la palla) 
        foreach(Transform obstacleTransform in shared.Value.m_Opponents)
        {
            if(DistancePtLine(pointA, pointB, obstacleTransform.GetPositionXY())>behindBallTreshold)
            {
                return false;
            }
        }
        foreach (Transform obstacleTransform in shared.Value.m_Teams)
        {
            if (pointA != obstacleTransform.GetPositionXY() && pointB != obstacleTransform.GetPositionXY() && DistancePtLine(pointA, pointB, obstacleTransform.GetPositionXY()) > behindBallTreshold) // TODO trova un check piu' sicuro sul giocatore
            {
                return false;
            }
        }
        return true;
    }
    
    private float DistancePtLine(Vector2 a, Vector2 b, Vector2 p)
    {
        Vector2 n = b - a;
        Vector2 pa = a - p;
        Vector2 c = n * (Vector2.Dot(pa, n) / Vector2.Dot(n, n));
        Vector2 d = pa - c;
        return Mathf.Sqrt(Vector2.Dot(d, d));
    }
}
