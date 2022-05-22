using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class A_KeepFree : A_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();
        
        Vector2 targetDirection = ((myPosition - ballPosition) * -1).normalized;

        if (myPosition.y < 0)
            targetDirection = Vector2.Perpendicular(targetDirection);
        else
            targetDirection = Vector2.Perpendicular(targetDirection) * -1;

        //go to that position
        output.Value.axes = targetDirection;
        
        CheckHurry(myPosition, targetPosition);
        
        m_owner.SetVariableValue("Output", output);

        return TaskStatus.Running;
    }
}