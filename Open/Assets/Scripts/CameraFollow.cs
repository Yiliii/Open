using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // which will be the Player
    public Tilemap tilemap;  // the world map

    private float minX, maxX, minY, maxY;
    private float camHalfHeight;
    private float camHalfWidth;

    void Start()
    {
        // Calculate camera half extents
        Camera cam = Camera.main;
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = cam.aspect * camHalfHeight;

        // Get tilemap bounds (world space)
        Bounds mapBounds = tilemap.localBounds;

        minX = mapBounds.min.x + camHalfWidth;
        maxX = mapBounds.max.x - camHalfWidth;
        minY = mapBounds.min.y + camHalfHeight;
        maxY = mapBounds.max.y - camHalfHeight;
    }

    void LateUpdate()
    {
        if (!target) return;

        float clampedX = Mathf.Clamp(target.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(target.position.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}