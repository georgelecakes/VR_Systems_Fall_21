using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Chunk : MonoBehaviour
{

    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    [SerializeField]
    Material chunkMaterial;

    List<Vector3> vertices = new List<Vector3>();
    List<Vector2> uvs = new List<Vector2>();
    List<Vector3> normals = new List<Vector3>();
    List<int> indices = new List<int>();

    const int atlasSize= 16;

    Vector3 v0 = new Vector3(0.5f, -0.5f, -0.5f);
    Vector3 v1 = new Vector3(-0.5f, -0.5f, -0.5f);
    Vector3 v2 = new Vector3(-0.5f, 0.5f, -0.5f);
    Vector3 v3 = new Vector3(0.5f, 0.5f, -0.5f);
    Vector3 v4 = new Vector3(0.5f, -0.5f, 0.5f);
    Vector3 v5 = new Vector3(-0.5f, -0.5f, 0.5f);
    Vector3 v6 = new Vector3(-0.5f, 0.5f, 0.5f);
    Vector3 v7 = new Vector3(0.5f, 0.5f, 0.5f);

    Vector2 uv0 = new Vector2(1.0f,0.0f) * (1.0f / atlasSize);
    Vector2 uv1 = new Vector2(0.0f, 0.0f) * (1.0f / atlasSize);
    Vector2 uv2 = new Vector2(0.0f, 1.0f) * (1.0f / atlasSize);
    Vector2 uv3 = new Vector2(1.0f, 1.0f) * (1.0f / atlasSize);

    enum BlockType
    {
        Air = 0,
        Dirt = 1,
        Brick = 2,
        Wood = 3,
        Ore = 4
    }

    // Start is called before the first frame update
    void Start()
    {
        if(meshFilter == null)
        {
            meshFilter = this.gameObject.AddComponent<MeshFilter>();
        }
        if(meshRenderer == null)
        {
            meshRenderer = this.gameObject.AddComponent<MeshRenderer>();
        }

        meshRenderer.material = chunkMaterial;

        System.Random rand = new System.Random();
        BlockType[,,] blocks = new BlockType[5, 5, 5]; 
        for(int z = 0; z < 5; z++)
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    blocks[x, y, z] = (BlockType)rand.Next(5);
                }
            }
        }

        for (int z = 0; z < 5; z++)
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (blocks[x, y, z] != BlockType.Air)
                    {
                        CreateCube(new Vector3(x, y, z), blocks[x, y, z]);
                    }
                }
            }
        }

        Mesh mesh = new Mesh();
        mesh.SetVertices(vertices);
        mesh.SetIndices(indices.ToArray(), MeshTopology.Quads, 0);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
        meshFilter.mesh= mesh;

    }

    void CreateCube(Vector3 offset, BlockType blockType)
    {
        Vector2 uvOffset = new Vector2();
        switch(blockType)
        {
            case BlockType.Air:
                uvOffset = Vector2.zero;
                break;
            case BlockType.Brick:
                uvOffset = new Vector2(7.0f / atlasSize, 15.0f / atlasSize);
                break;
            case BlockType.Dirt:
                uvOffset = new Vector2(2.0f / atlasSize, 15.0f / atlasSize);
                break;
            case BlockType.Ore:
                uvOffset = new Vector2(0.0f / atlasSize, 14.0f / atlasSize);
                break;
            case BlockType.Wood:
                uvOffset = new Vector2(4.0f / atlasSize, 14.0f /atlasSize);
                break;
        }

        List<Vector3> v = new List<Vector3>();
        List<Vector3> n = new List<Vector3>();
        List<Vector2> u = new List<Vector2>();
        //List<int> i = new List<int>();
        IEnumerable<int> i = Enumerable.Range(indices.Count, 24);

        //Front Face
        v.Add(v0 + offset);
        v.Add(v1 + offset);
        v.Add(v2 + offset);
        v.Add(v3 + offset);
        n.Add(Vector3.forward);
        n.Add(Vector3.forward);
        n.Add(Vector3.forward);
        n.Add(Vector3.forward);
        u.Add(uv0 + uvOffset);
        u.Add(uv1 + uvOffset);
        u.Add(uv2 + uvOffset);
        u.Add(uv3 + uvOffset);

        //Back Face
        v.Add(v5 + offset);
        v.Add(v4 + offset);
        v.Add(v7 + offset);
        v.Add(v6 + offset);
        n.Add(-Vector3.forward);
        n.Add(-Vector3.forward);
        n.Add(-Vector3.forward);
        n.Add(-Vector3.forward);
        u.Add(uv0 + uvOffset);
        u.Add(uv1 + uvOffset);
        u.Add(uv2 + uvOffset);
        u.Add(uv3 + uvOffset);

        //Right Face
        v.Add(v4 + offset);
        v.Add(v0 + offset);
        v.Add(v3 + offset);
        v.Add(v7 + offset);
        n.Add(Vector3.right);
        n.Add(Vector3.right);
        n.Add(Vector3.right);
        n.Add(Vector3.right);
        u.Add(uv0 + uvOffset);
        u.Add(uv1 + uvOffset);
        u.Add(uv2 + uvOffset);
        u.Add(uv3 + uvOffset);

        //Left Face
        v.Add(v1 + offset);
        v.Add(v5 + offset);
        v.Add(v6 + offset);
        v.Add(v2 + offset);
        n.Add(Vector3.left);
        n.Add(Vector3.left);
        n.Add(Vector3.left);
        n.Add(Vector3.left);
        u.Add(uv0 + uvOffset);
        u.Add(uv1 + uvOffset);
        u.Add(uv2 + uvOffset);
        u.Add(uv3 + uvOffset);

        //Top Face
        v.Add(v3 + offset);
        v.Add(v2 + offset);
        v.Add(v6 + offset);
        v.Add(v7 + offset);
        n.Add(Vector3.up);
        n.Add(Vector3.up);
        n.Add(Vector3.up);
        n.Add(Vector3.up);
        u.Add(uv0 + uvOffset);
        u.Add(uv1 + uvOffset);
        u.Add(uv2 + uvOffset);
        u.Add(uv3 + uvOffset);

        //Bottom Face
        v.Add(v4 + offset);
        v.Add(v5 + offset);
        v.Add(v1 + offset);
        v.Add(v0 + offset);
        n.Add(Vector3.down);
        n.Add(Vector3.down);
        n.Add(Vector3.down);
        n.Add(Vector3.down);
        u.Add(uv0 + uvOffset);
        u.Add(uv1 + uvOffset);
        u.Add(uv2 + uvOffset);
        u.Add(uv3 + uvOffset);

        vertices.AddRange(v);
        indices.AddRange(i);
        normals.AddRange(n);
        uvs.AddRange(u);

    }

}
