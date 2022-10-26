using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CompareSpeedTest
{
    //CompareSpeedTest.TEST_COMPARE<Player, Item>(other.transform, "Player", "MainCamera", 100000);

    //Tested compare speed
    //CompareTag vs GetComponent vs TryGetComponent.
    //For best time - CompareTag
    //For minimal errors(with string in CompareTag) - better to use TryGetComponent<>

    public static void TEST_COMPARE<T, W> (Transform other, string trueCompare, string falseCompare, int maxIterarions)
    {
        float startTime;
        bool boolResult;
        T notNullClass;
        W nullClass;

        Debug.Log("Test CompareTag and result = true");
        startTime = Time.realtimeSinceStartup;
        for (int i = 0; i < maxIterarions; i++)
        {
            boolResult = other.CompareTag(trueCompare);
        }
        Debug.Log($"Time elapsed = {Time.realtimeSinceStartup - startTime}");

        Debug.Log("Test CompareTag and result = false");
        startTime = Time.realtimeSinceStartup;
        for (int i = 0; i < maxIterarions; i++)
        {
            boolResult = other.CompareTag(falseCompare);
        }
        Debug.Log($"Time elapsed = {Time.realtimeSinceStartup - startTime}");

        Debug.Log("Test GetComponent<> not null");
        startTime = Time.realtimeSinceStartup;
        for (int i = 0; i < maxIterarions; i++)
        {
            notNullClass = other.GetComponent<T>();
        }
        Debug.Log($"Time elapsed = {Time.realtimeSinceStartup - startTime}");

        Debug.Log("Test GetComponent<> null");
        startTime = Time.realtimeSinceStartup;
        for (int i = 0; i < maxIterarions; i++)
        {
            nullClass = other.GetComponent<W>();
        }
        Debug.Log($"Time elapsed = {Time.realtimeSinceStartup - startTime}");
        
        Debug.Log("Test TryGetComponent<> not null");
        startTime = Time.realtimeSinceStartup;
        for (int i = 0; i < maxIterarions; i++)
        {
            boolResult = other.TryGetComponent<T>(out notNullClass);
        }
        Debug.Log($"Time elapsed = {Time.realtimeSinceStartup - startTime}");

        Debug.Log("Test TryGetComponent<> null");
        startTime = Time.realtimeSinceStartup;
        for (int i = 0; i < maxIterarions; i++)
        {
            boolResult = other.TryGetComponent<W>(out nullClass);
        }
        Debug.Log($"Time elapsed = {Time.realtimeSinceStartup - startTime}");
    }
}
