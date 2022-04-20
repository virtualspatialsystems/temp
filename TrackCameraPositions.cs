using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class AnalyticsTransform
{
    public Vector3 _position;
    public Quaternion _rotation;

    public AnalyticsTransform(Transform t)
    {
        _position = t.position;
        _rotation = t.rotation;
    }

}

[Serializable]
public class SceneAnalytics
{
    public string _sceneName;
    public string _date;
    public AnalyticsTransform[] _transforms;

    public SceneAnalytics(string name, string date, AnalyticsTransform[] analyticsTransforms)
    {
        _sceneName = name;
        _date = date;
        _transforms = analyticsTransforms;
    }
}

public class TrackCameraPositions : MonoBehaviour
{
    private static GameObject instance;
    
    private List<AnalyticsTransform> _analyticsTransforms = new List<AnalyticsTransform>();
    private float frames = 0;
    private float interval = .5f;
    private string sceneName = "";
    GameObject _cameraToTrack;

    CheckForVRActivity _checkForVRActivity;
    // Start is called before the first frame update
    void Awake()
    {

        if (instance == null) { 
            instance = gameObject;
        }
        else { 
            Destroy(gameObject);
        }

        //SceneManager.sceneUnloaded += OnSceneUnload;
        SceneManager.sceneLoaded += OnSceneload;
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        _checkForVRActivity = GetComponent<CheckForVRActivity>();

        DontDestroyOnLoad(this.gameObject);

    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        WriteAnalytics();
    }

    private void OnSceneload(Scene arg0, LoadSceneMode arg1)
    {
        _cameraToTrack = GameObject.Find("CameraLeftEye").gameObject;
        _analyticsTransforms = new List<AnalyticsTransform>();
        frames = 0;
        sceneName = SceneManager.GetActiveScene().name;
    }


    void WriteAnalytics()
    {
        if(sceneName == "" || sceneName == "TitleScene" || _analyticsTransforms.Count == 0) { return; }
        string analyticsData = JsonUtility.ToJson(_analyticsTransforms);
        string date = ((Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();

        string jsonString = JsonUtility.ToJson(new SceneAnalytics(sceneName, date, _analyticsTransforms.ToArray()));
        string path = Application.persistentDataPath + "/"+date.Replace(" " , "")+"_positiontracking_" + sceneName + "_"+ System.Guid.NewGuid()+".json";
        Debug.Log(path);
        System.IO.File.WriteAllText(path, jsonString);
    }


    // Update is called once per frame
    void Update()
    {
        if (_cameraToTrack == null || 
            _checkForVRActivity.playerState == VRPlayerState.INACTIVE || 
            SceneManager.GetActiveScene().name == "TitleScene")
        { return; }

        frames += Time.deltaTime;

        if (frames >= interval)
        {
            frames = frames % interval;

            _analyticsTransforms.Add(new AnalyticsTransform(_cameraToTrack.transform));
        }
    }


}
