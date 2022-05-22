using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.Player;
using Coach;
using UnityEngine;

public class KeepBallAndWait : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        CoachPlayerCommunication ballCarrier = GetMostFreePlayerNearBall();
        if (!MoveForwardPlayer(ballCarrier))
        {
            ballCarrier.SetState(PlayerFocus.PlayerStateFocus.KICKBALL, false);
        }
        return TaskStatus.Success;
    }
}
