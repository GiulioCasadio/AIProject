using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Coach;
using UnityEngine;

public class UpdateGravitiesCenters : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {

        ref Vector2 MyTeamCenterGravity = ref m_sharedCoachVariables.Value.MyTeamCenterGravity;
        ref Vector2 OtherTeamCenterGravity = ref m_sharedCoachVariables.Value.OtherTeamCenterGravity;

        SetCenterGravity(shared.Value.m_Teams, ref MyTeamCenterGravity);
        SetCenterGravity(shared.Value.m_Opponents, ref OtherTeamCenterGravity);

        return TaskStatus.Success;
    }


    private void SetCenterGravity(List<Transform> i_teamTransforms, ref  Vector2 i_centerGravity)
    {
        if (i_teamTransforms == null || i_centerGravity == null || i_teamTransforms.Count == 0)
            return;
        
        float newCenterX = 0;
        float newCenterY = 0;
        foreach (Transform transform in i_teamTransforms)
        {
            newCenterX += transform.position.x;
            newCenterY += transform.position.y;
        }

        newCenterX /= i_teamTransforms.Count;
        newCenterY /= i_teamTransforms.Count;
        
        i_centerGravity.x = newCenterX;
        i_centerGravity.y = newCenterY;
    }
}
