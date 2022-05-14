using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coach;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa_Ro.Player;


[TaskCategory("Coach")]
public class CoverZoneOrFight : CoachBaseAction
{
    public override TaskStatus OnUpdate()
    {
        
        
        foreach (CoachPlayerCommunication cpc  in m_sharedCoachVariables.Value.playersCommunications)
        {

            if (cpc.m_focusGiven)
            {
                
            }

        }
        
        return TaskStatus.Success;
    }
}
