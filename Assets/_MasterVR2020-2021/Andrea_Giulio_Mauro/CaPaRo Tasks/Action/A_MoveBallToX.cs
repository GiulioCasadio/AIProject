using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class A_MoveBallToX : A_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        // calcola distanza posizione x con trashold
        var toTarget = myPosition - targetPosition;
        var distance = toTarget.magnitude;

        // se non l'ho raggiunta mi sposto
        if (distance > radiusTrashold)
        {
            Vector2 targetDirection = ((myPosition - targetPosition) * -1).normalized;

            //go to that position
            output.Value.axes = targetDirection;
            output.Value.isAttracting = true;
            m_owner.SetVariableValue("Output", output);
        }

        return TaskStatus.Success;
    }
}
