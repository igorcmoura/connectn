using ConnectN.Disks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ConnectN.Boards
{
    public class Board : MonoBehaviour, IBoard
    {
        [SerializeField] private int _width = 7;
        [SerializeField] private int _height = 6;
        [SerializeField] private GameObject _boardPiecePrefab;
        [SerializeField] private GameObject _boardBasePiecePrefab;

        public UnityAction OnSizeChange;

        private SortedDictionary<int, List<IDisk>> _columns = new SortedDictionary<int, List<IDisk>>();
        private BoardRenderer _renderer;

        public int Width { get => _width; }
        public int Height { get => _height; }

        public Dictionary<(int, int), IDisk> Placements {
            get {
                var placements = new Dictionary<(int, int), IDisk>();

                foreach (var columnPosition in _columns) {
                    var columnIndex = columnPosition.Key;
                    var column = columnPosition.Value;
                    for (int lineIndex = 0; lineIndex < column.Count; lineIndex++) {
                        placements.Add((columnIndex, lineIndex), column[lineIndex]);
                    }
                }

                return placements;
            }
        }

        public int[] AvailablePositions {
            get {
                var availablePositions = new List<int>();
                for (var index = 0; index < Width; index++) {
                    if (!_columns.ContainsKey(index) || _columns[index].Count < Height) {
                        availablePositions.Add(index);
                    }
                }
                return availablePositions.ToArray();
            }
        }

        public Action<IDisk> OnInsert { get; set; }

        public void Construct(int width, int height)
        {
            Construct(width, height, new BoardRenderer(this, _boardPiecePrefab, _boardBasePiecePrefab));
        }

        public void Construct(int width, int height, BoardRenderer renderer)
        {
            _width = width;
            _height = height;
            _renderer = renderer;
            UpdateBoard();
        }

        private void OnValidate()
        {
            _width = Math.Max(1, _width);
            _height = Math.Max(1, _height);
        }

        private void Start()
        {
            UpdateBoard();
        }

        private void OnDestroy()
        {
            _renderer.Destroy();
        }

        private void UpdateBoard()
        {
            _renderer.Render();
            OnSizeChange?.Invoke();
        }

        public void Insert(int columnIndex, IDisk disk)
        {
            if (columnIndex < 0) throw new ArgumentOutOfRangeException("columnIndex", columnIndex, "Cannot insert before position 0.");
            if (columnIndex >= Width) throw new ArgumentOutOfRangeException("columnIndex", columnIndex, "Cannot insert after last position.");

            if (!_columns.ContainsKey(columnIndex)) {
                _columns.Add(columnIndex, new List<IDisk>());
            }

            var column = _columns[columnIndex];
            if (column.Count >= Height) throw new ArgumentException("Cannot place in a full column.", "columnIndex");
            column.Add(disk);
            OnInsert?.Invoke(disk);
        }

        public IDisk DiskAtPosition(int columnIndex, int lineIndex)
        {
            if (_columns.TryGetValue(columnIndex, out List<IDisk> column) && column.Count > lineIndex)
                return column[lineIndex];
            return null;
        }
    }
}
