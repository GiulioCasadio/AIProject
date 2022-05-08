using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class A_ThrowBallToX : A_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        Vector2 targetDirection = ((myPosition - targetPosition) * -1).normalized;

        //go to that position
        output.Value.axes = targetDirection;

        CheckHurry(myPosition, targetPosition);

        m_owner.SetVariableValue("Output", output);

        return TaskStatus.Success;
    }
}