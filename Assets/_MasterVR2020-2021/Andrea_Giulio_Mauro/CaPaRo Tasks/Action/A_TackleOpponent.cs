using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class A_TackleOpponent : A_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();
        
        //Transform opponent = GetOpponentNearestTo(shared.Value.myPosition, shared.Value.m_Opponents);
        Vector2 dir = ((myPosition - targetPosition) * -1).normalized;

        //rendilo conditional
        if (Vector2.Distance(myPosition, targetPosition) < radiusTreshold)
        {
            requestKick = true;
        }
        else
        {
            axes = dir;
        }
        
        CheckHurry(myPosition, targetPosition);
        
        m_owner.SetVariableValue("Output", output);
        return TaskStatus.Success;
    }
}