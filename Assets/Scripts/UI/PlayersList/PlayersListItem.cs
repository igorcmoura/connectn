using UnityEngine;
using UnityEngine.UI;

namespace ConnectN.UI.PlayersList
{
    public class PlayersListItem : MonoBehaviour
    {
        [SerializeField] private Image _colorImage;
        [SerializeField] private Button _colorButton;
        [SerializeField] private SharedColorPicker _colorPicker;

        private void Start()
        {
            _colorButton.onClick.AddListener(() => {
                _colorPicker.SetCurrentOnValueChanged(_colorImage.color, color => _colorImage.color = color);
            });
        }
    }
}
