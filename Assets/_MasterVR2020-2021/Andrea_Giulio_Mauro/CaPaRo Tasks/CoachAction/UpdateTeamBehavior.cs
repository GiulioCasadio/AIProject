using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Coach;
using UnityEngine;


public class UpdateTeamBehavior : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        int myTeamScore = m_sharedCoachVariables.Value.myTeamScore;
        int otherTeamScore = m_sharedCoachVariables.Value.otherTeamScore;

        float remaningTime = shared.Value.m_tnBaseMatchController.remainingTime.AsFloat();
        

        int golDifference = myTeamScore - otherTeamScore;

        if (golDifference >= 2) // il 2 è parametrizzabile
        {
            m_sharedCoachVariables.Value.m_behavior = CoachVariables.TeamBehavior.DEFENSIVE;
            return TaskStatus.Success;
        }

        if (golDifference <= -2)
        {
            m_sharedCoachVariables.Value.m_behavior = CoachVariables.TeamBehavior.AGGRESSIVE;
            return TaskStatus.Success;
        }

        if (golDifference == 0)
        {
            m_sharedCoachVariables.Value.m_behavior = CoachVariables.TeamBehavior.NEUTRAL;
            return TaskStatus.Success;
        }

        if (golDifference == -1)
        {
            if (remaningTime < 30)
            {
                m_sharedCoachVariables.Value.m_behavior = CoachVariables.TeamBehavior.AGGRESSIVE;
            }
            else
            {
                m_sharedCoachVariables.Value.m_behavior = CoachVariables.TeamBehavior.NEUTRAL;
            }
            return TaskStatus.Success;
        }
        
        if (golDifference == 1)
        {
            if (remaningTime < 30)
            {
                m_sharedCoachVariables.Value.m_behavior = CoachVariables.TeamBehavior.DEFENSIVE;
            }
            else
            {
                m_sharedCoachVariables.Value.m_behavior = CoachVariables.TeamBehavior.NEUTRAL;
            }
            return TaskStatus.Success;
        }
        
        return TaskStatus.Success;
    }
}
