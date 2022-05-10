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

            // controlla se la direzione è corretta

            // se si tira
            // se no disponi la palla in modo tale da lanciarla nella giusta direzione
        }
        else
        {
            // spostati in un'altra posizione
        }



        //attract ball
        output.Value.isAttracting = true;

        //go to that position
        output.Value.axes = targetDirection;

        CheckHurry(myPosition, targetPosition);

        m_owner.SetVariableValue("Output", output);

        return TaskStatus.Success;
    }
}