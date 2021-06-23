using System.Collections.Generic;
using UnityEngine;

public class PersonModel : MonoBehaviour
{
    [SerializeField] private List<Color> colorsOfBodyList;
    [SerializeField] private List<Renderer> partOfBodyList;
    [SerializeField] private Dictionary<string, Renderer> partOfBodyDict = new Dictionary<string, Renderer>();

    private void Start()
    {
        //partOfBodyList[0].material.color = X

        for (int index = 0; index < colorsOfBodyList.Count; index++)
        {
            //print($"key: {ColorUtility.ToHtmlStringRGBA(colorsOfBodyList[index])} | ");
            partOfBodyDict.Add(ColorUtility.ToHtmlStringRGBA(colorsOfBodyList[index]), partOfBodyList[index]);
        }
    }

    public List<Color> GetColors()
    {
        return colorsOfBodyList;
    }

    public Dictionary<string, Renderer> GetPartOfBodyDict()
    {
        return partOfBodyDict;
    }
}
