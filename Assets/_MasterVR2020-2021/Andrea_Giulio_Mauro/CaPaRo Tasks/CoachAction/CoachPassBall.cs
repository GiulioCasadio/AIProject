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
        CoachPlayerCommunication ballCarrier = GetMostFreePlayerNearBall();

        CoachPlayerCommunication playerToPass = m_sharedCoachVariables.Value.playerToPassBall;
        
        ballCarrier.SetState(PlayerFocus.PlayerStateFocus.PASSBALL, false, playerToPass.m_sharedInput.myTransform);

        m_sharedCoachVariables.Value.playerToPassBall.SetState(PlayerFocus.PlayerStateFocus.MAKEFREE, false, playerToPass.m_sharedInput.myPosition);
        
        return TaskStatus.Success;
    }
}
