using System;
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

        return IsBetween(ballPosition, i_Character.position, myPosition);
    }

    public bool IsBetween(Vector2 pointA, Vector2 pointB, Vector2 pointToCheck)
    {
        /*float crossproduct = (pointToCheck.y - pointA.y) * (pointB.x - pointA.x) - (pointToCheck.x - pointA.x) * (pointB.y - pointA.y);

        // allignment
        if (Math.Abs(crossproduct) > Math.E)
            return false;

        float dotproduct = (pointToCheck.x - pointA.x) * (pointB.x - pointA.x) + (pointToCheck.y - pointA.y) * (pointB.y - pointA.y);
        if (dotproduct < 0)
            return false;

        float squaredlengthba = (pointB.x - pointA.x) * (pointB.x - pointA.x) + (pointB.y - pointA.y) * (pointB.y - pointA.y);
        if (dotproduct > squaredlengthba)
            return false;

        return true;*/

        Vector2 midPoint = new Vector2((pointA.x+pointB.x)/2, (pointA.y+pointB.y)/2);
        if(pointToCheck==midPoint)
            return true;
        return false;
    }
    #endregion
}
