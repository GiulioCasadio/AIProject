using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Coach;
using UnityEngine;

public class MyBallCarrierFree : CoachBaseConditional
{
    public override TaskStatus OnUpdate()
    {
        CoachPlayerCommunication ballCarrier = GetMostFriendNearBall();

        if (playerIsFarFromOpponents(ballCarrier))
        {
            return TaskStatus.Success;
        }
        
        return TaskStatus.Failure;
    }
}
