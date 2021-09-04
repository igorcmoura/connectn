using ConnectN.Disks;
using System;
using System.Collections.Generic;

namespace ConnectN.Boards
{
    public class Board : IBoard
    {

        private SortedDictionary<int, List<IDisk>> _columns = new SortedDictionary<int, List<IDisk>>();

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Board(int width, int height)
        {
            if (width <= 0) throw new ArgumentOutOfRangeException("width", width, "Value cannot be 0 or less.");
            if (height <= 0) throw new ArgumentOutOfRangeException("height", height, "Value cannot be 0 or less.");

            Width = width;
            Height = height;
        }

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
