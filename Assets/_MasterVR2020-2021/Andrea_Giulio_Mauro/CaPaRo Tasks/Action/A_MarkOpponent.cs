using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class A_MarkOpponent : A_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        if (m_sharedPlayerVariables.Value.m_targetTransform != null) { 
            targetPosition = m_sharedPlayerVariables.Value.m_targetTransform.GetPositionXY();
        }
        Vector2 midPoint = new Vector2((ballPosition.x + targetPosition.x) / 2, (ballPosition.y + targetPosition.y) / 2);

        Vector2 midMidPoint = new Vector2((midPoint.x + targetPosition.x) / 2, (midPoint.y + targetPosition.y) / 2);

        Vector2 targetDirection = ((myPosition - midMidPoint) * -1).normalized;

        //go to that position
        output.Value.axes = targetDirection;

        CheckHurry(myPosition, midPoint);

        m_owner.SetVariableValue("Output", output);
        return TaskStatus.Running;
    }
}