using UnityEngine;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

using Ca_Pa_Ro.CaPaRo_SharedVariables;

public class A_MarkOpponent : A_Base
{
    public override TaskStatus OnUpdate()
    {
        Transform opponent = GetOpponentNearestTo(shared.Value.myPosition, shared.Value.m_Opponents);

        Vector2 ballPosition = shared.Value.ballPosition;
        Vector2 myPosition = shared.Value.myPosition;
        Vector2 direction = (ballPosition - opponent.GetPositionXY()).normalized;

       // Vector2 targetPosition = FindNearestPointOnLine(ballPosition, direction, myPosition);

        //go to that position
        output.Value.axes = direction;

        m_owner.SetVariableValue("Output", output);
        return TaskStatus.Success;
    }
}