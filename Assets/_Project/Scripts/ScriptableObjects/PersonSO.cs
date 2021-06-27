using PaintMeUp.Prefabs;
using UnityEngine;

namespace PaintMeUp.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PersonDataSO", menuName = "ScriptableObjects/PersonData")]
    public class PersonSO : ScriptableObject
    {
        public string Name;
        public Sprite icon;
        public PersonModel PersonModel;
    }
}