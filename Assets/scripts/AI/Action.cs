using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public int priority;
    public string name;

    public Action(string argName, int argPriority)
    {
        name = argName;
        priority = argPriority;
    }
}
