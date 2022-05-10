using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class C_IsHurry : C_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        if (m_sharedPlayerVariables.Value.m_hurry)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}

