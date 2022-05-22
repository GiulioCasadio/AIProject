using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Coach;
using UnityEngine;

public class ManOnBall : CoachBaseConditional
{
    public override TaskStatus OnUpdate()
    {
        foreach (CoachPlayerCommunication cpc in m_sharedCoachVariables.Value.playersCommunications)
        {
            if (checkPlayerBehindBall(cpc))
            {
                if(checkManNearBall(cpc))
                    return TaskStatus.Success;
            }
        }
        return TaskStatus.Failure;
    }
}
