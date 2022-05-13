﻿using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class A_ThrowBall : A_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        // calcola direzione tiro e direzione attuale della palla
        Vector2 targetDirection = ((myPosition - targetPosition) * -1).normalized;
        Vector2 ballDirection = ((myPosition - ballPosition) * -1).normalized;

        // la destinazione e' raggiungibile?
        if (IsReachable(myPosition, targetPosition))
        {
            float ballAngle = Vector2.Angle(targetDirection, ballDirection);

            // controlla se la direzione è corretta
            if (ballAngle < angleTreshold)
            {
                // tira
                output.Value.requestKick = true;
            }
            else
            {
                // disponi la palla in modo tale da lanciarla nella giusta direzione al successivo frame
                // se l'angolo si avvicina a quello di 180 allora mi sposto perpendicolarmente
                if(ballAngle > 180 - behindBallTreshold)
                {
                    targetDirection = Vector2.Perpendicular(targetDirection);
                }
                else
                {
                    // calcolo direzione giocatore-porta e vai in quella opposta così da allineare la palla
                    targetDirection *= -1;
                }

                //go to that position
                output.Value.axes = targetDirection;
                //attract ball
                output.Value.isAttracting = true;
            }
        }
        else
        {
            targetDirection = Vector2.Perpendicular(targetDirection);

            //go to that position
            output.Value.axes = targetDirection;

            //attract ball
            output.Value.isAttracting = true;
        }

        m_owner.SetVariableValue("Output", output);

        return TaskStatus.Running;
    }
}