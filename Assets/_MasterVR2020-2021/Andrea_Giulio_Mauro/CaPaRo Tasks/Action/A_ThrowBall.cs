using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.Player;

using Ca_Pa_Ro.CaPaRo_SharedVariables;

public class A_ThrowBall : A_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        // Controllo se è un passaggio o un tiro in porta
        if(m_sharedPlayerVariables.Value.m_targetTransform!=null && m_sharedPlayerVariables.Value.m_state != PlayerFocus.PlayerStateFocus.PASSBALL)
        {
            targetPosition = m_sharedPlayerVariables.Value.m_targetTransform.GetPositionXY();
        }
        else
        {
            targetPosition = shared.Value.opponentGoal.GetPositionXY();
        }

        // calcola direzione tiro e direzione attuale della palla
        Vector2 targetDirection = ((myPosition - targetPosition) * -1).normalized;
        Vector2 ballDirection = ((myPosition - ballPosition) * -1).normalized;

        // sono attaccato alla palla?
        if ((myPosition - ballPosition).magnitude < AIInputData.m_AttractMaxRadius) {
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
                    if (ballAngle > 180 - behindBallTreshold)
                    {
                        if(myPosition.y<0)
                            targetDirection = Vector2.Perpendicular(targetDirection);
                        else
                            targetDirection = Vector2.Perpendicular(targetDirection)*-1;
                    }
                    else
                    {
                        // calcolo direzione giocatore-porta e vai in quella opposta così da allineare la palla
                        targetDirection *= -1;
                    }

                    //go to that position
                    output.Value.axes = targetDirection;
                    //attract ball
                    output.Value.requestAttracting = true;
                }
            }
            else
            {
                if (myPosition.y < 0)
                    targetDirection = Vector2.Perpendicular(targetDirection);
                else
                    targetDirection = Vector2.Perpendicular(targetDirection) * -1;

                //go to that position
                output.Value.axes = targetDirection;

                //attract ball
                output.Value.requestAttracting = true;
            }
        }
        else
        {
            // mi sposto verso la palla
            targetDirection = ((myPosition - ballPosition) * -1).normalized;
            output.Value.axes = targetDirection;

            CheckHurry(myPosition, ballPosition);
        }

        m_owner.SetVariableValue("Output", output);

        return TaskStatus.Running;
    }
}