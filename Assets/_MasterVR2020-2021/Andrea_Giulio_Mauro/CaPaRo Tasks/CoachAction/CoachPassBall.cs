using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.Player;
using Coach;
using UnityEngine;

public class CoachPassBall : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        CoachPlayerCommunication nearestPlayerToBall = GetMostFreePlayerNearBall();
        nearestPlayerToBall.SetState(PlayerFocus.PlayerStateFocus.PASSBALL, false);
        
        return TaskStatus.Success;
    }
}
