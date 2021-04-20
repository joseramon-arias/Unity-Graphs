using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] Transform pointPrefab = default;

    [SerializeField, Range(9, 100)] int resolution = 10;

    [SerializeField] FunctionLibrary.FunctionName function = default;

    Transform[] points;
    float step;

    private void Awake()
    {
        points = new Transform[resolution * resolution];
        step = 2f / resolution;
        Vector3 scale = Vector3.one * step;

        for (int i = 0; i < points.Length; i++)
        {
            Transform point = Instantiate(pointPrefab);
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time;
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);

        float v = 0.5f * step - 1f;
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
            if (x == resolution)
            {
                x = 0;
                z += 1;
                v = (z + 0.5f) * step - 1f;
            }

            float u = (x + 0.5f) * step - 1f;

            Vector3 position = f(u, v, time);
            points[i].localPosition = position;
        }
    }
}
