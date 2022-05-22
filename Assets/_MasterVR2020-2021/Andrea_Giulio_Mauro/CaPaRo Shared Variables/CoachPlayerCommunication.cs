using System.Collections;
using System.Collections.Generic;
using Ca_Pa_Ro.CaPaRo_SharedVariables;
using UnityEngine;

using Ca_Pa_Ro.Player;

namespace Coach
{
    public class CoachPlayerCommunication
    {
        private PlayerFocus m_playerFocus;
        
        protected internal AIInputData m_sharedInput;

        private bool focusGiven;
        
        protected internal bool m_focusGiven
        {
            get { return focusGiven; }
            private set { focusGiven = value; }
        }
        protected internal void ResetFocusGiven()
        {
            m_focusGiven = false;
        }

        protected internal void SetState(PlayerFocus.PlayerStateFocus playerFocus, bool isHurry, Transform targetTransform)
        {
            m_playerFocus.m_targetTransform = targetTransform;
            SetState(playerFocus, isHurry);
        }
        protected internal void SetState(PlayerFocus.PlayerStateFocus playerFocus, bool isHurry, Vector2 targetPosition)
        {
            m_playerFocus.m_targetPosition = targetPosition;
            SetState(playerFocus, isHurry);
        }
        
        protected internal void SetState(PlayerFocus.PlayerStateFocus playerFocus, bool isHurry)
        {
            m_playerFocus.m_state = playerFocus;
            m_playerFocus.m_hurry = isHurry;
            m_focusGiven = true;
        }

        protected internal PlayerFocus.PlayerStateFocus GetState()
        {
            return m_playerFocus.m_state;
        }
        
        public CoachPlayerCommunication(PlayerFocus i_playerFocus, AIInputData i_sharedInput)
        {
            this.m_playerFocus = i_playerFocus;
            this.m_sharedInput = i_sharedInput;
            this.m_focusGiven = false;
        }
    }
}


