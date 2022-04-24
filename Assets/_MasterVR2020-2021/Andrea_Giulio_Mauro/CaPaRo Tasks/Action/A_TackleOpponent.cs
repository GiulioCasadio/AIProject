using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class A_TackleOpponent : A_Base
{
    public override TaskStatus OnUpdate()
    {
        var opponent = GetOpponentNearestTo(shared.Value.myPosition, shared.Value.m_Opponents);

        output.Value.axes = opponent.position;

        return TaskStatus.Success;
    }
}