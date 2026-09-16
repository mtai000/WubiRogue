using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class Utils
{
    public static string[] texts;
 
    public static Vector3 RandomOutscreenPosition(float height = 50f,float apsect = 16f / 9f)
    {
        float width = 50f * apsect;
        int selectArea = UnityEngine.Random.Range(0, 4);
        if (selectArea == 0)
        {
            return new Vector3(UnityEngine.Random.Range(width, width * 1.2f),
                              UnityEngine.Random.Range(-height, height), 0);
        }
        else if (selectArea == 1)
        {
            return new Vector3(-UnityEngine.Random.Range(width, width * 1.2f),
                               UnityEngine.Random.Range(-height, height), 0);
        }
        else if (selectArea == 2)
        {
            return new Vector3(UnityEngine.Random.Range(-width, width),
                              UnityEngine.Random.Range(height, height * 1.2f), 0);
        }
        else if (selectArea == 3)
        {
            return new Vector3(UnityEngine.Random.Range(-width, width),
                             -UnityEngine.Random.Range(height, height * 1.2f), 0);
        }
        return Vector3.zero;
    }

    public static List<GameObject> FindGameObjectsWithName(GameObject parent, string name)
    {
        List<GameObject> matchedObjects = new List<GameObject>();

        Transform[] allChildren = parent.GetComponentsInChildren<Transform>(true);

        foreach (Transform child in allChildren)
        {
            if (child.gameObject.name.StartsWith(name))
            {
                matchedObjects.Add(child.gameObject);
            }
        }

        return matchedObjects;
    }
}