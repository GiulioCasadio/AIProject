using BehaviorDesigner.Runtime;
using System.Collections.Generic;
using UnityEngine;

namespace Ca_Pa_Ro.CaPaRo_SharedVariables
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
        
        // Seek-and-flee behaviour.

        private float m_MinFleeDistanceFactor = 0.25f;
        private float m_MaxFleeDistanceFactor = 0.50f;

        // Separation.

        private float m_SeparationThreshold = 3f;

        // Energy thresholds.

        private float m_MinDashEnergy = 0.40f;
        private float m_MinKickEnergy = 0.05f;
        private float m_MinTackleEnergy = 0.50f;
        private float m_MinAttractEnergy = 0.10f;

        // Cooldown timers.

        private float m_DashCooldown = 0.50f;
        private float m_KickCooldown = 0.25f;
        private float m_TackleCooldown = 2.0f;
        private float m_AttractCooldown = 0.5f;

        // Dash behaviour.

        private float m_DashDistance = 3.5f;
        private float m_ForcedDashDistance = 2f;

        // Kick behaviour.

        private float m_KickPrecision = 0.1f;

        // Tackle behaviour.

        private float m_TackleRadius = 0.8f;
        private float m_BallDistanceThreshold = 2f;

        // Attract behaviour.

        private float m_AttractMinRadius = 0.70f;
        private float m_AttractMaxRadius = 0.95f;

        private float m_AttractTimeThreshold = 2f;

        // Extra parameters.

        private float m_RecoverRadius = 1.0f;
        private float m_RecoverTimeThreshold = 1.0f;

        private float m_SmoothTime = 0.0f;

        public List<Transform> m_Opponents;
        public List<Transform> m_Teams;
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