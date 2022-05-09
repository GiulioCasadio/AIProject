using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class A_MoveBallToX : A_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        // calcola direzione tiro
        Vector2 targetDirection = ((myPosition - targetPosition) * -1).normalized;
        Vector2 ballDirection = ((myPosition - ballPosition) * -1).normalized;


        // la destinazione e' raggiungibile?
        // se si disponi la palla i modo tale da lanciarla nella giusta direzione
        // se no spostati in quella direzione

        //attract ball
        output.Value.isAttracting = true;

        //go to that position
        output.Value.axes = targetDirection;

        CheckHurry(myPosition, targetPosition);

        m_owner.SetVariableValue("Output", output);

        return TaskStatus.Success;
    }
}
