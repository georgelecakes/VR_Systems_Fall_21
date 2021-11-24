using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class RibbonGenerator : MonoBehaviour
{
    MeshFilter mf;
    MeshRenderer mr;

    public float length = 10;
    public float width = 1;
    public int segments = 10;

    public Material material;

    public List<float> vertx = new List<float>();
    public List<float> verty = new List<float>();
    public List<float> vertz = new List<float>();

    public List<Vector3> vertex;

    public List<int> indicies = new List<int>();
    int counter = 0;

    public Mesh Generate()
    {
        float leseg = length / segments;

        Mesh mesh = new Mesh();
        mf.mesh = mesh;
        List<Vector3> verticies = new List<Vector3>();
        indicies = new List<int>();
        List<Vector3> normals = new List<Vector3>();


        for (int i = 0; i < segments; i++)
        {
            verticies.Add(new Vector3(i * leseg, 0, 0));
            indicies.Add(4 * i);
            normals.Add(-Vector3.forward);
            verticies.Add(new Vector3(i * leseg, 0, width));
            indicies.Add(4 * i + 1);
            normals.Add(-Vector3.forward);
            verticies.Add(new Vector3(i * leseg + leseg, 0, width));
            indicies.Add(4 * i + 2);
            normals.Add(-Vector3.forward);
            verticies.Add(new Vector3(i * leseg + leseg, 0, 0));
            indicies.Add(4 * i + 3);
            normals.Add(-Vector3.forward);
        }
        
        for (int i = 0; i < 4 * segments; i++)
        {
            vertx.Add(verticies[i].x);
            verty.Add(verticies[i].y);
            vertz.Add(verticies[i].z);
        }

        print(vertx.Count);
        print(verty.Count);
        print(vertz.Count);

        mesh.SetVertices(verticies);
        mesh.SetIndices(indicies.ToArray(), MeshTopology.Quads, 0);
        mesh.SetNormals(normals);
        return mesh;
    }

    public Mesh UpdateMesh()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();

        List<Vector3> v = new List<Vector3>(mf.mesh.vertices);

        for (int i = 0; i < v.Count; i++)
        {
            float newY = Mathf.Sin(v[i].x + Time.time);
            v[i] = new Vector3( v[i].x, newY, v[i].z);
;        }

        Mesh mesh = new Mesh();
        mesh.SetVertices(v);
        mesh.SetIndices(indicies.ToArray(), MeshTopology.Quads, 0);
        return mesh;
    }

    // Start is called before the first frame update
    void Start()
    {

        mf = GetComponent<MeshFilter>();
        if(!mf)
        {
            mf = this.gameObject.AddComponent<MeshFilter>();
        }

        mr = GetComponent<MeshRenderer>();
        if (!mr)
        {
            mr = this.gameObject.AddComponent<MeshRenderer>();
            
        }

        if(mr.material == null)
        {
            mr.material = this.material;
        }

        Generate();
    }

    // Update is called once per frame
    void Update()
    {
            mf.mesh = UpdateMesh();
    }

    
}
