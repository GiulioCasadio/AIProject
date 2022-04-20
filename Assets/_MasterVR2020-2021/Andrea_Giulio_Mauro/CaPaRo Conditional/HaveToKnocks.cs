using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.CaPaRo_SharedVariables;
using UnityEngine;

[TaskCategory("StatusCheck")]
public class HaveToKnocks : Conditional
{
    public PlayerFocus.SharedPlayerFocus focus;
    public override TaskStatus OnUpdate()
    {
        if (focus.Value.m_state == PlayerFocus.PlayerStateFocus.KNOCKS)
            return TaskStatus.Success;
        return TaskStatus.Failure;
    }
}
