using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Action: ScriptableObject
{
    protected ITargetable Target;
    protected bool Ready;

    public abstract void Use(Entity CallingObject);

    public virtual bool SetTarget(ITargetable target)
    {
        Target = target;
        return true;
    }
}
