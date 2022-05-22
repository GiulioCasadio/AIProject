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

        public float ballRadiusNearTreshold;

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

        public const float m_MinFleeDistanceFactor = 0.25f;
        public const float m_MaxFleeDistanceFactor = 0.50f;

        // Separation.

        public const float m_SeparationThreshold = 3f;

        // Energy thresholds.

        public const float m_MinDashEnergy = 0.40f;
        public const float m_MinKickEnergy = 0.05f;
        public const float m_MinTackleEnergy = 0.50f;
        public const float m_MinAttractEnergy = 0.10f;
        public const float m_RecoveryRate = 0.0825f;

        // Cooldown timers.

        public const float m_DashCooldown = 0.50f;
        public const float m_KickCooldown = 0.25f;
        public const float m_TackleCooldown = 2.0f;
        public const float m_AttractCooldown = 0.5f;

        // Dash behaviour.

        public const float m_DashDistance = 3.5f;
        public const float m_ForcedDashDistance = 2f;

        // Kick behaviour.

        public const float m_KickPrecision = 0.1f;

        // Tackle behaviour.

        public const float m_TackleRadius = 0.8f;
        public const float m_BallDistanceThreshold = 2f;

        // Attract behaviour.

        public const float m_AttractMinRadius = 0.70f;
        public const float m_AttractMaxRadius = 0.95f;

        public const float m_AttractTimeThreshold = 2f;

        // Extra parameters.

        public const float m_RecoverRadius = 1.0f;
        public const float m_RecoverTimeThreshold = 1.0f;

        public const float m_SmoothTime = 0.0f;

        public List<Transform> m_Opponents;
        public List<Transform> m_Teams;

        public tnBaseMatchController m_tnBaseMatchController;

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