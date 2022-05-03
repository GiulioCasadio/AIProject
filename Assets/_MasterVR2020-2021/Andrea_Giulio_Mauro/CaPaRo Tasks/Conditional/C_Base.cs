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

    public float radiusTrashold = 1.5f;

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
        ResetOutput(output);
        return TaskStatus.Success;
    }

    protected void ResetOutput(SharedAIOutputData tempOutput)
    {
        if (tempOutput == null)
            return;

        tempOutput.Value.axes = new Vector2(0, 0);
        tempOutput.Value.requestKick = false;
        tempOutput.Value.requestDash = false;

        m_owner.SetVariableValue("Output", output);
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

        return IsBetween(ballPosition, i_Character, myPosition);
    }

    // PointToCheck sta nel punto medio tra A e B?
    public bool IsBetween(Vector2 pointA, Vector2 pointB, Vector2 pointToCheck)
    {
        Vector2 midPoint = new Vector2((pointA.x+pointB.x)/2, (pointA.y+pointB.y)/2);
        if (Vector2.Distance(pointToCheck, midPoint) < radiusTrashold)
            return true;
        return false;
    }

    protected bool IsCoveringBall(Transform i_Character)
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

        return IsCoveringView(ballPosition, myPosition, i_Character.position);
    }

    // Tra A e B e' presente C?
    public bool IsCoveringView(Vector2 pointA, Vector2 pointB, Vector2 pointToCheck)
    {
        var line = (pointB - pointA);
        var len = line.magnitude;
        line.Normalize();

        var v = pointToCheck - pointA;
        var d = Vector2.Dot(v, line);
        d = Mathf.Clamp(d, 0f, len);

        if (Vector2.Distance(pointA + line * d, pointToCheck) < radiusTrashold)
        {
            return true;
        }
        return false;
    }

    #endregion
}
