using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static GameObject[] getAvailableLocations()
    {
        GameObject[] locations = GameObject.FindGameObjectsWithTag("locationMarker");
        var result = new List<GameObject>();

        foreach (var location in locations)
        {
            var markerInstance = location.GetComponent<locationMarker>();

            if (!markerInstance.isOccupied)
            {
                result.Add(location);
            }
        }

        return result.ToArray();
    }



    public static void ShuffleArray<T>(T[] arg)
    {

        T tempGO;

        for (int i = 0; i < arg.Length; i++)
        {
            int rnd = Random.Range(0, arg.Length);
            tempGO = arg[rnd];
            arg[rnd] = arg[i];
            arg[i] = tempGO;
        }
    }



    public static TitleData GetNextDynastyTitle(Dynasty arg)
    {
        Titles titles = new Titles();

        TitleData currentTitle = System.Array.Find<TitleData>(titles.list, item => item.title == arg.dynastyTitle);

        int index = System.Array.IndexOf(titles.list, currentTitle);

        return titles.list[index + 1];
    }



    public static bool CheckIfInProgress(Dynasty dynasty, string actionName)
    {
        var isInProgress = false;

        dynasty.members.ForEach(member =>
       {
           if (member.currentAction != null && member.currentAction.name == actionName)
           {
               isInProgress = true;
           }
       });

        return isInProgress;
    }
}
