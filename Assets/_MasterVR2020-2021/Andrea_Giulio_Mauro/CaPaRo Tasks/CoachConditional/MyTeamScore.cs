using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class MyTeamScore : CoachBaseConditional
{
    public override TaskStatus OnUpdate()
    {
        float ballXPosition = shared.Value.ballPosition.x;

        float goalXPosition = shared.Value.opponentGoal.position.x;

        bool goalRight = goalXPosition > 0;

        if (goalRight)
        {
            goalXPosition -= shared.Value.goalWidth / 2;
            ballXPosition -= shared.Value.ballRadius;
            if (ballXPosition > goalXPosition)
            {
                return TaskStatus.Success;
            }
        }
        
        else
        {
            goalXPosition += shared.Value.goalWidth / 2;
            ballXPosition += shared.Value.ballRadius;
            if (ballXPosition < goalXPosition)
            {
                return TaskStatus.Success;
            }
        }

        return TaskStatus.Failure;

    }
}
