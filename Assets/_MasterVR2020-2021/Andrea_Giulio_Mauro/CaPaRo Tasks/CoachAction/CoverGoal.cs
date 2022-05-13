using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.Player;
using UnityEngine;
using Coach;


[TaskCategory("Coach")]
public class CoverGoal : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        GetMostPlayerNearMyGoal().m_playerFocus.m_state = PlayerFocus.PlayerStateFocus.COVERZONE;
        
        return TaskStatus.Success;
    }
}
