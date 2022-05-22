using System;
using UnityEngine;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

using Ca_Pa_Ro.CaPaRo_SharedVariables;
using Ca_Pa_Ro.Player;


public class C_Base : Conditional
{
    #region conditional task setup
    protected Task m_task;
    protected Behavior m_owner => m_task.Owner;

    public SharedAIInputData shared;
    public SharedAIOutputData output;
    public SharedPlayerFocus m_sharedPlayerVariables;

    public Vector2 targetPosition;
    public Vector2 ballPosition;
    public Vector2 myPosition;

    protected float radiusTreshold = 1f;     // treshold usato per vicinanza dalla palla/player/destinazione
    protected float distanceTreshold = 4f;     // distanza dash
    protected float angleTreshold = 2f;        // angolo tiro
    protected float behindBallTreshold = 1f;   // treshold intercettazione

    public override void OnAwake()
    {
        m_task = this;
    }

    public override void OnStart()
    {
        shared = m_owner.GetVariable("Shared") as SharedAIInputData;
        output = m_owner.GetVariable("Output") as SharedAIOutputData;
        m_sharedPlayerVariables = m_owner.GetVariable("m_playerFocus") as SharedPlayerFocus;
    }

    public override TaskStatus OnUpdate()
    {
        targetPosition = m_sharedPlayerVariables.Value.m_targetPosition;
        ballPosition = shared.Value.ballPosition;
        myPosition = shared.Value.myPosition;

        return TaskStatus.Success;
    }
    #endregion

    #region conditional task methods
    public Transform GetOpponentNearestTo(Vector3 position, List<Transform> opponents)
    {
        Transform nearest = null;
        var minDistance = float.MaxValue;

        foreach (var opponent in opponents)
        {
            if (opponent == null) continue;

            Vector2 opponentPosition = opponent.position;
            var toTarget = (Vector2)position - opponentPosition;
            var distance = toTarget.magnitude;

            if (!(distance <= minDistance)) continue;
            nearest = opponent;
            minDistance = distance;
        }

        return nearest;
    }
    protected bool IsCharacterCovered(Vector2 i_Character)
    {
        if (i_Character == null)
        {
            return false;
        }

        if (shared.Value.ball == null || shared.Value.myPosition == null)
        {
            return false;
        }

        Vector2 ballPosition = shared.Value.ballPosition;
        Vector2 myPosition = shared.Value.myPosition;

        return IsInTheMidMidPoint(i_Character, ballPosition, myPosition);
    }

    // PointToCheck sta nel punto medio del punto medio tra A e B? A e' sempre quello a cui deve essere piu' vicino
    public bool IsInTheMidMidPoint(Vector2 pointA, Vector2 pointB, Vector2 pointToCheck)
    {
        Vector2 midPoint = new Vector2((pointA.x + pointB.x) / 2, (pointA.y + pointB.y) / 2);
        Vector2 midMidPoint = new Vector2((pointA.x + midPoint.x) / 2, (pointA.y + midPoint.y) / 2);
        if (Vector2.Distance(pointToCheck, midMidPoint) < radiusTreshold)
            return true;
        return false;
    }

    protected bool IsBallInFeets(Vector2 i_Character)
    {
        if (i_Character == null)
        {
            return false;
        }

        if (shared.Value.ball == null || shared.Value.myPosition == null)
        {
            return false;
        }

        Vector2 ballPosition = shared.Value.ballPosition;

        if (Vector2.Distance(ballPosition, i_Character) < radiusTreshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsReachable(Vector2 pointA, Vector2 pointB)
    {
        // ciclo ogni giocatore (escluso chi ha la palla) 
        foreach (Transform obstacleTransform in shared.Value.m_Opponents)
        {
            if (DistancePtLine(pointA, pointB, obstacleTransform.GetPositionXY()) < behindBallTreshold)
            {
                return false;
            }
        }
        foreach (Transform obstacleTransform in shared.Value.m_Teams)
        {
            if (pointA != obstacleTransform.GetPositionXY() && pointB != obstacleTransform.GetPositionXY() && DistancePtLine(pointA, pointB, obstacleTransform.GetPositionXY()) < behindBallTreshold) // TODO trova un check piu' sicuro sul giocatore
            {
                return false;
            }
        }
        return true;
    }

    float DistancePtLine(Vector2 a, Vector2 b, Vector2 p)
    {
        Vector2 n = b - a;
        Vector2 pa = a - p;
        Vector2 c = n * (Vector2.Dot(pa, n) / Vector2.Dot(n, n));
        Vector2 d = pa - c;
        return Mathf.Sqrt(Vector2.Dot(d, d));
    }
    #endregion
}
