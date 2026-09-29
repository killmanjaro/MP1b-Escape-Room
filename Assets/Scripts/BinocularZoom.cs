using UnityEngine;

public class BinocularZoom : MonoBehaviour
{
    public Camera binocularCameraL;
    public Camera binocularCameraR;
    public Transform playerHead; // auto-found at runtime if left empty
    public float normalFOV = 60f;
    public float zoomedFOV = 20f;
    public float maxDistanceForZoom = 0.15f; // how close counts as "held up to eye"

    void Start()
    {
        if (playerHead == null)
        {
            Camera mainCam = Camera.main; // finds the camera tagged "MainCamera"
            if (mainCam != null)
                playerHead = mainCam.transform;
            else
                Debug.LogWarning("BinocularZoom: No Main Camera found — make sure your XR head camera is tagged 'MainCamera'.");
        }
    }

    void Update()
    {
        if (playerHead == null) return;

        float distance = Vector3.Distance(transform.position, playerHead.position);
        float t = Mathf.InverseLerp(maxDistanceForZoom, maxDistanceForZoom * 3f, distance);
        // t = 0 when very close (zoomed in), 1 when far (normal view)

        float fov = Mathf.Lerp(zoomedFOV, normalFOV, t);

        if (binocularCameraL != null)
            binocularCameraL.fieldOfView = fov;

        if (binocularCameraR != null)
            binocularCameraR.fieldOfView = fov;
    }
}