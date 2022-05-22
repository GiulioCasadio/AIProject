using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

using Ca_Pa_Ro.Player;

[TaskCategory("StatusCheck")]
public class HaveToKnocks : Conditional
{
    [SerializeField]
    private SharedPlayerFocus m_playerFocus;
    public override TaskStatus OnUpdate()
    {
        if (m_playerFocus.Value.m_state == PlayerFocus.PlayerStateFocus.KNOCKS)
            return TaskStatus.Success;
        return TaskStatus.Failure;
    }
}
