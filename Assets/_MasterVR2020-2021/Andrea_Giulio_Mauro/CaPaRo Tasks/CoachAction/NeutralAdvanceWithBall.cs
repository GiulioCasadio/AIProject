using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.Player;
using Coach;
using UnityEngine;

public class NeutralAdvanceWithBall : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        CoachPlayerCommunication nearestPlayerToBall = GetMostFreePlayerNearBall();

        MoveForwardPlayer(nearestPlayerToBall);
        
        return TaskStatus.Success;
    }
}
