using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Coach;
using UnityEngine;

public class GotTheBall : CoachBaseConditional
{
    public override TaskStatus OnUpdate()
    {
        foreach (CoachPlayerCommunication cpc in m_sharedCoachVariables.Value.playersCommunications)
        {
            if(checkManControlBall(cpc))
                return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
