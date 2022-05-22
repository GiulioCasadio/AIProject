using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.CaPaRo_SharedVariables;
using Ca_Pa_Ro.Player;
using UnityEngine;

public class HaveToBoot : Conditional
{
    [SerializeField]
    private SharedPlayerFocus m_playerFocus;
    public override TaskStatus OnUpdate()
    {
        if (m_playerFocus.Value.m_state == PlayerFocus.PlayerStateFocus.BOOTBALL)
            return TaskStatus.Success;
        return TaskStatus.Failure;
    }
}
