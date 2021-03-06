using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coach;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.Player;


[TaskCategory("Coach")]
public class CoverZoneOrFight : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        List<CoachPlayerCommunication> freePlayers = GetFreePlayers();

        if (freePlayers.Count == 0) 
            return TaskStatus.Success;

        switch (m_sharedCoachVariables.Value.m_behavior)
        {
            case CoachVariables.TeamBehavior.NEUTRAL:
                foreach (CoachPlayerCommunication cpc in freePlayers)
                {
                    cpc.SetState(PlayerFocus.PlayerStateFocus.COVERZONE, false, GetMostAdvancedOpponent().GetPositionXY());
                }
                break;
            case CoachVariables.TeamBehavior.DEFENSIVE:
                foreach (CoachPlayerCommunication cpc in freePlayers)
                {
                    if (IsOpponentNearBall())
                    {
                        cpc.SetState(PlayerFocus.PlayerStateFocus.KNOCKS, false, GetMostOpponentNearBall());
                    }

                    else
                    {
                        cpc.SetState(PlayerFocus.PlayerStateFocus.MARK, false, GetMostAdvancedOpponent().GetPositionXY()); //mettiti nella zona dell'avversario
                    }
                    
                }
                break;
            case CoachVariables.TeamBehavior.AGGRESSIVE:
                foreach (CoachPlayerCommunication cpc in freePlayers)
                {
                    cpc.SetState(PlayerFocus.PlayerStateFocus.MAKEFREE, false, new Vector2(shared.Value.myGoal.position.x * -0.75f ,0)); // 3/4 campo avversaria
                }
                break;
        }
        
        return TaskStatus.Success;
    }
}
