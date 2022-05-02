using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class A_KeepFree : A_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        Vector2 targetPosition = m_sharedPlayerVariables.Value.m_targetPosition;
        Vector2 myPosition = shared.Value.myPosition;

        if (Vector2.Distance(targetPosition, myPosition) < trashold)
        {
            return TaskStatus.Failure;
        }

        Vector2 targetDirection = ((myPosition - targetPosition) * -1).normalized;

        //go to that position
        output.Value.axes = targetDirection;

        m_owner.SetVariableValue("Output", output);
        return TaskStatus.Success;
    }
}