using ConnectN.Boards;
using ConnectN.Disks;
using System;
using UnityEngine;

namespace ConnectN
{
    public class DiskPlacementController
    {
        private IBoard _board;
        private int _currentPositionIndex;

        private IDisk _toBePlaced;
        private Vector3 _offset;

        public DiskPlacementController(IBoard board)
        {
            _board = board;
        }

        public void StartPlacement(IDisk toBePlaced, Vector3 offset)
        {
            if (_toBePlaced != null) throw new InvalidOperationException("Another placement is already happening, ending it is required to start a new one.");

            _toBePlaced = toBePlaced;
            _offset = offset;
            _currentPositionIndex = Mathf.RoundToInt(_board.AvailablePositions.Length / 2);

            var position = PositionIndexToWorldPosition(_currentPositionIndex);
            toBePlaced.SetCurrentPosition(position);
        }

        public void EndPlacement()
        {
            if (_toBePlaced != null) {
                _board.Insert(PositionIndexToPosition(_currentPositionIndex), _toBePlaced);
                _toBePlaced.Drop();
                _toBePlaced = null;
            }
        }

        public void MovePlacement(int delta)
        {
            if (_toBePlaced != null) {
                _currentPositionIndex += delta;
                var position = PositionIndexToWorldPosition(_currentPositionIndex);
                _toBePlaced.MoveTo(position);
            }
        }

        private Vector3 PositionIndexToWorldPosition(int index)
        {
            var height = _board.Height;
            return new Vector3(PositionIndexToPosition(index), height) + _offset;
        }

        private int PositionIndexToPosition(int index)
        {
            var availablePositions = _board.AvailablePositions;
            if (availablePositions.Length == 0) throw new InvalidOperationException("No available positions to start a placement.");
            Array.Sort(availablePositions);

            var boundedIndex = index % availablePositions.Length;
            if (boundedIndex < 0)
                boundedIndex += availablePositions.Length;

            return availablePositions[boundedIndex];
        }
    }
}
