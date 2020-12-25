using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;

    private Camera _camera;


    void Start()
    {
        _camera = GetComponent<Camera>();

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void OnGUI()
    { 
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y + 8f, target.position.z - 11), 0.1f);
       
    }
}
