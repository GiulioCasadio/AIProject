using UnityEngine;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

using Ca_Pa_Ro.CaPaRo_SharedVariables;
using Coach;

[TaskCategory("CoachConditional")]
public class CoachBaseConditional : Conditional
{
    #region conditional task setup
    protected Task m_task;
    protected Behavior m_owner => m_task.Owner;

    public SharedAIInputData shared;
    public SharedCoachVariables m_sharedCoachVariables;

    public override void OnAwake()
    {
        m_task = this;
    }

    public override void OnStart()
    {
        shared = m_owner.GetVariable("Shared") as SharedAIInputData;
        m_sharedCoachVariables = m_owner.GetVariable("m_coachVariables") as SharedCoachVariables;
    }
    #endregion


    protected bool betweenRange(float value, float start, float end)
    {
        float internalStart = start;
        float internalEnd = end;

        if (start > end)
        {
            internalStart = end;
            internalEnd = start;
        }
        
        return value >= internalStart && value <= internalEnd;
    }

    protected bool checkPlayerBehindBall(CoachPlayerCommunication player)
    {
        float extremeBallPoint = shared.Value.ballPosition.x + shared.Value.ballRadius;
        
        
        bool myGoalLeft = shared.Value.myGoal.position.x < 0;

        if (myGoalLeft)
            extremeBallPoint = shared.Value.ballPosition.x - shared.Value.ballRadius;

        return betweenRange(player.m_sharedInput.myPosition.x, shared.Value.myGoal.position.x, extremeBallPoint);

    }
}
