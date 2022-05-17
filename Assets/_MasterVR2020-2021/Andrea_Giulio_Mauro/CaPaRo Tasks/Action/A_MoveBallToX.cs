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

        // controlla se non sono abbastanza vicino alla palla per controllarla e nel caso la raggiungo
        if ((myPosition-ballPosition).magnitude > radiusTreshold)
        {
            //go to that position
            Vector2 targetDirection = ((myPosition - ballPosition) * -1).normalized;
            output.Value.axes = targetDirection;

            CheckHurry(myPosition, ballPosition);
        }
        else
        {
            // mi sposto verso target oppure attiro e sto fermo
            if (distance > radiusTreshold)
            {
                //go to that position
                Vector2 targetDirection = ((myPosition - targetPosition) * -1).normalized;
                output.Value.axes = targetDirection;
            }
            output.Value.requestAttracting = true;
        }

        m_owner.SetVariableValue("Output", output);

        return TaskStatus.Running;
    }
}
