using UnityEngine;

namespace PaintMeUp.Screens
{
    public class StartScreen : MonoBehaviour
    {
        #region PUBLIC_METHODS

        public void OpenLink(string url)
        {
            Application.OpenURL(url);
        }

        #endregion
    }
}