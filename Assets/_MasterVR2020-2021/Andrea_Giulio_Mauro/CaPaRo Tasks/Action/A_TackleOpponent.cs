using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class A_TackleOpponent : A_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        if (m_sharedPlayerVariables.Value.m_targetTransform != null)
        {
            targetPosition = m_sharedPlayerVariables.Value.m_targetTransform.GetPositionXY();
        }

        Vector2 dir = ((myPosition - targetPosition) * -1).normalized;
        
        if (Vector2.Distance(myPosition, targetPosition) < shared.Value.colliderRadius*3)
        {
            output.Value.requestKick = true;
        }
        CheckHurry(myPosition, targetPosition);
        output.Value.axes = dir;

        m_owner.SetVariableValue("Output", output);
        return TaskStatus.Running;
    }
}