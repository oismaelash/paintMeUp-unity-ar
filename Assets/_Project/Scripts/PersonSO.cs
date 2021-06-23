using UnityEngine;

[CreateAssetMenu(fileName = "PersonDataSO", menuName = "ScriptableObjects/PersonData")]
public class PersonSO : ScriptableObject
{
    public string Name;
    public Sprite icon;
    public PersonModel PersonModel;
}
