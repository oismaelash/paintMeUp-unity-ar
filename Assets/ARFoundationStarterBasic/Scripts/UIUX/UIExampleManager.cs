using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class UIExampleManager : MonoBehaviour
{
    private ARCameraManager m_CameraManager;
    private ARPlaneManager m_PlaneManager;

    /// <summary>
    /// Get or set the <c>ARCameraManager</c>.
    /// </summary>
    public ARCameraManager cameraManager
    {
        get { return m_CameraManager; }
        set
        {
            if (m_CameraManager == value)
                return;

            if (m_CameraManager != null)
                m_CameraManager.frameReceived -= FrameChanged;

            m_CameraManager = value;

            if (m_CameraManager != null & enabled)
                m_CameraManager.frameReceived += FrameChanged;
        }
    }

    const string k_FadeOffAnim = "FadeOff";
    const string k_FadeOnAnim = "FadeOn";

    public ARPlaneManager planeManager
    {
        get { return m_PlaneManager; }
        set { m_PlaneManager = value; }
    }

    [SerializeField]
    Animator m_MoveDeviceAnimation;

    public Animator moveDeviceAnimation
    {
        get { return m_MoveDeviceAnimation; }
        set { m_MoveDeviceAnimation = value; }
    }

    [SerializeField]
    Animator m_TapToPlaceAnimation;

    public Animator tapToPlaceAnimation
    {
        get { return m_TapToPlaceAnimation; }
        set { m_TapToPlaceAnimation = value; }
    }

    static List<ARPlane> s_Planes = new List<ARPlane>();

    bool m_ShowingTapToPlace = false;

    bool m_ShowingMoveDevice = true;

    void OnEnable()
    {
        m_CameraManager = FindObjectOfType<ARCameraManager>();
        m_PlaneManager = FindObjectOfType<ARPlaneManager>();

        if (m_CameraManager != null)
            m_CameraManager.frameReceived += FrameChanged;

        //PlaceContent.OnPlacedContent += PlacedObject;
        PlaceMultipleContent.OnPlacedContent += PlacedObject;
    }

    void OnDisable()
    {
        if (m_CameraManager != null)
            m_CameraManager.frameReceived -= FrameChanged;

        PlaceContent.OnPlacedContent -= PlacedObject;
        //PlaceMultipleContent.OnPlacedContent -= PlacedObject;
    }

    void FrameChanged(ARCameraFrameEventArgs args)
    {
        if (PlanesFound() && m_ShowingMoveDevice)
        {
            if (moveDeviceAnimation)
                moveDeviceAnimation.SetTrigger(k_FadeOffAnim);

            if (tapToPlaceAnimation)
                tapToPlaceAnimation.SetTrigger(k_FadeOnAnim);

            m_ShowingTapToPlace = true;
            m_ShowingMoveDevice = false;
        }
    }

    bool PlanesFound()
    {
        if (planeManager == null)
            return false;

        return planeManager.trackables.count > 0;
    }

    void PlacedObject()
    {
        if (m_ShowingTapToPlace)
        {
            if (tapToPlaceAnimation)
                tapToPlaceAnimation.SetTrigger(k_FadeOffAnim);

            m_ShowingTapToPlace = false;
        }
    }

    public void EnableTapToPlaceAnimation()
    {
        tapToPlaceAnimation.SetTrigger(k_FadeOnAnim);
    }

    public void DisableTapToPlaceAnimation()
    {
        tapToPlaceAnimation.SetTrigger(k_FadeOffAnim);
    }
}