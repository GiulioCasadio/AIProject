using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Coach;
using UnityEngine;

public class CanKickToGoal : CoachBaseConditional
{
    public override TaskStatus OnUpdate()
    {
        if(!BallCanReachTarget()) 
            return TaskStatus.Failure;
        
        foreach (CoachPlayerCommunication cpc in m_sharedCoachVariables.Value.playersCommunications)
        {
            if (checkManControlBall(cpc))
            {
                return TaskStatus.Success;
            }
                
        }
        return TaskStatus.Failure;
    }
}
