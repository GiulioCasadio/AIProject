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
    public SharedAIOutputData output;

    float trashold = 1.5f;

    public override void OnAwake()
    {
        m_task = this;
    }

    public override void OnStart()
    {
        shared = m_owner.GetVariable("Shared") as SharedAIInputData;
        output = m_owner.GetVariable("Output") as SharedAIOutputData;
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
        Vector2 midPoint = new Vector2((pointA.x+pointB.x)/2, (pointA.y+pointB.y)/2);
        if (Vector2.Distance(pointToCheck, midPoint) < trashold)
            return true;
        return false;
    }
    #endregion
}
