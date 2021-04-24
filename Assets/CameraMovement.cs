using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed = 1f;
    public float dollyXPos = 0f;

    private const float SEGMENT_LENGTH = 23.937f;

    private CinemachineVirtualCamera currentCamera;
    private CinemachineTrackedDolly dolly;

    private int currentSegmentIndex = 1;

    /*
    * Adds a waypoint to the DollyTrack with SEGMENT_LENGTH below the last one (y-axis).
    * Ex. Waypoints = [(0,0,0)] => Waypoints = [(0,0,0),(0, -SEGMENT_LENGTH, 0)]
    */
    public void AddSegmentPointToPath()
    {
        currentSegmentIndex++;

        CinemachineSmoothPath currentPath = (CinemachineSmoothPath)dolly.m_Path;
        int currentPathLength = currentPath.m_Waypoints.Length;

        CinemachineSmoothPath.Waypoint[] newWayPoints = new CinemachineSmoothPath.Waypoint[currentPath.m_Waypoints.Length + 1];
        for(int i = 0; i < currentPath.m_Waypoints.Length; i++)
        {
            newWayPoints[i] = currentPath.m_Waypoints[i];
        }
        newWayPoints[currentPath.m_Waypoints.Length] = new CinemachineSmoothPath.Waypoint();
        newWayPoints[currentPath.m_Waypoints.Length].position = new Vector3(dollyXPos, (-1) * currentSegmentIndex * SEGMENT_LENGTH);
        currentPath.m_Waypoints = newWayPoints;
    }

    void Start()
    {
        currentCamera = GetComponent<CinemachineVirtualCamera>();
        dolly = currentCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    void Update()
    {
        dolly.m_PathPosition += cameraSpeed * Time.deltaTime;
    }
}
