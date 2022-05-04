using System.Collections;
using System.Collections.Generic;
using Ca_Pa_Ro.CaPaRo_SharedVariables;
using UnityEngine;

using Ca_Pa_Ro.Player;

namespace Coach
{
    public class CoachPlayerCommunication
    {
        protected internal PlayerFocus m_playerFocus;
        protected internal AIInputData m_sharedInput;
        protected internal bool m_focusGiven;

        public CoachPlayerCommunication(PlayerFocus i_playerFocus, AIInputData i_sharedInput)
        {
            this.m_playerFocus = i_playerFocus;
            this.m_sharedInput = i_sharedInput;
            this.m_focusGiven = false;
        }
    }
}


