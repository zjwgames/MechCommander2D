using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public float zoomLevel = 20f;
    public float zoomSpeed = 2f;
    public float minimumZoom = 5f;
    public float maximumZoom = 100f;

    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        zoomLevel -= Input.GetAxisRaw("Mouse ScrollWheel") * zoomSpeed;
        if (zoomLevel < minimumZoom) zoomLevel = minimumZoom;
        if (zoomLevel > maximumZoom) zoomLevel = maximumZoom;
        cam.orthographicSize = zoomLevel;
    }
}
