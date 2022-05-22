using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

using Ca_Pa_Ro.CaPaRo_SharedVariables;
using Ca_Pa_Ro.Player;

namespace Coach
{
    [TaskCategory("Coach")]
    public class CoachBaseAction : Action
    {
        public SharedAIInputData shared;
        public SharedCoachVariables m_sharedCoachVariables;
    
        protected Task m_task;
        protected Behavior m_owner => m_task.Owner;
    
    
        public override void OnAwake()
        {
            m_task = this;
        }
    
        public override void OnStart()
        {
            shared = m_owner.GetVariable("Shared") as SharedAIInputData;
            m_sharedCoachVariables = m_owner.GetVariable("m_coachVariables") as SharedCoachVariables;
        }

        protected CoachPlayerCommunication GetMostFreePlayerNearMyGoal()
        {
            return GetMostFreePlayerNearTarget(shared.Value.myGoal.position);
        }

        protected CoachPlayerCommunication GetMostFreePlayerNearBall()
        {
            return GetMostFreePlayerNearTarget(shared.Value.ballPosition);
        }
        
        protected CoachPlayerCommunication GetMostFreePlayerNearOpponent(Transform opponent)
        {
            return GetMostFreePlayerNearTarget(opponent.GetPositionXY());
        }
        
        private CoachPlayerCommunication GetMostFreePlayerNearTarget(Vector2 i_targetPosition)
        {
            List<CoachPlayerCommunication> players = m_sharedCoachVariables.Value.playersCommunications;
            
            CoachPlayerCommunication nearest = null;
            float currentRecord = float.MaxValue;
            
            for(int i = 0; i < players.Count; ++i)
            {
                CoachPlayerCommunication currentPlayer = players[i];

                if (currentPlayer.m_focusGiven)
                    continue;

                float distanceGoalCurrentPlayer = Vector2.Distance(i_targetPosition, currentPlayer.m_sharedInput.myPosition);

                if (distanceGoalCurrentPlayer < currentRecord)
                {
                    currentRecord = distanceGoalCurrentPlayer;
                    nearest = currentPlayer;
                }
            }
            return nearest;
        }

        protected int CountFreePlayers()
        {
            int freePlayers = 0;

            foreach (CoachPlayerCommunication cpc in m_sharedCoachVariables.Value.playersCommunications)
            {
                if (!cpc.m_focusGiven)
                    freePlayers++;
            }
            return freePlayers;
        }


        protected List<CoachPlayerCommunication> GetFreePlayers()
        {
            return m_sharedCoachVariables.Value.playersCommunications.Where(x => !x.m_focusGiven).ToList();
        }

        protected bool IsOpponentNearBall()
        {
            foreach (Transform opponent in shared.Value.m_Opponents)
            {
                if (Vector2.Distance(opponent.GetPositionXY(), shared.Value.ballPosition) < shared.Value.ballRadiusNearTreshold)
                    return true;
            }
            return false;
        }
        
        protected bool IsOpponentNearBall(Vector2 opponent)
        {
            return Vector2.Distance(opponent, shared.Value.ballPosition) < shared.Value.ballRadiusNearTreshold;
        }
        
        private Transform GetMostOpponentNearTarget(Vector2 target)
        {
            Transform output = null;
            float record = float.MaxValue;
            foreach (Transform opponent in shared.Value.m_Opponents)
            {
                if (Vector2.Distance(opponent.GetPositionXY(), shared.Value.ballPosition) < record)
                    output = opponent;
            }
            return output;
        }
        
        protected Transform GetMostOpponentNearBall()
        {
            return GetMostOpponentNearTarget(shared.Value.ballPosition);
        }

        protected Transform GetMostAdvancedOpponent()
        {
            return GetMostOpponentNearTarget(shared.Value.myGoal.GetPositionXY());
        }
        
        protected Transform GetLastOpponent()
        {
            return GetMostOpponentNearTarget(shared.Value.opponentGoal.GetPositionXY());
        }
        
        protected bool checkManControlBall(CoachPlayerCommunication cpc)
        {
            return checkManControlBall(cpc.m_sharedInput.myPosition);
        }
        protected bool checkManControlBall(Vector2 player)
        {
            return Vector2.Distance(player, shared.Value.ballPosition) < shared.Value.ballRadiusNearTreshold;
        }
        
        protected Vector2 GetPlayerForwardPosition(CoachPlayerCommunication cpc)
        {
            FieldZoneCache cache = m_sharedCoachVariables.Value.FieldZoneCache;

            if (!cache.CanPlayerGoForward(cpc.m_sharedInput.myPosition, true))
                return Vector2.negativeInfinity;
            
            int xCounter = 1;
            while (xCounter < 3)
            {
                for (int counterY = 1; counterY < 4; counterY++)
                {
                    int xOffset = xCounter * (int)Mathf.Sign(shared.Value.opponentGoal.position.x); // verso porta avversaria
                    int yOffset = (counterY % 3) - 1;
                    FieldZoneCache.FieldZoneStatus fieldZoneStatus = cache.GetPlayerFieldZoneStatusOffset(cpc.m_sharedInput.myPosition, xOffset, yOffset);
                    if (fieldZoneStatus == FieldZoneCache.FieldZoneStatus.FREE)
                    {
                        return cache.GetCenterCellWithRandom(cpc.m_sharedInput.myPosition, xOffset, yOffset);
                    }
                }
                xCounter++;
            }
            return Vector2.negativeInfinity;
        }
    }
}


