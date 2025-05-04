using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Camera))]
public class CameraResizer : MonoBehaviour
{
    public Tilemap tilemap;
    [Tooltip("Fraction of vertical space to leave at bottom for dialogue (e.g., 0.2 = 20%)")]
    [Range(0f, 0.5f)]
    public float dialogueBoxPadding = 0.2f;

    void Start()
    {
        ResizeAndPositionCamera();
    }

    void ResizeAndPositionCamera()
    {
        if (!tilemap) return;

        Camera cam = Camera.main;
        Bounds bounds = tilemap.localBounds;

        float mapWidth = bounds.size.x;
        float mapHeight = bounds.size.y;

        float screenAspect = (float)Screen.width / Screen.height;

        // Account for dialogue box padding: shrink usable height
        float paddedMapHeight = mapHeight / (1f - dialogueBoxPadding);

        // Compute orthographic size needed to fit vertically (with padding)
        float cameraSizeToFitHeight = paddedMapHeight / 2f;

        // Compute orthographic size needed to fit horizontally
        float cameraSizeToFitWidth = (mapWidth / screenAspect) / 2f;

        // Choose the larger of the two
        cam.orthographicSize = Mathf.Max(cameraSizeToFitHeight, cameraSizeToFitWidth);

        // Recenter the camera on the map
        Vector3 mapCenter = bounds.center;
        float offsetY = cam.orthographicSize * dialogueBoxPadding;
        cam.transform.position = new Vector3(mapCenter.x, mapCenter.y - offsetY, cam.transform.position.z);
    }

}
