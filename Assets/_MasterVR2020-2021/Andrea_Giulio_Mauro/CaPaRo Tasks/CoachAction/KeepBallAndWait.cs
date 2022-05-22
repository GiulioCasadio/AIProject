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

        Vector2 forwardPosition = GetPlayerForwardPosition(ballCarrier);
        if (forwardPosition.Equals(Vector2.negativeInfinity))
        {
            ballCarrier.SetState(PlayerFocus.PlayerStateFocus.KICKBALL, false);
        }

        else
        {
            ballCarrier.SetState(PlayerFocus.PlayerStateFocus.BRINGBALLINX, false, forwardPosition);
        }
        return TaskStatus.Success;
    }
}
