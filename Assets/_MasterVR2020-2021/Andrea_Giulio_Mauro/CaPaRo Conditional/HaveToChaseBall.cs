using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

using Ca_Pa_Ro.Player;

[TaskCategory("StatusCheck")]
public class HaveToChaseBall : Conditional
{
    [SerializeField]
    private SharedPlayerFocus m_playerFocus;
    
    public override TaskStatus OnUpdate()
    {
        if (m_playerFocus.Value.m_state == PlayerFocus.PlayerStateFocus.CHASEBALL)
            return TaskStatus.Success;
        return TaskStatus.Failure;
    }
}
