using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace StagePanel
{
    public class StageIndicatorBehaviour : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;

        private Color _color = Color.white;

        public void PassStage()
        {
            SetTextColor(_color);
        }

        public void SetTextColor(Color color)
        {
            _image.enabled = false;
            _text.color = color;
        }

        public void SetTextValue(int value)
        {
            _text.text = value.ToString();
        }

        public void SetSprite(Sprite sprite)
        {
            _image.enabled = true;
            if (sprite != null)
                _image.sprite = sprite;

            _text.color = Color.black;
        }

        private void OnValidate()
        {
            if (_image == null)
            {
                _image = GetComponent<Image>();
                if (_image == null)
                {
                    Debug.LogError("Failed To Set Image Of Stage Indicator");
                }
            }
            if (_text == null)
            {
                ObjectFinder.FindObjectInChilderenWithType(ref _text, transform);
            }
        }
    }
}