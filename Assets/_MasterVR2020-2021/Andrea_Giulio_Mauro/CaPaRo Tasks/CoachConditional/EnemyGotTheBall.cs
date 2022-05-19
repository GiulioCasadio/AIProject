using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using Coach;

public class EnemyGotTheBall : CoachBaseConditional
{
    public override TaskStatus OnUpdate()
    {
        foreach (Transform opponent in shared.Value.m_Opponents)
        {
            if (checkManControlBall(opponent.GetPositionXY()))
                return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}