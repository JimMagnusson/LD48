using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float cameraSlowSpeed = 0.1f;
    [SerializeField] private float cameraFastSpeed = 0.5f;
    [SerializeField] private float depthSpeedupFactor = 1f;

    [SerializeField] private float maxCameraSpeed = 0.25f;
    [SerializeField] private float dollyXPos = 0f;

    private const float SEGMENT_LENGTH = 23.937f;

    private DepthMeter depthMeter;
    private int currentDepth;

    [SerializeField] private float cameraSpeed;
    private CinemachineVirtualCamera currentCamera;
    private CinemachineTrackedDolly dolly;
    private int currentSegmentIndex = 1;

    private bool isMoving = true;
    private const float DOLLY_AT_END_DIFF = 0.05f;
    private float dollyCurrentYPos;
    [SerializeField] GameObject goFasterColliderGO = null;
    [SerializeField] GameObject goSlowerColliderGO = null;
    private float startFasterColliderY = 0f;
    private float startSlowerColliderY = 0f;

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
        depthMeter = FindObjectOfType<Player>().GetComponent<DepthMeter>();

        currentCamera = GetComponent<CinemachineVirtualCamera>();
        dolly = currentCamera.GetCinemachineComponent<CinemachineTrackedDolly>();

        // Subscribe to events
        goSlowerColliderGO.GetComponent<SetSpeedOnTrigger>().SetSpeed += CameraMovement_GoSlower;
        goFasterColliderGO.GetComponent<SetSpeedOnTrigger>().SetSpeed += CameraMovement_GoFaster;

        startSlowerColliderY = goSlowerColliderGO.transform.position.y;
        startFasterColliderY = goFasterColliderGO.transform.position.y;

        cameraSpeed = cameraSlowSpeed;
    }

    private void CameraMovement_GoFaster()
    {
        cameraSpeed = cameraFastSpeed;
    }

    private void CameraMovement_GoSlower()
    {
        cameraSpeed = cameraSlowSpeed;
    }

    void Update()
    {
        if (isMoving == true)
        {
            dolly.m_PathPosition += cameraSpeed * Time.deltaTime;
        }
        CheckMovement();
        dollyCurrentYPos = dolly.m_PathPosition * SEGMENT_LENGTH;
        MoveColliders();

        HandleCameraSpeedIncrease();
    }

    private void HandleCameraSpeedIncrease()
    {
        int depthDiff = depthMeter.GetCurrentDepth() - currentDepth; // 0 or 1

        if(cameraSlowSpeed < maxCameraSpeed)
        {
            cameraSlowSpeed += depthDiff * depthSpeedupFactor * Mathf.Pow(10,-4);
            cameraSpeed += depthDiff * depthSpeedupFactor * Mathf.Pow(10, -4);
        }

        currentDepth = depthMeter.GetCurrentDepth();
    }

    private void MoveColliders()
    {
        if(goFasterColliderGO == null || goSlowerColliderGO == null)
        {
            Debug.Log("Either goFasterCollider or goSlowerCollider is null. You need to assign them from the scene.");
        }

        Vector3 goFasterNewPos = new Vector3(goFasterColliderGO.transform.position.x,
                                             startFasterColliderY - dollyCurrentYPos,
                                             goFasterColliderGO.transform.position.z);
        goFasterColliderGO.transform.position = goFasterNewPos;

        Vector3 goSlowerNewPos = new Vector3(goSlowerColliderGO.transform.position.x,
                                             startSlowerColliderY - dollyCurrentYPos,
                                             goSlowerColliderGO.transform.position.z);
        goSlowerColliderGO.transform.position = goSlowerNewPos;
    }

    /*
     * Stops the movement if the dolly has reached its end. Perhaps unneccessary since the game should end when player hits the top frame of the camera.
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
