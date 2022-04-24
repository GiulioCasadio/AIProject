using UnityEngine;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

using Ca_Pa_Ro.CaPaRo_SharedVariables;

public class C_Base : Conditional
{
    #region conditional task setup
    protected Task m_task;
    protected Behavior m_owner => m_task.Owner;

    public SharedAIInputData shared;

    public override void OnAwake()
    {
        m_task = this;
    }

    public override void OnStart()
    {
        shared = m_owner.GetVariable("Shared") as SharedAIInputData;
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

    protected bool IsCharacterCovered(Transform i_Character)
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
        Vector2 direction = (ballPosition - i_Character.GetPositionXY()).normalized;

        Vector2 targetPosition = FindNearestPointOnLine(ballPosition, direction, myPosition);

        if ((myPosition - targetPosition).magnitude < 0.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector2 FindNearestPointOnLine(Vector2 origin, Vector2 direction, Vector2 point)
    {
        direction.Normalize();
        Vector2 lhs = point - origin;

        float dotP = Vector2.Dot(lhs, direction);
        return origin + direction * dotP;
    }
    #endregion
}
