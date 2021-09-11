using HSVPicker;
using UnityEngine;
using UnityEngine.Events;

namespace ConnectN.UI
{
    public class SharedColorPicker : MonoBehaviour
    {
        [SerializeField] private ColorPicker _colorPicker;

        private UnityAction<Color> _currentOnValueChanged;

        public void SetCurrentOnValueChanged(Color initialColor, UnityAction<Color> onValueChanged)
        {
            gameObject.SetActive(true);
            if (_currentOnValueChanged != null) {
                _colorPicker.onValueChanged.RemoveListener(_currentOnValueChanged);
            }
            _currentOnValueChanged = onValueChanged;
            _colorPicker.CurrentColor = initialColor;
            _colorPicker.onValueChanged.AddListener(onValueChanged);
        }
    }
}
