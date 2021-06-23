using UnityEngine;

public class PartOfBody : MonoBehaviour
{
    private void OnMouseDown()
    {
        print($"PartOfBody: {GetComponentInParent<Transform>().name}");
        AppController.Instance.partOfBodyChooseEvent?.Invoke(GetComponent<Renderer>());
    }
}
