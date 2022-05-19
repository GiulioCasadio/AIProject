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
        public enum TeamFieldStatus {DEFENDING, NEUTRAL, ATTACKING}

        public TeamBehavior m_behavior = TeamBehavior.NEUTRAL;
        public TeamFieldStatus m_fieldStatus = TeamFieldStatus.NEUTRAL;
        
        public int myTeamScore = 0;
        public int opponentTeamScore = 0;
        
        public List<CoachPlayerCommunication> playersCommunications = new List<CoachPlayerCommunication>();

        public Vector2 MyTeamCenterGravity = new Vector2();
        public Vector2 OpponentTeamCenterGravity = new Vector2();

        public FieldZoneCache FieldZoneCache;

        public void initializeFieldZoneCache(float fieldWidth, float fieldHeight)
        {
            FieldZoneCache = new FieldZoneCache(fieldWidth, fieldHeight);
        }


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