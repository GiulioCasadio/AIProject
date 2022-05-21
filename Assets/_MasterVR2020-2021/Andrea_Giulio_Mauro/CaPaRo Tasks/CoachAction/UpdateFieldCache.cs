using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Coach;
using UnityEngine;

public class UpdateFieldCache : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        m_sharedCoachVariables.Value.FieldZoneCache.UpdateCache(shared.Value.m_Teams, shared.Value.m_Opponents);
        return TaskStatus.Success;
    }
}
