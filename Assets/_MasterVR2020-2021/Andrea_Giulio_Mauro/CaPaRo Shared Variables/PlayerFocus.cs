using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Ca_Pa_Ro.CaPaRo_SharedVariables;
using UnityEngine;

public class PlayerFocus
{
    
    public enum PlayerStateFocus { KNOCKS,CHASEBALL,NONE}

    public PlayerStateFocus m_state = PlayerStateFocus.NONE;
    
    [System.Serializable]
    public class SharedPlayerFocus : SharedVariable<PlayerFocus>
    {
        public override string ToString()
        {
            return mValue == null ? "null" : mValue.ToString();
        }

        public static implicit operator SharedPlayerFocus(PlayerFocus value)
        {
            return new SharedPlayerFocus { mValue = value };
        }
    }
}
