using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.CaPaRo_SharedVariables;
using UnityEngine;

[TaskCategory("StatusCheck")]
public class HaveToKnocks : Conditional
{
    [SerializeField]
    private PlayerFocus.SharedPlayerFocus m_playerFocus;
    public override TaskStatus OnUpdate()
    {
        if (m_playerFocus.Value.m_state == PlayerFocus.PlayerStateFocus.KNOCKS)
            return TaskStatus.Success;
        return TaskStatus.Failure;
    }
}
