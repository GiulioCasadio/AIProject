using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Ca_Pa.CaPa_SharedVariables
{
    [System.Serializable]
    public class AIOutputData
    {
        public Vector2 axes;
        public bool requestKick;
        public bool requestDash;
        public bool isAttracting;
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

    public class AIOutputDataBuilder
    {
        private AIOutputData _data = new AIOutputData();

        public static AIOutputDataBuilder Builder()
        {
            return new AIOutputDataBuilder();
        }

        public AIOutputDataBuilder WithAxes(Vector2 axes)
        {
            _data.axes = axes;
            return this;
        }

        public AIOutputDataBuilder WithKick(bool kick)
        {
            _data.requestKick = kick;
            return this;
        }

        public AIOutputDataBuilder WithDash(bool dash)
        {
            _data.requestDash = dash;
            return this;    
        }

        public AIOutputDataBuilder WithAttract(bool attract)
        {
            _data.isAttracting = attract;
            return this;
        }

        public AIOutputData Build()
        {
            return _data;
        }
    }
}