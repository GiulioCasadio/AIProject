using UnityEngine;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

using Ca_Pa_Ro.CaPaRo_SharedVariables;

namespace Coach
{
    public class CoachBaseTask : Action
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
    }
}


