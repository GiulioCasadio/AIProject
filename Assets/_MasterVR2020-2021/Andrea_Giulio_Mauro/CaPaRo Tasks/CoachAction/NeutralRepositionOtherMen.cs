using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.Player;
using Coach;
using UnityEngine;

public class NeutralRepositionOtherMen : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        List<CoachPlayerCommunication> freePlayers = GetFreePlayers();

        if (freePlayers.Count == 0)
        {
            return TaskStatus.Success;
        }

        switch (m_sharedCoachVariables.Value.m_behavior)
        {
            case CoachVariables.TeamBehavior.NEUTRAL:
                CoachPlayerCommunication mostFreePlayerNearMyGoal = GetMostFreePlayerNearMyGoal();
                mostFreePlayerNearMyGoal.SetState(PlayerFocus.PlayerStateFocus.COVERZONE, false, new Vector2(shared.Value.myGoal.position.x * 0.75f, 0));

                freePlayers.Remove(mostFreePlayerNearMyGoal);
                
                foreach (CoachPlayerCommunication cpc in freePlayers)
                {
                    Vector2 playerForwardPosition = GetPlayerForwardPosition(cpc);
                    if (!playerForwardPosition.Equals(Vector2.negativeInfinity))
                    {
                        cpc.SetState(PlayerFocus.PlayerStateFocus.MAKEFREE, false, playerForwardPosition);
                    }
                    else
                    {
                        cpc.SetState(PlayerFocus.PlayerStateFocus.KNOCKS, false, GetLastOpponent());
                    }
                }
                break;
            case CoachVariables.TeamBehavior.DEFENSIVE:
                CoachPlayerCommunication lastMan = GetMostFreePlayerNearMyGoal();
                lastMan.SetState(PlayerFocus.PlayerStateFocus.COVERGOAL, false);
                freePlayers.Remove(lastMan);
                foreach (CoachPlayerCommunication cpc in freePlayers)
                {
                    cpc.SetState(PlayerFocus.PlayerStateFocus.KNOCKS, false, GetMostOpponentNearBall());
                }
                break;
            case CoachVariables.TeamBehavior.AGGRESSIVE:
                foreach (CoachPlayerCommunication cpc in freePlayers)
                {
                    Vector2 playerForwardPosition = GetPlayerForwardPosition(cpc);
                    if (!playerForwardPosition.Equals(Vector2.negativeInfinity))
                    {
                        cpc.SetState(PlayerFocus.PlayerStateFocus.MAKEFREE, false, playerForwardPosition);
                    }
                    else
                    {
                        cpc.SetState(PlayerFocus.PlayerStateFocus.KNOCKS, false, GetLastOpponent());
                    }
                }
                break;
        }
        return TaskStatus.Success;
    }
}
