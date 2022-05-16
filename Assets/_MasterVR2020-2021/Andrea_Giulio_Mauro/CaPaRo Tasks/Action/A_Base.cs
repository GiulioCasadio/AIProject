using UnityEngine;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

using Ca_Pa_Ro.CaPaRo_SharedVariables;
using Ca_Pa_Ro.Player;


public class A_Base : Action
{
    #region action task setup
    protected Task m_task;
    protected Behavior m_owner => m_task.Owner;

    public SharedAIInputData shared;
    public SharedAIOutputData output;
    public SharedPlayerFocus m_sharedPlayerVariables;

    public Vector2 targetPosition;
    public Vector2 ballPosition;
    public Vector2 myPosition;
    public Vector2 axes;

    public bool requestKick;

    public float radiusTreshold = 0.5f;     // treshold usato per vicinanza dalla palla/player/destinazione
    public float distanceTreshold = 4f;     // distanza dash
    public float angleTreshold = 2f;        // angolo tiro
    public float behindBallTreshold = 1f;   // treshold intercettazione

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
        axes = output.Value.axes;

        requestKick = output.Value.requestKick;

        return TaskStatus.Success;
    }
    #endregion

    #region action task methods
    public void CheckHurry(Vector2 myPos, Vector2 targetPosition)
    {
        if (m_sharedPlayerVariables.Value.m_hurry && (myPos - targetPosition).magnitude > distanceTreshold)
        {
            output.Value.requestDash = true;
        }
    }

    public Transform GetOpponentNearestTo(Vector2 position, List<Transform> opponents)
    {
        Transform nearest = null;
        var minDistance = float.MaxValue;

        foreach (var opponent in opponents)
        {
            if (opponent == null) continue;

            Vector2 opponentPosition = opponent.position;
            var toTarget = position - opponentPosition;
            var distance = toTarget.magnitude;

            if (!(distance <= minDistance)) continue;
            nearest = opponent;
            minDistance = distance;
        }

        return nearest;
    }

    public bool IsReachable(Vector2 pointA, Vector2 pointB)
    {
        // TODO
        // ciclo ogni giocatore (escluso chi ha la palla) 
        foreach(Transform obstacleTransform in shared.Value.m_Opponents)
        {
            if(IsBetweenPoint(pointA, pointB, obstacleTransform.GetPositionXY()))
            {
                return false;
            }
        }
        foreach (Transform obstacleTransform in shared.Value.m_Teams)
        {
            if (pointA != obstacleTransform.GetPositionXY() && pointB != obstacleTransform.GetPositionXY() && IsBetweenPoint(pointA, pointB, obstacleTransform.GetPositionXY())) // TODO trova un check piu' sicuro sul giocatore
            {
                return false;
            }
        }
        return true;
    }

    public bool IsBetweenPoint(Vector2 pointA, Vector2 pointB, Vector2 pointToCheck) // TODO verificarne il funzionamento
    {
        var line = (pointB - pointA);
        var len = line.magnitude;
        line.Normalize();

        var v = pointToCheck - pointA;
        var d = Vector2.Dot(v, line);
        d = Mathf.Clamp(d, 0f, len);

        if (Vector2.Distance(pointA + line * d, pointToCheck) < radiusTreshold)
        {
            return true;
        }
        return false;
    }
    #endregion
}
