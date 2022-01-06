using System;
using BehaviorDesigner.Runtime;
using System.Collections.Generic;
using UnityEngine;

namespace Ca_Pa.SharedVariables
{
    [System.Serializable]
    public class AIInputData
    {
        //Fields
        public Vector2 myPosition;

        public Vector2 referencePosition;

        public bool initialized;
        
        public Transform ball;
        public float ballDistance;
        public float ballRadius;
        public Vector2 ballPosition;

        public int teamCharactersCount;
        public int teammatesCount;
        public int opponentsCount;

        public Transform myGoal;
        public Transform opponentGoal;

        public Vector2 topLeft;
        public Vector2 bottomRight;
        public Vector2 topRight;
        public Vector2 bottomLeft;
        public Vector2 midfield;
        
        public float fieldWidth;
        public float fieldHeight;
        public float halfFieldWidth;
        public float halfFieldHeight;

        public float gkAreaMinHeight;
        public float gkAreaMaxHeight;
        public float gkAreaWidth;
        public float gkAreaHeight;

        public float goalMinHeight;
        public float goalMaxHeight;
        public float goalWidth;
        public float colliderRadius;
    }

    [System.Serializable]
    public class SharedAIInputData : SharedVariable<AIInputData>
    {
        public override string ToString()
        {
            return mValue == null ? "null" : mValue.ToString();
        }

        public static implicit operator SharedAIInputData(AIInputData value)
        {
            return new SharedAIInputData { mValue = value };
        }
    }
}