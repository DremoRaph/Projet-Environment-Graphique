using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMeshMaker : MonoBehaviour
{
    [System.Serializable]
    public class Triangle{
        public int PointA, PointB, PointC;
    }
    private Mesh m_Mesh;
    public List<Triangle> Triangles = new List<Triangle>();
    // Start is called before the first frame update
    private void Awake()
    {
        
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Generate();
    }

    public void Generate()
    {
        GetComponent<MeshFilter>().mesh = m_Mesh = new Mesh();
        m_Mesh.name = "Procedural Shape";
        List<Vector3> t_Vertices = new List<Vector3>();
        foreach (Transform Vertice in transform)
        {
            t_Vertices.Add(Vertice.localPosition);
            //Debug.Log(Vertice.gameObject.name);
        }
        m_Mesh.vertices = t_Vertices.ToArray();
        int[] triangles = new int[6];
        triangles[0] = 1;
        triangles[1] = 2;
        triangles[2] = 3;
        triangles[3] = 1;
        triangles[4] = 3;
        triangles[5] = 2;
        
        List<int> t_MeshTriangles = new List<int>();
        foreach(Triangle t_triangle in Triangles)
        {
            t_MeshTriangles.Add(t_triangle.PointA - 1);
            t_MeshTriangles.Add(t_triangle.PointB - 1);
            t_MeshTriangles.Add(t_triangle.PointC - 1);
            t_MeshTriangles.Add(t_triangle.PointA - 1);
            t_MeshTriangles.Add(t_triangle.PointC - 1);
            t_MeshTriangles.Add(t_triangle.PointB - 1);
        }
        
        m_Mesh.triangles = t_MeshTriangles.ToArray();
        //m_Mesh.triangles = triangles;
        

    }
}
