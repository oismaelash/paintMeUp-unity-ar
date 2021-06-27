using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using System;
using PaintMeUp.Controllers;
using PaintMeUp.Commons;

public class PlaceContent : MonoBehaviour
{
    [SerializeField] private Transform m_PlacedPrefab;
    [SerializeField] private GraphicRaycaster raycaster;
    [SerializeField] private GraphicRaycaster raycaster2;
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.transform.GetComponent<PartOfBody>())
            {
                Debug.LogWarning($"Click on PartOfBody");
                return;
            }
        }

        if (Input.GetMouseButtonDown(0) && !Utils.IsClickOverUI(raycaster) && !Utils.IsClickOverUI(raycaster2)) 
        {   
            List<ARRaycastHit> hitPoints = new List<ARRaycastHit>();
            raycastManager.Raycast(Input.mousePosition, hitPoints, TrackableType.Planes);

            if (hitPoints.Count > 0)
            {
                if (spawnedPrefab == null)
                {
                    m_PlacedPrefab = AppController.Instance.PersonSOSelected.PersonModel.transform;
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