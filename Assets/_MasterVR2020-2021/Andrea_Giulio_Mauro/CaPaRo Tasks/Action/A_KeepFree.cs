using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class A_KeepFree : A_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();
        
        if (Vector2.Distance(targetPosition, myPosition) < radiusTrashold)
        {
            return TaskStatus.Failure;
        }

        Vector2 targetDirection = ((myPosition - targetPosition) * -1).normalized;

        //go to that position
        output.Value.axes = targetDirection;
        
        CheckHurry(myPosition, targetPosition);
        
        m_owner.SetVariableValue("Output", output);

        return TaskStatus.Success;
    }
}