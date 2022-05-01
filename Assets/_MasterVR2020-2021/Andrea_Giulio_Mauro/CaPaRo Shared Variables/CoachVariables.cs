using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Coach
{
    [System.Serializable]
    public class CoachVariables
    {
        public enum TeamBehavior { AGGRESSIVE,DEFENSIVE,NEUTRAL}

        public TeamBehavior m_behavior = TeamBehavior.NEUTRAL;
        
        public int myTeamScore = 0;
        public int otherTeamScore = 0;
        
        public List<CoachPlayerCommunication> playersCommunications = new List<CoachPlayerCommunication>();

        public Vector2 MyTeamCenterGravity = new Vector2();
        public Vector2 OtherTeamCenterGravity = new Vector2();
        
        


    }
    
    [System.Serializable]
    public class SharedCoachVariables : SharedVariable<CoachVariables>
    {
        public override string ToString()
        {
            return mValue == null ? "null" : mValue.ToString();
        }

        public static implicit operator SharedCoachVariables(CoachVariables value)
        {
            return new SharedCoachVariables { mValue = value };
        }

    }
}