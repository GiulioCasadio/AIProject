﻿using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class A_MoveBallToX : A_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        // calcola distanza posizione x con trashold
        var toTarget = myPosition - targetPosition;
        var distance = toTarget.magnitude;

        // controlla se sono abbastanza vicino alla palla per controllarla


        // altrimenti attiro e se non l'ho raggiunta mi sposto verso target
        if (distance > radiusTreshold)
        {
            Vector2 targetDirection = ((myPosition - targetPosition) * -1).normalized;

            //go to that position
            output.Value.axes = targetDirection;
            m_owner.SetVariableValue("Output", output);
        }

        output.Value.requestAttracting = true;

        return TaskStatus.Running;
    }
}
