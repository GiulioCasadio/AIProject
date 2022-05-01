using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class C_IsOpponentNotCovered : C_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        //Transform opponent = GetOpponentNearestTo(shared.Value.myPosition, shared.Value.m_Opponents);

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