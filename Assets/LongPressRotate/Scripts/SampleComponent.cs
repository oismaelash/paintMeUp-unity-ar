using IsmaelNascimentoAsh.Assets;
using UnityEngine;

public class SampleComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RotateController.controlTypeEvent += (controlType) => { Debug.Log($"controlType selected: {controlType}"); };
        RotateController.resetPositionEvent += () => { Debug.Log($"Reset position object selected"); };
        RotateController.sideControlTypeEvent += (sideControlType) => { Debug.Log($"sideControlType selected: {sideControlType}"); };
    }

    // Update is called once per frame
    void Update()
    {

    }
}