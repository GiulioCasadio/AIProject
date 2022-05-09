using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class A_ThrowBall : A_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        //attract ball
        output.Value.requestKick = true;

        m_owner.SetVariableValue("Output", output);

        return TaskStatus.Success;
    }
}