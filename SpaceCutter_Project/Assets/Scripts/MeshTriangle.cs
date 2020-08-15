using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTriangle
{
    private List<Vector3> _Vertices = new List<Vector3>();
    private List<Vector3> _Normals = new List<Vector3>();
    private List<Vector2> _UVs = new List<Vector2>();
    int _SubMeshIndex;

    public List<Vector3> Vertices { get { return _Vertices; } set { _Vertices = value; } }
    public List<Vector3> Normals { get { return _Normals; } set { _Normals = value; } }
    public List<Vector2> UVs { get { return _UVs; } set { _UVs = value; } }
    public int SubMeshIndex { get { return _SubMeshIndex; } set { SubMeshIndex = value; } }

    public MeshTriangle(Vector3[] vertices, Vector3[] normals, Vector2[] uvs, int submeshindex)
    {
        Clear();
        
        _Vertices.AddRange(vertices);
        _Normals.AddRange(normals);
        _UVs.AddRange(uvs);

        _SubMeshIndex = submeshindex;
    }

    public void Clear()
    {
        _Vertices.Clear();
        _Normals.Clear();
        _UVs.Clear();

        _SubMeshIndex = 0;
    }
}