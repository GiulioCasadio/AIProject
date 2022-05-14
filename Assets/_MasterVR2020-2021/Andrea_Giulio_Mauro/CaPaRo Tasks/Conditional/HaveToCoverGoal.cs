using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.Player;
using UnityEngine;


[TaskCategory("StatusCheck")]
public class HaveToCoverGoal : Conditional
{
    [SerializeField]
    private SharedPlayerFocus m_playerFocus;
    public override TaskStatus OnUpdate()
    {
        if (m_playerFocus.Value.m_state == PlayerFocus.PlayerStateFocus.COVERGOAL)
            return TaskStatus.Success;
        return TaskStatus.Failure;
    }
}
