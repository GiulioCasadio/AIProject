using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class C_IsBallVisible : C_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        if (!IsReachable(myPosition, ballPosition))
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
        
    }
}
