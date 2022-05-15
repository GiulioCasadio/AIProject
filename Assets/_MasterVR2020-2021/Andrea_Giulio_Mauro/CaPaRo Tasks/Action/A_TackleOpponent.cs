using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class A_TackleOpponent : A_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        Vector2 opponentXY = m_sharedPlayerVariables.Value.m_targetOpponent.GetPositionXY();
        Vector2 dir = ((myPosition - opponentXY) * -1).normalized;
        
        if (Vector2.Distance(myPosition, opponentXY) < shared.Value.colliderRadius*3)
        {
            output.Value.requestKick = true;
        }
        CheckHurry(myPosition, opponentXY);
        output.Value.axes = dir;

        m_owner.SetVariableValue("Output", output);
        return TaskStatus.Running;
    }
}