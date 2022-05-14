using UnityEngine;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

using Ca_Pa_Ro.CaPaRo_SharedVariables;

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
    }
}


