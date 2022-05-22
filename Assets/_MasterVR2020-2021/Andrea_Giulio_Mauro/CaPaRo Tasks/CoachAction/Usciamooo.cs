using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.Player;
using Coach;
using UnityEngine;

public class Usciamooo : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        CoachPlayerCommunication ballCarrier = GetMostFreePlayerNearBall();
        
        ballCarrier.SetState(PlayerFocus.PlayerStateFocus.BOOTBALL, true);
        
        return TaskStatus.Success;
    }
}
