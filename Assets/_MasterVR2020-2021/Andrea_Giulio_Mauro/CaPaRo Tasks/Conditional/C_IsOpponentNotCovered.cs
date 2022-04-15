using UnityEngine;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

using Ca_Pa_Ro;
using Ca_Pa_Ro.CaPaRo_SharedVariables;

public class C_IsOpponentNotCovered : Conditional
{
    protected Task m_task;

    public GameObject self;

   // public AIInputData shared;

    protected Behavior m_owner => m_task.Owner;

    public override void OnAwake()
    {
        m_task = this;
    }

    public override void OnStart()
    {
        self = m_owner.GetVariable("Self").GetValue() as GameObject;
    }

    public override TaskStatus OnUpdate()
	{
        //SharedAIInputData input = (SharedAIInputData)m_owner.GetVariable("DIO");
        // Transform opponent = GetOpponentNearestTo(input, m_owner.GetVariable("m_opponents"));
        return TaskStatus.Failure;
	}

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
}

