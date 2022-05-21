using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.Player;
using Coach;
using UnityEngine;

public class NeutralSendManOnBall : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        CoachPlayerCommunication nearestPlayerToEnemyWithBall = GetMostFreePlayerNearBall();
        nearestPlayerToEnemyWithBall.m_playerFocus.m_state = PlayerFocus.PlayerStateFocus.CHASEBALL;
        nearestPlayerToEnemyWithBall.m_playerFocus.m_hurry = true;

        return TaskStatus.Success;
    }
}
