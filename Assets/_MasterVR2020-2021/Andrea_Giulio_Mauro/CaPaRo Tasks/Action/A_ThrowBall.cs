using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class A_ThrowBall : A_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();



        // la destinazione e' raggiungibile?
        if (IsReachable(myPosition, targetPosition))
        {
            // calcola direzione tiro e direzione attuale della palla
            Vector2 targetDirection = ((myPosition - targetPosition) * -1).normalized;
            Vector2 ballDirection = ((myPosition - ballPosition) * -1).normalized;

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
                // calcolo direzione giocatore-porta e vai in quella opposta così da allineare la palla

                //go to that position
                output.Value.axes = targetDirection;

                //attract ball
                output.Value.isAttracting = true;
            }
        }
        else
        {
            // spostati in un'altra posizione

            // muoviti perpendicolare rispetto alla direzione tra te e la porta. 
            // la direzione perpendicolare va in basso o in alto in base alla posizione del giocatore nel campo

            //go to that position
            output.Value.axes = targetDirection;

            //attract ball
            output.Value.isAttracting = true;
        }

        m_owner.SetVariableValue("Output", output);

        return TaskStatus.Success;
    }
}