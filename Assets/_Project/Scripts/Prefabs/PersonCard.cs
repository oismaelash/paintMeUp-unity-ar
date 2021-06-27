using PaintMeUp.Controllers;
using PaintMeUp.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace PaintMeUp.Prefabs
{
    public class PersonCard : MonoBehaviour
    {
        [SerializeField] private PersonSO personSO;
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private bool isGameplay;
        [SerializeField] private Image colorCardPrefab;
        [SerializeField] private Transform colorCardContent;

        // Start is called before the first frame update
        void Start()
        {
            iconImage.sprite = personSO.icon;

            if (!isGameplay)
                nameText.text = personSO.Name;

            if (isGameplay)
            {
                personSO = AppController.Instance.PersonSOSelected;

                foreach (var color in personSO.PersonModel.GetColors())
                {
                    Image newColorCardPrefab = Instantiate(colorCardPrefab, colorCardContent);
                    newColorCardPrefab.color = color;
                }

                iconImage.sprite = personSO.icon;
                //nameText.text = personSO.Name;
            }

            GetComponent<Button>().onClick.AddListener(() =>
            {
                AppController.Instance.PersonSOSelected = personSO;
            });
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}