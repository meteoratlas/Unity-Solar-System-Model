using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawOrbitLine : MonoBehaviour {

    LineRenderer lineRenderer;
    Material mat;
    Vector3[] points;
    public int Resolution = 64;
    public GameObject Parent;
    public int Radius = 10;
    public float rotateSpeed = 0.4f;

    void Awake () {
        Debug.Log("make orbit");

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = Resolution;

        mat = lineRenderer.material;

        points = new Vector3[Resolution];

        for (int i = 0; i <= Resolution - 1; i++)
        {
            float x = Parent.transform.position.x + Mathf.Cos(i * (Mathf.PI / 180)) * Radius;
            float y = Parent.transform.position.y + Mathf.Sin(i * (Mathf.PI / 180)) * Radius;
            points[i] = new Vector3(x, y, 0);
        }
        lineRenderer.SetPositions(points);
        
	}

    void Update()
    {

        float offset = Time.time * rotateSpeed;
        lineRenderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        // Vector2 cur = mat.GetTextureOffset("_MainTex");
        //mat.SetTextureOffset("_MainTex", new Vector2(cur.x += rotateSpeed, cur.y));
    }


}
