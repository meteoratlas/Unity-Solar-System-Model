using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit {

    public Vector2 Axis;

    public Orbit(float x, float y)
    {
        Axis.x = x;
        Axis.y = y;
    }

    public Vector2 Evaluate(float t)
    {
        t += 0.00001f;
        float angle = Mathf.Deg2Rad * 360f * t;
        float x = Mathf.Sin(angle) * Axis.x;
        float y = Mathf.Cos(angle) * Axis.y;
        //Debug.Log(x);
        return new Vector2(x, y);
    }
}
