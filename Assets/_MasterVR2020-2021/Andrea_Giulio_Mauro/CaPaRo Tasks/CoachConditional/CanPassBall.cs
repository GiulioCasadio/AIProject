using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Coach;
using UnityEngine;

public class CanPassBall : CoachBaseConditional
{
    public override TaskStatus OnUpdate()
    {
        foreach (CoachPlayerCommunication player in m_sharedCoachVariables.Value.playersCommunications)
        {
            if (checkPlayerBeyondBall(player) && !checkManNearBall(player))
            {
                if(BallCanReachFriend(player))
                {
                    m_sharedCoachVariables.Value.playerToPassBall = player;
                    return TaskStatus.Success;
                }
            }
        }
        return TaskStatus.Failure;
    }
}
