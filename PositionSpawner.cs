using System.Collections;
using System.Collections.Generic;
using UnityEngine;


struct PointWithOrientation {
    public Vector3 position;
    public Vector3 direction;

    public PointWithOrientation(Vector3 pos,Vector3 dir)
    {
        position = pos;
        direction = dir;

    }
}

public class PositionSpawner : MonoBehaviour
{

    public GameObject spawnPositions;
    public List<GameObject> objectsToSpawn;
    public bool buildCluster;
    public int clusterCount;

    public void SpawnObjects()
    {
        GameObject holder = new GameObject("holder");

        //Child Transforms
        Transform[] _positionObjects = spawnPositions.GetComponentsInChildren<Transform>();
        List<PointWithOrientation> pointsWithOrientations = new List<PointWithOrientation>();

        Debug.Log(_positionObjects[1]);

        // Loop über alle children für die Position und Direction
        //NOTE: beginnt mit 1 da GetComponentsInChildren auch den Parent enthält (index für parent == 0)
        for (int i = 1; i < _positionObjects.Length; i++)
        {
            Vector3 _pos = _positionObjects[i].transform.position;
            Vector3[] verts = _positionObjects[i].gameObject.GetComponent<MeshFilter>().sharedMesh.vertices;

            Vector3 direction = _positionObjects[i].TransformPoint(verts[1]);// Vector3.Cross(_positionObjects[i].TransformPoint(verts[0]), _positionObjects[i].TransformPoint(verts[1]) );

            pointsWithOrientations.Add( new PointWithOrientation(_pos, direction) );



        }
        pointsWithOrientations.RemoveAt(0);

        List<PointWithOrientation> _finalPositionsWithOrientation = pointsWithOrientations;

        // Create Cluster
        GameObject[] cluster = new GameObject[clusterCount];

        if (buildCluster) { 
            for(int c = 0; c < clusterCount; c++)
            {
                cluster[c] = new GameObject("Cluster-" + c);
                cluster[c].transform.SetParent( holder.transform );
            }
        }


        // Spawn Objects
        for (int i = 0; i < _finalPositionsWithOrientation.Count; i++)
        {
            int clusterNumber = Random.Range(0, clusterCount);
            GameObject _parent = buildCluster ? cluster[clusterNumber] : holder;
            Vector3 pos = _finalPositionsWithOrientation[i].position;
            
            int randomObjectNumber = Random.Range(0, objectsToSpawn.Count);

            GameObject _spawned = Instantiate(objectsToSpawn[randomObjectNumber], pos, Quaternion.identity, _parent.transform);

            _spawned.transform.LookAt(_finalPositionsWithOrientation[i].direction);
        }

    }

}
