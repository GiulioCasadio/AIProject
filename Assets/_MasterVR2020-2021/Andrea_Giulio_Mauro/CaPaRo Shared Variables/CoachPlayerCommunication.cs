using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ca_Pa_Ro.Player;

namespace Coach
{
    public class CoachPlayerCommunication
    {
        protected internal PlayerFocus m_playerFocus;
        protected internal bool m_focusGiven;

        public CoachPlayerCommunication(PlayerFocus i_playerFocus)
        {
            this.m_playerFocus = i_playerFocus;
            this.m_focusGiven = false;
        }
    }
}


