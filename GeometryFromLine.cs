using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Line)), ExecuteAlways]
public class GeometryFromLine : MonoBehaviour
{
    public Material _lineMat;
    LineRenderer _lineComponent;
    GameObject _gO;
    MeshFilter _meshFilter;
    MeshRenderer _meshRenderer;

    public Vector2 _lineWidth;

    public float lineWidthMultiplier = 4f;
    
    
    private List<Vector3> points = new List<Vector3>();


    private void OnEnable()
    {
        _lineComponent = GetComponent<LineRenderer>();

    }


    public GameObject ConvertToMesh()
    {
        GameObject bakeMesh = new GameObject("bake-mesh-"+name);
        MeshFilter _meshFilter = bakeMesh.AddComponent<MeshFilter>();
        MeshRenderer _meshRenderer = bakeMesh.AddComponent<MeshRenderer>();
        bakeMesh.AddComponent<ChangeOffsetMaterialBlock>();

        _meshRenderer.material = _lineMat;
        
            
            Mesh _mesh= new Mesh();

        _lineComponent.BakeMesh(_mesh);

        _meshFilter.sharedMesh = _mesh;

        //start(_lineComponent);

        return bakeMesh;

    }

}
