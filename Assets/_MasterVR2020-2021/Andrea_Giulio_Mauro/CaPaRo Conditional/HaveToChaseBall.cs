using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("StatusCheck")]
public class HaveToChaseBall : Conditional
{
    public PlayerFocus.SharedPlayerFocus focus;
    
    public override TaskStatus OnUpdate()
    {
        if (focus.Value.m_state == PlayerFocus.PlayerStateFocus.CHASEBALL)
            return TaskStatus.Success;
        return TaskStatus.Failure;
    }
}
