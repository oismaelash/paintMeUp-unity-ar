using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace IsmaelNascimentoAsh.Assets
{
    public class RotateController : MonoBehaviour
    {
        #region ENUMS

        public enum ControlType
        {
            ALL,
            AXIS_X,
            AXIS_Y,
            AXIS_Z,
            AXIS_X_Y,
            AXIS_X_Z,
            AXIS_Y_Z,
        }

        public enum SideControlType
        {
            NONE,
            X_POSITIVE,
            X_NEGATIVE,
            Y_POSITIVE,
            Y_NEGATIVE,
            Z_POSITIVE,
            Z_NEGATIVE,
        }

        #endregion

        #region VARIABLES

        [SerializeField] private ControlType controlType = ControlType.ALL;
        private SideControlType sideControlCurrent;
        [SerializeField] private Transform objectSelected;
        [SerializeField] private float velocityRotation;
        [Header("UI Controls")]
        [SerializeField] private GameObject axisXControls;
        [SerializeField] private GameObject axisYControls;
        [SerializeField] private GameObject axisZControls;
        [SerializeField] private Button resetRotationButton;
        [SerializeField] private EventTrigger axisXNegativeEventTriggerButton;
        [SerializeField] private EventTrigger axisXPositiveEventTriggerButton;
        [SerializeField] private EventTrigger axisYNegativeEventTriggerButton;
        [SerializeField] private EventTrigger axisYPositiveEventTriggerButton;
        [SerializeField] private EventTrigger axisZNegativeEventTriggerButton;
        [SerializeField] private EventTrigger axisZPositiveEventTriggerButton;

        // Actions
        public static Action<ControlType> controlTypeEvent;
        public static Action<SideControlType> sideControlTypeEvent;
        public static Action resetPositionEvent;

        #endregion

        #region MONOBEHAVIOUR_METHODS

        private void Start()
        {
            AttachReferences();
            VerificationControlType();
        }

        private void Update()
        {
            ControlTypeActionVerification();
        }

        #endregion

        #region PRIVATE_METHODS

        private void AttachReferences()
        {
            resetRotationButton.onClick.AddListener(ResetRotation);

            EventTriggerAddListener(axisXNegativeEventTriggerButton, new Dictionary<EventTriggerType, Action>()
        {
            {
                EventTriggerType.PointerDown,
                () =>
                {
                    SideControlCurrent(SideControlType.X_NEGATIVE);
                }
            },
            {
                EventTriggerType.PointerUp,
                () =>
                {
                    SideControlCurrent(SideControlType.NONE);
                }
            },
        });
            EventTriggerAddListener(axisXPositiveEventTriggerButton, new Dictionary<EventTriggerType, Action>()
        {
            {
                EventTriggerType.PointerDown,
                () =>
                {
                    SideControlCurrent(SideControlType.X_POSITIVE);
                }
            },
            {
                EventTriggerType.PointerUp,
                () =>
                {
                    SideControlCurrent(SideControlType.NONE);
                }
            },
        });

            EventTriggerAddListener(axisYNegativeEventTriggerButton, new Dictionary<EventTriggerType, Action>()
        {
            {
                EventTriggerType.PointerDown,
                () =>
                {
                    SideControlCurrent(SideControlType.Y_NEGATIVE);
                }
            },
            {
                EventTriggerType.PointerUp,
                () =>
                {
                    SideControlCurrent(SideControlType.NONE);
                }
            },
        });
            EventTriggerAddListener(axisYPositiveEventTriggerButton, new Dictionary<EventTriggerType, Action>()
        {
            {
                EventTriggerType.PointerDown,
                () =>
                {
                    SideControlCurrent(SideControlType.Y_POSITIVE);
                }
            },
            {
                EventTriggerType.PointerUp,
                () =>
                {
                    SideControlCurrent(SideControlType.NONE);
                }
            },
        });

            EventTriggerAddListener(axisZNegativeEventTriggerButton, new Dictionary<EventTriggerType, Action>()
        {
            {
                EventTriggerType.PointerDown,
                () =>
                {
                    SideControlCurrent(SideControlType.Z_NEGATIVE);
                }
            },
            {
                EventTriggerType.PointerUp,
                () =>
                {
                    SideControlCurrent(SideControlType.NONE);
                }
            },
        });
            EventTriggerAddListener(axisZPositiveEventTriggerButton, new Dictionary<EventTriggerType, Action>()
        {
            {
                EventTriggerType.PointerDown,
                () =>
                {
                    SideControlCurrent(SideControlType.Z_POSITIVE);
                }
            },
            {
                EventTriggerType.PointerUp,
                () =>
                {
                    SideControlCurrent(SideControlType.NONE);
                }
            },
        });
        }

        private void EventTriggerAddListener(EventTrigger eventTrigger, Dictionary<EventTriggerType, Action> eventTriggerTypesCallback)
        {
            foreach (KeyValuePair<EventTriggerType, Action> item in eventTriggerTypesCallback)
            {
                EventTrigger.Entry entryPointer = new EventTrigger.Entry
                {
                    eventID = item.Key
                };
                entryPointer.callback.AddListener((eventData) =>
                {
                    item.Value?.Invoke();
                });
                eventTrigger.triggers.Add(entryPointer);
            }
        }

        private void VerificationControlType()
        {
            switch (controlType)
            {
                case ControlType.AXIS_X:
                    axisYControls.SetActive(false);
                    axisZControls.SetActive(false);
                    break;
                case ControlType.AXIS_Y:
                    axisXControls.SetActive(false);
                    axisZControls.SetActive(false);
                    break;
                case ControlType.AXIS_Z:
                    axisXControls.SetActive(false);
                    axisYControls.SetActive(false);
                    break;
                case ControlType.AXIS_X_Y:
                    axisZControls.SetActive(false);
                    break;
                case ControlType.AXIS_X_Z:
                    axisYControls.SetActive(false);
                    break;
                case ControlType.AXIS_Y_Z:
                    axisXControls.SetActive(false);
                    break;
            }
        }

        private void SideControlCurrent(SideControlType sideControl)
        {
            sideControlCurrent = sideControl;
            sideControlTypeEvent?.Invoke(sideControlCurrent);
        }

        private void ControlTypeActionVerification()
        {
            float valueX;
            float valueY;
            float valueZ;
            switch (sideControlCurrent)
            {
                case SideControlType.X_NEGATIVE:
                    valueX = -(velocityRotation * Time.deltaTime);
                    objectSelected.Rotate(new Vector3(x: valueX,
                                                      y: 0,
                                                      z: 0));

                    break;
                case SideControlType.X_POSITIVE:
                    valueX = (velocityRotation * Time.deltaTime);
                    objectSelected.Rotate(new Vector3(x: valueX,
                                                       y: 0,
                                                       z: 0));

                    break;
                case SideControlType.Y_NEGATIVE:
                    valueY = -(velocityRotation * Time.deltaTime);
                    objectSelected.Rotate(new Vector3(x: 0,
                                                      y: valueY,
                                                      z: 0));
                    break;
                case SideControlType.Y_POSITIVE:
                    valueY = (velocityRotation * Time.deltaTime);
                    objectSelected.Rotate(new Vector3(x: 0,
                                                      y: valueY,
                                                      z: 0));
                    break;

                case SideControlType.Z_NEGATIVE:
                    valueZ = -(velocityRotation * Time.deltaTime);
                    objectSelected.Rotate(new Vector3(x: 0,
                                                      y: 0,
                                                      z: valueZ));
                    break;
                case SideControlType.Z_POSITIVE:
                    valueZ = (velocityRotation * Time.deltaTime);
                    objectSelected.Rotate(new Vector3(x: 0,
                                                      y: 0,
                                                      z: valueZ));
                    break;
            }
        }

        [ContextMenu("ResetRotation")]
        private void ResetRotation()
        {
            objectSelected.rotation = Quaternion.identity;
            resetPositionEvent?.Invoke();
        }

        #endregion

        #region PUBLIC_METHODS

        public void SetObjectForRotation(Transform transform)
        {
            objectSelected = transform;
        }

        #endregion
    }
}