using ConnectN.UI.PlayersList;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Unity.Netcode;

namespace ConnectN.UI
{
    public class HostGameMenuController : MonoBehaviour
    {

        [SerializeField] private TMP_InputField _boardWidthInput;
        [SerializeField] private TMP_InputField _boardHeightInput;
        [SerializeField] private TMP_InputField _numberOfConnections;

        [SerializeField] private PlayersListController _playersListController;

        [SerializeField] private MatchConfigurations matchConfigurations;

        private void Start()
        {
            _boardWidthInput.contentType = TMP_InputField.ContentType.IntegerNumber;
            _boardHeightInput.contentType = TMP_InputField.ContentType.IntegerNumber;
            _numberOfConnections.contentType = TMP_InputField.ContentType.IntegerNumber;
        }

        public void StartHost()
        {
            NetworkManager.Singleton.StartHost();
        }

        public void StartClient()
        {
            NetworkManager.Singleton.StartClient();
        }

        public void StartGame()
        {
            SaveValues();
            LoadGameScene();
        }

        private void LoadGameScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        private void SaveValues()
        {
            int boardWidth = ParseNumberInput(_boardWidthInput, "Width");
            int boardHeight = ParseNumberInput(_boardHeightInput, "Height");
            int numberOfConnections = ParseNumberInput(_numberOfConnections, "Connections");

            matchConfigurations.boardWidth = boardWidth;
            matchConfigurations.boardHeight = boardHeight;
            matchConfigurations.numberOfConnections = numberOfConnections;
            matchConfigurations.players = _playersListController.PlayersList;
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
