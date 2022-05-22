using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

using Ca_Pa_Ro.Player;

public class C_IsGoingToKick : C_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        if (m_sharedPlayerVariables.Value.m_state == PlayerFocus.PlayerStateFocus.KICKBALL)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}

