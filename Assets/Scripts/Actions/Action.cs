using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
*   Name: Sean Robbins
*   ID: 2328696
*   Email: srobbins@chapman.edu
*   Class: CPSC245
*   Turn Based Combat System
*   This is my own work. I did not cheat on this assignment
*/

public abstract class Action: ScriptableObject
{
    protected ITargetable Target;
    protected bool Ready;

    /// <summary>
    /// Method to determine what the action will do on the target selected
    /// </summary>
    /// <param name="CallingObject"></param>
    public abstract void Use(Entity CallingObject);

    /// <summary>
    /// Method to select a target for the action to act on
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public virtual bool SetTarget(ITargetable target)
    {
        Target = target;
        Ready = true;
        return Ready;
    }
}
