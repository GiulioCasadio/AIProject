using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Ca_Pa_Ro.CaPaRo_SharedVariables
{
    [System.Serializable]
    public class AIOutputData
    {
        public Vector2 axes;
        public bool requestKick;
        public bool requestDash;
        public bool requestAttracting;
    }

    [System.Serializable]
    public class SharedAIOutputData : SharedVariable<AIOutputData>
    {
        public override string ToString()
        {
            return mValue == null ? "null" : mValue.ToString();
        }

        public static implicit operator SharedAIOutputData(AIOutputData value)
        {
            return new SharedAIOutputData { mValue = value };
        }
    }
}