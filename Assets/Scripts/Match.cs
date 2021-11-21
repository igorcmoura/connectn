using ConnectN.Boards;
using ConnectN.Disks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ConnectN
{
    public class Match
    {
        public Player CurrentPlayer {
            get => _currentPlayer.Value;
        }
        public bool IsFinished { get; private set; } = false;
        public Player Winner { get; private set; }

        private int _numberToConnect;
        private LinkedListNode<Player> _currentPlayer;
        private IBoard _board;

        public Match(int numberToConnect, Player[] players, IBoard board)
        {
            if (players.Length < 2) throw new ArgumentException("At least two players are required for a match.", "players");

            _numberToConnect = numberToConnect;

            var playersLinkedList = new LinkedList<Player>(players);
            _currentPlayer = playersLinkedList.First;

            _board = board;
            _board.OnInsert += OnPlayerPlayed;
        }

        private void OnPlayerPlayed(IDisk disk)
        {
            if (IsFinished) throw new InvalidOperationException("Cannot play after a game has finished.");
            if (disk.Owner != CurrentPlayer) throw new InvalidOperationException("The disk placed on the board is not from the current player.");

            if (LineChecker.CheckForAWinner(_board.Placements, _numberToConnect, out Player winner)) {
                IsFinished = true;
                Winner = winner;
            }
            foreach (var pos in _board.AvailablePositions) {
                Debug.Log(pos);
            }
            if (_board.AvailablePositions.Length == 0) {
                IsFinished = true;
            }

            if (!IsFinished) {
                _currentPlayer = _currentPlayer.Next ?? _currentPlayer.List.First;
            }
        }
    }
}
