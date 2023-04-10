using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollower : MonoBehaviour
{
    public OVRCameraRig cameraRig;
    public Vector3 canvasPos;
    public Quaternion canvasRot;
    public Canvas canvas;
    private Transform centerCamera;

    // Start is called before the first frame update
    void Start()
    {
        centerCamera = cameraRig.centerEyeAnchor;
        canvasPos = canvas.transform.position;
        canvasRot = canvas.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        canvasPos.x = centerCamera.position.x;
        canvasPos.y = centerCamera.position.y;
        canvasPos.z = centerCamera.position.z + 0.3f;
        canvasRot = centerCamera.rotation;
        canvas.transform.position = canvasPos;
        canvas.transform.rotation = canvasRot;
        
    }
}
