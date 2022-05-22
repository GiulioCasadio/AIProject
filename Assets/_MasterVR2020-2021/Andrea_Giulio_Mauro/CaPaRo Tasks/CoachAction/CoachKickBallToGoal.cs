using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.Player;
using UnityEngine;
using Coach;


public class CoachKickBallToGoal : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        CoachPlayerCommunication player = GetMostFreePlayerNearBall();

        player.SetState(PlayerFocus.PlayerStateFocus.KICKBALL, true);
        return TaskStatus.Success;
    }
}
