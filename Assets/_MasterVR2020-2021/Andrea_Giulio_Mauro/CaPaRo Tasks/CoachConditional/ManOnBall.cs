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
                float distanceBall = Vector2.Distance(cpc.m_sharedInput.myPosition, shared.Value.ballPosition);
                
                if(distanceBall <= shared.Value.ballRadius * 3)
                    return TaskStatus.Success;
            }
        }
        return TaskStatus.Failure;
    }
}
