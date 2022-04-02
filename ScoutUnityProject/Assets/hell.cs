using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hell : MonoBehaviour
{

    public float evalPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    float GetBezierValueAtTime(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector2 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p.y;
    }

    // Update is called once per frame
    void Update()
    {
        float modifier;

        float sample = evalPoint / 40;

        if (sample < 0.5)
        {
            modifier = GetBezierValueAtTime(sample, new Vector2(0, 1.75f), new Vector2(20, 1.75f), new Vector2(20, 0.25f), new Vector2(40, 0.25f));
        }
        else if (sample > 0.5)
        {
            modifier = GetBezierValueAtTime(sample, new Vector2(0, 1.5f), new Vector2(20, 1.5f), new Vector2(20, 0.5f), new Vector2(40, 0.5f));
        }
        else
        {
            modifier = 1;
        }

        Debug.Log(sample + ", " + modifier);
    }
}
