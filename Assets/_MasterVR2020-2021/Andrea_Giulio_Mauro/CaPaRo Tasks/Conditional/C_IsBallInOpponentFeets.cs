using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class C_IsBallInOpponentFeets : C_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        if (IsBallInOpponentFeets(m_sharedPlayerVariables.Value.m_targetPosition))
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
