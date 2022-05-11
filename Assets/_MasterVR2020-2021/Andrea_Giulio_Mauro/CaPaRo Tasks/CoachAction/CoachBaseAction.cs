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

        protected CoachPlayerCommunication GetMostPlayerNearMyGoal()
        {
            List<CoachPlayerCommunication> players = m_sharedCoachVariables.Value.playersCommunications;

            Vector2 myGoalPosition = shared.Value.myGoal.position;
            CoachPlayerCommunication nearest = players[0];
            float currentRecord = Vector2.Distance(myGoalPosition, nearest.m_sharedInput.myPosition);
            
            for(int i = 0; i < players.Count; ++i)
            {
                CoachPlayerCommunication currentPlayer = players[i];

                float distanceGoalCurrentPlayer =
                    Vector2.Distance(myGoalPosition, currentPlayer.m_sharedInput.myPosition);

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


