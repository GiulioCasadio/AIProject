using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Coach;
using UnityEngine;

public class OpponentGoalCovered : CoachBaseConditional
{
    public override TaskStatus OnUpdate()
    {
        
        foreach (Transform opponent in shared.Value.m_Opponents)
        {
            if (checkOpponentAdvanceBall(opponent))
            {
                float distanceXFromGoal = Vector2.Distance(opponent.GetPositionXY(), shared.Value.opponentGoal.GetPositionXY());

                if (distanceXFromGoal <= shared.Value.halfFieldWidth * 0.5)
                {
                    float goalWidthBigger = shared.Value.goalWidth * 1.5f;

                    if (betweenRange(opponent.position.y, goalWidthBigger, -goalWidthBigger))
                        return TaskStatus.Success;
                }
            }
        }

        return TaskStatus.Failure;

    }
}
