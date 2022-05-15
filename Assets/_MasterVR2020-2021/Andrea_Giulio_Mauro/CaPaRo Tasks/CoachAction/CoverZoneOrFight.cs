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
                    cpc.m_playerFocus.m_state = PlayerFocus.PlayerStateFocus.COVERZONE;
                    cpc.m_playerFocus.m_targetPosition = GetMostAdvancedOpponent().GetPositionXY();
                    cpc.m_playerFocus.m_hurry = false;
                }
                break;
            case CoachVariables.TeamBehavior.DEFENSIVE:
                foreach (CoachPlayerCommunication cpc in freePlayers)
                {
                    if (IsOpponentNearBall(shared.Value.ballRadius * 2))
                    {
                        cpc.m_playerFocus.m_state = PlayerFocus.PlayerStateFocus.KNOCKS;
                        cpc.m_playerFocus.m_targetOpponent = GetMostOpponentNearBall();
                    }

                    else
                    {
                        cpc.m_playerFocus.m_state = PlayerFocus.PlayerStateFocus.COVERZONE;
                        cpc.m_playerFocus.m_targetPosition = GetMostAdvancedOpponent().GetPositionXY(); //mettiti nella zona dell'avversario
                    }
                    
                    cpc.m_playerFocus.m_hurry = false;
                }
                break;
            case CoachVariables.TeamBehavior.AGGRESSIVE:
                foreach (CoachPlayerCommunication cpc in freePlayers)
                {
                    cpc.m_playerFocus.m_state = PlayerFocus.PlayerStateFocus.COVERZONE;
                    cpc.m_playerFocus.m_hurry = false;
                    cpc.m_playerFocus.m_targetPosition = new Vector2(shared.Value.myGoal.position.x * -0.75f ,0); // 3/4 campo avversaria
                }
                break;
        }
        
        return TaskStatus.Success;
    }
}
