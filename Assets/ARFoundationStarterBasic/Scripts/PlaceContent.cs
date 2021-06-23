using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using System;

public class PlaceContent : MonoBehaviour
{
    [SerializeField] private Transform m_PlacedPrefab;
    [SerializeField] private GraphicRaycaster raycaster;
    private ARRaycastManager raycastManager;
    /// <summary>
    /// Invoked whenever an object is placed in on a plane.
    /// </summary>
    public static event Action OnPlacedContent;

    private Transform spawnedPrefab;

    private void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Utils.IsClickOverUI(raycaster))
        {
            List<ARRaycastHit> hitPoints = new List<ARRaycastHit>();
            raycastManager.Raycast(Input.mousePosition, hitPoints, TrackableType.Planes);

            if (hitPoints.Count > 0)
            {
                if (spawnedPrefab == null)
                {
                    Pose hitPose = hitPoints[0].pose;
                    spawnedPrefab = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                    OnPlacedContent?.Invoke();
                }
                else
                {
                    Pose hitPose = hitPoints[0].pose;
                    spawnedPrefab.position = hitPose.position;
                    spawnedPrefab.rotation = hitPose.rotation;
                }
            }
        }
    }
}