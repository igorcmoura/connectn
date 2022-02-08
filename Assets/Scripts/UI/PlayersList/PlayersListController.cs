using UnityEngine;
using System.Collections.Generic;

namespace ConnectN.UI.PlayersList
{
    public class PlayersListController : MonoBehaviour
    {
        [SerializeField] private int _minimumPlayers = 2;
        [SerializeField] private PlayersListItem _playersListItemPrefab;
        [SerializeField] private SharedColorPicker _colorPicker;

        private List<PlayersListItem> _playersList;

        public Player[] PlayersList {
            get {
                var playersList = new List<Player>();
                foreach (var playerItem in _playersList) {
                    var player = new Player(playerItem.name, playerItem.Color);
                    playersList.Add(player);
                }
                return playersList.ToArray();
            }
        }

        private void Start()
        {
            _playersList = new List<PlayersListItem>();
            for (int i = 0; i < _minimumPlayers; i++) {
                CreateNewPlayer();
            }
        }

        private void OnDestroy()
        {
            for (int i = _playersList.Count - 1; i >= 0; i--) {
                var player = _playersList[i];
                _playersList.RemoveAt(i);
                Destroy(player.gameObject);
            }
        }

        public void CreateNewPlayer()
        {
            var newPlayer = Instantiate(_playersListItemPrefab, Vector3.zero, Quaternion.identity, transform);
            newPlayer.Construct(_colorPicker, () => { RemovePlayer(newPlayer); });
            _playersList.Add(newPlayer);
        }

        public void RemovePlayer(PlayersListItem player)
        {
            if (_playersList.Count > _minimumPlayers) {
                _playersList.Remove(player);
                Destroy(player.gameObject);
            }
        }
    }
}
