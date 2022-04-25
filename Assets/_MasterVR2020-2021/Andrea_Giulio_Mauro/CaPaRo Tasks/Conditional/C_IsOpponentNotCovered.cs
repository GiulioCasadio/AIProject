using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class C_IsOpponentNotCovered : C_Base
{
    public override TaskStatus OnUpdate()
    {
        Transform opponent = GetOpponentNearestTo(shared.Value.myPosition, shared.Value.m_Opponents);

        if (IsCharacterCovered(opponent))
        {
            //reset axes
            output.Value.axes = new Vector2(0,0);

            m_owner.SetVariableValue("Output", output);
            return TaskStatus.Failure;
        }
        else
        {
            return TaskStatus.Success;
        }
    }
}