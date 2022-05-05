using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class C_CanKick : C_Base
{
    public override TaskStatus OnUpdate()
    {
        base.OnUpdate();

        if (IsBallInFeets(shared.Value.myPosition) && )
    }
}
