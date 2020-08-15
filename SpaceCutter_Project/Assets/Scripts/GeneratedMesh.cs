using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedMesh 
{
    private List<Vector3> _Vertices = new List<Vector3>();
    private List<Vector3> _Normals = new List<Vector3>();
    private List<Vector2> _UVs = new List<Vector2>();
    private List<List<int>> _SubMeshIndices = new List<List<int>>();

    public List<Vector3> Vertices { get { return _Vertices; } set { _Vertices = value; } }
    public List<Vector3> Normals { get { return _Normals; } set { _Normals = value; } }
    public List<Vector2> UVs { get { return _UVs; } set { _UVs = value; } }
    public List<List<int>> SubMeshIndices {  get { return _SubMeshIndices; } set { _SubMeshIndices = value; } }

    public void AddTriangle(MeshTriangle triangle)
    {
        int currentVerticeCount = _Vertices.Count;

        _Vertices.AddRange(triangle.Vertices);
        _Normals.AddRange(triangle.Normals);
        _UVs.AddRange(triangle.UVs);

        if(_SubMeshIndices.Count < triangle.SubMeshIndex + 1)
        {
            for(int i = _SubMeshIndices.Count; i<triangle.SubMeshIndex + 1; i++)
            {
                _SubMeshIndices.Add(new List<int>());
            }
        }

        for(int i = 0; i < 3; i++)
        {
            _SubMeshIndices[triangle.SubMeshIndex].Add(currentVerticeCount + i);
        }
    }
    public Mesh GetGeneratedMesh()
    {
        Mesh mesh = new Mesh();
        mesh.SetVertices(_Vertices);
        mesh.SetNormals(_Normals);
        mesh.SetUVs(0, _UVs);
        mesh.SetUVs(1, _UVs);

        mesh.subMeshCount = _SubMeshIndices.Count;
        for (int i = 0; i < _SubMeshIndices.Count; i++)
        {
            mesh.SetTriangles(_SubMeshIndices[i], i);
        }
        return mesh;
    }
}
