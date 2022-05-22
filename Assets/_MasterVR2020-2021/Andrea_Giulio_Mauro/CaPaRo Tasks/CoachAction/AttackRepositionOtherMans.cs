using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.Player;
using Coach;
using UnityEngine;

public class AttackRepositionOtherMans : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        List<CoachPlayerCommunication> freePlayers = GetFreePlayers();

        if (freePlayers.Count == 0) 
            return TaskStatus.Success;
        
        CoachPlayerCommunication lastMan = null;
        switch (m_sharedCoachVariables.Value.m_behavior)
        {
            case CoachVariables.TeamBehavior.NEUTRAL:
                lastMan = GetMostFreePlayerNearMyGoal();
                lastMan.SetState(PlayerFocus.PlayerStateFocus.COVERZONE, false, new Vector2(0,0)); //Copri la metà campo
                freePlayers.Remove(lastMan);
                foreach (CoachPlayerCommunication cpc in freePlayers)
                {
                    if (!MoveForwardPlayer(cpc))
                    {
                        cpc.SetState(PlayerFocus.PlayerStateFocus.KNOCKS, false, GetLastOpponent());
                    }
                }
                break;
            case CoachVariables.TeamBehavior.DEFENSIVE:
                lastMan = GetMostFreePlayerNearMyGoal();
                lastMan.SetState(PlayerFocus.PlayerStateFocus.COVERZONE, false, new Vector2(shared.Value.myGoal.position.x * 0.75f,0)); //Copri la 1/4 campo
                foreach (CoachPlayerCommunication cpc in freePlayers)
                {
                    cpc.SetState(PlayerFocus.PlayerStateFocus.MARK, false, GetMostAdvancedOpponent());
                }
                break;
            case CoachVariables.TeamBehavior.AGGRESSIVE:
                foreach (CoachPlayerCommunication cpc in freePlayers)
                {
                    if (!MoveForwardPlayer(cpc))
                    {
                        cpc.SetState(PlayerFocus.PlayerStateFocus.KNOCKS, false, GetLastOpponent());
                    }
                }
                break;
        }
        
        return TaskStatus.Success;
    }
        
}
