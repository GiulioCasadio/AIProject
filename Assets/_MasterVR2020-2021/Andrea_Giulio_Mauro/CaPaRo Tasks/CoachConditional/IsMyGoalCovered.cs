using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Coach;
using UnityEngine;

public class IsMyGoalCovered : CoachBaseConditional
{
    public override TaskStatus OnUpdate()
    {
        

        foreach (CoachPlayerCommunication cpc in m_sharedCoachVariables.Value.playersCommunications)
        {
            if (checkPlayerBehindBall(cpc))
            {
                float distanceXFromGoal = Vector2.Distance(cpc.m_sharedInput.myPosition, shared.Value.myGoal.GetPositionXY());

                if (distanceXFromGoal <= shared.Value.halfFieldWidth * 0.5)
                {
                    float goalWidthBigger = shared.Value.goalWidth * 1.5f;

                    if (betweenRange(cpc.m_sharedInput.myPosition.y, goalWidthBigger, -goalWidthBigger))
                        return TaskStatus.Success;
                }
            }
        }

        return TaskStatus.Failure;

    }
}
