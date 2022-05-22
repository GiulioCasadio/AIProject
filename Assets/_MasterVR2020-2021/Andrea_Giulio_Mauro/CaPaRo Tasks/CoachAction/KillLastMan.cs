using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.Player;
using Coach;
using UnityEngine;

public class KillLastMan : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        Transform lastMan = GetLastOpponent();
        CoachPlayerCommunication kira = GetMostFreePlayerNearOpponent(lastMan);
        if (kira != null)
        {
            GetMostFreePlayerNearOpponent(lastMan).SetState(PlayerFocus.PlayerStateFocus.KNOCKS, true, lastMan);
        }
        return TaskStatus.Success;
    }
}
