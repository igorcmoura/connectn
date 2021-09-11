using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ConnectN.UI
{
    public class HostGameMenuController : MonoBehaviour
    {

        [SerializeField] private TMP_InputField _boardWidthInput;
        [SerializeField] private TMP_InputField _boardHeightInput;
        [SerializeField] private TMP_InputField _numberOfConnections;

        private void Start()
        {
            _boardWidthInput.contentType = TMP_InputField.ContentType.IntegerNumber;
            _boardHeightInput.contentType = TMP_InputField.ContentType.IntegerNumber;
            _numberOfConnections.contentType = TMP_InputField.ContentType.IntegerNumber;
        }

        public void StartGame()
        {
            ValidateValues();
            LoadGameScene();
        }

        private void LoadGameScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        private void ValidateValues()
        {
            int boardWidth = ParseNumberInput(_boardWidthInput, "Width");
            int boardHeight = ParseNumberInput(_boardHeightInput, "Height");
            int numberOfConnections = ParseNumberInput(_numberOfConnections, "Connections");
        }

        private int ParseNumberInput(TMP_InputField input, string inputName)
        {
            if (!int.TryParse(input.text, out int inputValue)) {
                throw new InvalidCastException($"{inputName} must be a number.");
            }
            if (inputValue < 1) {
                throw new ArgumentOutOfRangeException(inputName, $"{inputName} must be at least 1.");
            }
            return inputValue;
        }
    }
}