using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Position
{
    public static Vector3 GetRandomPosition(float xMaxAbs, float zMaxAbs)
    {
        float xRand = Random.Range(-xMaxAbs,xMaxAbs);
        float zRand = Random.Range(-zMaxAbs, zMaxAbs);
        return new Vector3(xRand, 0f, zRand);
    }
}
