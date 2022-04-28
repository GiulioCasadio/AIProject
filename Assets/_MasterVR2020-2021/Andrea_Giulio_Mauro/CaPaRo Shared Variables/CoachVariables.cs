using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Coach
{
    public class CoachVariables
    {
        public List<CoachPlayerCommunication> playersCommunications = new List<CoachPlayerCommunication>();
        // Start is called before the first frame update

        [System.Serializable]
        public class SharedCoachVariables : SharedVariable<CoachVariables>
        {
            public override string ToString()
            {
                return mValue == null ? "null" : mValue.ToString();
            }

            public static implicit operator CoachVariables.SharedCoachVariables(CoachVariables value)
            {
                return new CoachVariables.SharedCoachVariables { mValue = value };
            }

        }
    }
}