using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class A_CoverGoal : A_Base
{
    public float timer = Time.deltaTime;
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        targetPosition = shared.Value.myGoal.GetPositionXY();

        Vector2 midPoint = new Vector2((ballPosition.x + targetPosition.x) / 2, (ballPosition.y + targetPosition.y) / 2);

        Vector2 midMidPoint = new Vector2((midPoint.x + targetPosition.x) / 2, (midPoint.y + targetPosition.y) / 2);

        Vector2 targetDirection = ((myPosition - midMidPoint) * -1).normalized;

        //go to that position
        output.Value.axes = targetDirection;

        CheckHurry(myPosition, targetPosition);

        m_owner.SetVariableValue("Output", output);

        return TaskStatus.Running;
    }
}

