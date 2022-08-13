using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer
{
    public int dynastyCount = 3;

    [RuntimeInitializeOnLoadMethod]
    public static void init()
    {
        Debug.Log("Game started");

        Initializer obj = new Initializer();
        DynastyHandler handler = new DynastyHandler(obj.dynastyCount);

        handler.populate();
    }
}
