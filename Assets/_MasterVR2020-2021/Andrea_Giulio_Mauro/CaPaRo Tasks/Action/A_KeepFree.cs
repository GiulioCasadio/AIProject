using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class A_KeepFree : A_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        Vector2 opponentPosition = GetOpponentNearestTo(shared.Value.myPosition, shared.Value.m_Opponents).position;
        Vector2 ballPosition = shared.Value.ballPosition;
        Vector2 myPosition = shared.Value.myPosition;

        Vector2 midPoint = new Vector2((ballPosition.x + opponentPosition.x) / 2, (ballPosition.y + opponentPosition.y) / 2);
        if (midPoint == myPosition)
            return TaskStatus.Failure;

        Vector2 targetDirection = ((myPosition - midPoint) * -1).normalized;

        //go to that position
        output.Value.axes = targetDirection;

        m_owner.SetVariableValue("Output", output);
        return TaskStatus.Success;
    }
}