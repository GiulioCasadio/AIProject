﻿using UnityEngine;
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

    public float radiusTrashold = 1.5f;
    public float distanceTrashold = 5f;
    public float angleTreshold = 2f;

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

        targetPosition = m_sharedPlayerVariables.Value.m_targetPosition;
        ballPosition = shared.Value.ballPosition;
        myPosition = shared.Value.myPosition;

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

    #region action task methods
    public void CheckHurry(Vector2 myPos, Vector2 targetPosition)
    {
        if (m_sharedPlayerVariables.Value.m_hurry && (myPos - targetPosition).magnitude > distanceTrashold)
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
        return true;
    }
    #endregion
}
