using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.CaPaRo_SharedVariables;
using UnityEngine;

using Ca_Pa_Ro.Player;

[TaskCategory("StatusCheck")]
public class HaveToMakeFree : Conditional
{
    [SerializeField]
    private SharedPlayerFocus m_playerFocus;
    public override TaskStatus OnUpdate()
    {
        if (m_playerFocus.Value.m_state == PlayerFocus.PlayerStateFocus.MAKEFREE)
            return TaskStatus.Success;
        return TaskStatus.Failure;
    }
}