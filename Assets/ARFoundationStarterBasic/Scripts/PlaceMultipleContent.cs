using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceMultipleContent : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    private Transform m_PlacedPrefab;
    [SerializeField] private GraphicRaycaster raycaster;

    /// <summary>
    /// Invoked whenever an object is placed in on a plane.
    /// </summary>
    public static event Action OnPlacedContent;

    private ARRaycastManager raycastManager;

    private static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

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
                Pose hitPose = hitPoints[0].pose;
                Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                OnPlacedContent?.Invoke();
            }
        }
    }
}