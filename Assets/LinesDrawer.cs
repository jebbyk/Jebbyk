using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinesDrawer : MonoBehaviour
{
    public Material material;

    public Vector3[] starts, ends;
    public Color[] colors;
    
    int j = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnPostRender()
    {
        
        GL.Begin(GL.LINES);
        material.SetPass(0);
        for (int i = 0; i < starts.Length; i++)
        {
            GL.Color(colors[i]);
            GL.Vertex(starts[i]);
            GL.Vertex(ends[i]);


            starts[i] = Vector3.zero;
            ends[i] = Vector3.zero;
        }
        GL.End();
        j = 0;
    }

    public void pushLine(Vector3 start, Vector3 end, Color color)
    {
        
        Debug.DrawLine(start, end, color);
        starts[j] = start;
        ends[j] = end;
        colors[j] = color;
        j++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
