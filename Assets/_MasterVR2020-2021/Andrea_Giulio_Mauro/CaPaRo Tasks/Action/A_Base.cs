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

    protected const float angleTreshold = 20f;        // angolo tiro
    protected const float behindBallTreshold = 0.2f;   // treshold intercettazione

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
        if (m_sharedPlayerVariables.Value.m_hurry && (myPos - targetPosition).magnitude > AIInputData.m_DashDistance)
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
        // ciclo ogni giocatore (escluso chi ha la palla) 
        foreach(Transform obstacleTransform in shared.Value.m_Opponents)
        {
            if(Mathf.Abs(DistancePtLine(pointA, pointB, obstacleTransform.GetPositionXY()))<behindBallTreshold)
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
