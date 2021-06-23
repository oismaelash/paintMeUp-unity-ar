using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class Utils
{
    public static bool IsClickOverUI(GraphicRaycaster graphicRaycaster)
    {
        if (graphicRaycaster is null)
        {
            Debug.Log("graphicRaycaster is null");
            return false;
        }
        //dont place content if pointer is over ui element
        PointerEventData data = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(data, results);
        return results.Count > 0;
    }
}