using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class A_KeepFree : A_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();
        
        Vector2 targetDirection = ((myPosition - ballPosition) * -1).normalized;

        targetDirection = Vector2.Perpendicular(targetDirection);

        //go to that position
        output.Value.axes = targetDirection;
        
        CheckHurry(myPosition, targetPosition);
        
        m_owner.SetVariableValue("Output", output);

        return TaskStatus.Running;
    }
}