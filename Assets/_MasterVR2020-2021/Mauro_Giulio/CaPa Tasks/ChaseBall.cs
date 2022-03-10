using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Ca_Pa.CaPa_SharedVariables;

public class ChaseBall : Action
{
    protected Task m_task;
    private Transform ball;
    protected Behavior m_owner => m_task.Owner;

    public override void OnAwake()
    {
        //ball = AIInputData.ball;
    }

    public override TaskStatus OnUpdate()
    {
        /*if (Vector3.SqrMagnitude(transform.position - target.Value.position) < 0.1f)
        {
            return TaskStatus.Success;
        }
        transform.position = Vector3.MoveTowards(transform.position, target.Value.position, speed * Time.deltaTime);*/
        return TaskStatus.Running;
    }
}
