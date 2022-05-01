using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Ca_Pa_Ro.Player
{
    [System.Serializable]
    public class PlayerFocus
    {
        public enum PlayerStateFocus { KNOCKS, CHASEBALL, NONE }

        public PlayerStateFocus m_state = PlayerStateFocus.NONE;
        public bool m_hurry = false;
        public Vector2 m_targetPosition = new Vector2();
        public float m_rangeTargetPosition = 0f;

        
    }

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
