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

        switch (m_sharedCoachVariables.Value.m_behavior)
        {
            case CoachVariables.TeamBehavior.NEUTRAL:
                CoachPlayerCommunication lastMan = GetMostFreePlayerNearMyGoal();
                lastMan.SetState(PlayerFocus.PlayerStateFocus.COVERZONE, false, new Vector2(0,0));

                foreach (CoachPlayerCommunication cpc in freePlayers)
                {
                    cpc.SetState(PlayerFocus.PlayerStateFocus.MAKEFREE, false, GetMostAdvancedOpponent().GetPositionXY());
                }
                break;
            case CoachVariables.TeamBehavior.DEFENSIVE:
                foreach (CoachPlayerCommunication cpc in freePlayers)
                {
                    if (IsOpponentNearBall(shared.Value.ballRadius * 2))
                    {
                        cpc.SetState(PlayerFocus.PlayerStateFocus.KNOCKS, false, GetMostOpponentNearBall());
                    }

                    else
                    {
                        cpc.SetState(PlayerFocus.PlayerStateFocus.MARK, false,
                            GetMostAdvancedOpponent().GetPositionXY()); //mettiti nella zona dell'avversario
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
