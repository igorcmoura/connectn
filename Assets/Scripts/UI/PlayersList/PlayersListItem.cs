using UnityEngine;
using UnityEngine.UI;
using System;

namespace ConnectN.UI.PlayersList
{
    public class PlayersListItem : MonoBehaviour
    {
        [SerializeField] private Image _colorImage;
        [SerializeField] private Button _colorButton;
        [SerializeField] private Button _deleteButton;
        [SerializeField] private SharedColorPicker _colorPicker;

        private Action _onDeleteClicked;

        public Color Color { get => _colorImage.color; }

        private void Start()
        {
            _colorButton.onClick.AddListener(() => {
                _colorPicker.SetCurrentOnValueChanged(_colorImage.color, color => _colorImage.color = color);
            });
            _deleteButton.onClick.AddListener(() => {
                _onDeleteClicked?.Invoke();
            });
        }

        public void Construct(SharedColorPicker colorPicker, Action onDeleteClicked)
        {
            _colorPicker = colorPicker;
            _onDeleteClicked += onDeleteClicked;
        }
    }
}
