using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class Custom1927Renderer : MonoBehaviour
{
    public Camera camera;
    public GameObject renderObj;


    private CommandBuffer m_1927Buffer;
    Renderer r;
    Material mat;
    int tempID;

    [SerializeField]
    CameraEvent cameraEvent = CameraEvent.BeforeForwardOpaque;



    void Start()
    {   
        //Create Command Buffer
        m_1927Buffer = new CommandBuffer();
        m_1927Buffer.name = "1927 map buffer";
        m_1927Buffer.SetSinglePassStereo(SinglePassStereoMode.SideBySide);


        tempID = Shader.PropertyToID("_1927Map");

        r = renderObj.GetComponent<Renderer>();
        mat = renderObj.GetComponent<Custom1927Obj>().finalRenderMat;
        m_1927Buffer.SetGlobalTexture("_1927Map", tempID);


        //Add Command Buffer to Camera
        camera.AddCommandBuffer(CameraEvent.BeforeLighting, m_1927Buffer);
    }
    public void OnDisable()
    {
        camera.RemoveCommandBuffer(cameraEvent, m_1927Buffer);
    }

    public void OnWillRenderObject()
    {
        if (r && mat)
        {
            m_1927Buffer.Clear();

            m_1927Buffer.GetTemporaryRT(tempID, -1, -1, 24, FilterMode.Bilinear);
            m_1927Buffer.SetRenderTarget(tempID);
            m_1927Buffer.ClearRenderTarget(true, true, Color.black); // clear before drawing to it each frame!!

        
            m_1927Buffer.DrawRenderer(r, mat);
        }

    }
}