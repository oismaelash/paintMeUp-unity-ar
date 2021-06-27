using System;
using UnityEngine;
using UnityEngine.UI;

namespace PaintMeUp.Utils
{
    public class ColorPicker : MonoBehaviour
    {
        RectTransform Rect;
        Texture2D ColorTexture;
        [SerializeField] Image colorPreview;
        [SerializeField] Color colorTest;

        public Action<Color> OnColorPickerEvent;

        private void Start()
        {
            Rect = GetComponent<RectTransform>();
        }

        private void Update()
        {

            if (RectTransformUtility.RectangleContainsScreenPoint(Rect, Input.mousePosition))
            {
                ColorTexture = GetComponent<RawImage>().texture as Texture2D;

                Vector2 delta;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(Rect, Input.mousePosition, null, out delta);
                //print($"mousePosition: {Input.mousePosition}");
                //print($"delta: {delta}");

                float width = Rect.rect.width;
                float height = Rect.rect.height;
                //print($"width: {width}");
                //print($"height: {height}");

                delta += new Vector2(width * .5f, height * .5f);
                //print($"offset delta: {delta}");

                float x = Mathf.Clamp(delta.x / width, 0f, 1f);
                float y = Mathf.Clamp(delta.y / height, 0f, 1f);
                //print($"x: {x}");
                //print($"y: {y}");

                int texX = Mathf.RoundToInt(x * ColorTexture.width);
                int texY = Mathf.RoundToInt(y * ColorTexture.height);

                Color color = ColorTexture.GetPixel(texX, texY);
                //print($"Color: {color}");

                OnColorPickerEvent?.Invoke(color);

                if (Input.GetMouseButtonDown(0))
                    colorPreview.color = color;

            }
        }


        [ContextMenu("ColorPickerTest")]
        private void ColorPickerTest()
        {
            colorPreview.color = colorTest;
            OnColorPickerEvent?.Invoke(colorTest);
            GetComponent<Button>().onClick.Invoke();
        }
    }
}