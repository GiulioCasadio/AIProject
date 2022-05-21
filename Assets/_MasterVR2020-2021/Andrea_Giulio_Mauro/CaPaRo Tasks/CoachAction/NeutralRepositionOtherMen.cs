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

                foreach (CoachPlayerCommunication cpc in freePlayers)
                {
                    cpc.m_playerFocus.m_state = PlayerFocus.PlayerStateFocus.MAKEFREE;
                    //setta posizione dopo il merge
                    cpc.m_playerFocus.m_hurry = false;
                }
                break;
            case CoachVariables.TeamBehavior.DEFENSIVE:
                CoachPlayerCommunication lastMan = GetMostFreePlayerNearMyGoal();
                lastMan.m_playerFocus.m_state = PlayerFocus.PlayerStateFocus.COVERGOAL;
                lastMan.m_playerFocus.m_hurry = false;

                foreach (CoachPlayerCommunication cpc in freePlayers)
                {
                        cpc.m_playerFocus.m_state = PlayerFocus.PlayerStateFocus.KNOCKS;
                        cpc.m_playerFocus.m_targetTransform = GetMostOpponentNearBall();
                }
                break;
            case CoachVariables.TeamBehavior.AGGRESSIVE:
                foreach (CoachPlayerCommunication cpc in freePlayers)
                {
                    //todo
                }
                break;
        }
        return TaskStatus.Success;
    }
}
