using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class A_TackleOpponent : A_Base
{
    public override TaskStatus OnUpdate()
    {
        Transform opponent = GetOpponentNearestTo(shared.Value.myPosition, shared.Value.m_Opponents);
        Vector2 dir = ((shared.Value.myPosition - shared.Value.ballPosition) * -1).normalized;

        if (Vector2.Distance(shared.Value.myPosition, shared.Value.ballPosition) < 1f)
        {
            //reset axes
            output.Value.axes = new Vector2(0,0);
            output.Value.requestTackle = true;
        }
        else
        {
            output.Value.axes = dir;
        }

        return TaskStatus.Success;
    }
}