using System.Collections;
using System.Collections.Generic;
using Ca_Pa_Ro.CaPaRo_SharedVariables;
using UnityEngine;

using Ca_Pa_Ro.Player;

namespace Coach
{
    public class CoachPlayerCommunication
    {
        private PlayerFocus playerFocus;
        protected internal PlayerFocus m_playerFocus
        {
            get { return playerFocus; }
            set
            {
                m_focusGiven = true;
                playerFocus = value;
            }
        }
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

        public CoachPlayerCommunication(PlayerFocus i_playerFocus, AIInputData i_sharedInput)
        {
            this.m_playerFocus = i_playerFocus;
            this.m_sharedInput = i_sharedInput;
            this.m_focusGiven = false;
        }
    }
}


