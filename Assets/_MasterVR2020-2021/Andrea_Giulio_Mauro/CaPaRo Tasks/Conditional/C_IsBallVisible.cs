using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class C_IsBallVisible : C_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        foreach (Transform opponent in shared.Value.m_Opponents) {
            if (IsCoveringBall(opponent))
            {
                return TaskStatus.Success;
            }
        }
        return TaskStatus.Failure;
        
    }
}
