using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class C_IsOpponentNotCovered : C_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        if (IsCharacterCovered(m_sharedPlayerVariables.Value.m_targetPosition))
        {
            return TaskStatus.Failure;
        }
        else
        {
            return TaskStatus.Success;
        }
    }
}