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

    private bool isMoving = true;
    private const float DOLLY_AT_END_DIFF = 0.1f;

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
        if(isMoving == true)
        {
            dolly.m_PathPosition += cameraSpeed * Time.deltaTime;
        }
        CheckMovement(); // Perhaps unneccessary since the game should end when player hits the top frame of the camera.

        //float cameraPosition = dolly.m_Path
        //Debug.Log("Dolly position: " + dolly.m_PathPosition);
        //Debug.Log("Convert pos: " + dolly.m_PositionUnits);
    }

    /*
     * Stops the movement if the dolly has reached its 
     */
    private void CheckMovement()
    {
        CinemachineSmoothPath currentPath = (CinemachineSmoothPath)dolly.m_Path;
        float diff = Mathf.Abs(dolly.m_PathPosition - (currentPath.m_Waypoints.Length - 1));
        if (diff <= DOLLY_AT_END_DIFF)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }
}
