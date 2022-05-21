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

        player.m_playerFocus.m_state = PlayerFocus.PlayerStateFocus.KICKBALL;
        player.m_playerFocus.m_hurry = true;

        return TaskStatus.Success;
    }
}
