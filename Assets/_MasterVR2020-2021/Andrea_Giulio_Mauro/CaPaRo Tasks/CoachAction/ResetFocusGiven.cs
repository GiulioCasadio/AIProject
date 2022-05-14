using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coach;
using BehaviorDesigner.Runtime.Tasks;


[TaskCategory("Coach")]
public class ResetFocusGiven : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        foreach (CoachPlayerCommunication cpc  in m_sharedCoachVariables.Value.playersCommunications)
        {
            cpc.m_focusGiven = false;
        }
        return TaskStatus.Success;
    }
}
