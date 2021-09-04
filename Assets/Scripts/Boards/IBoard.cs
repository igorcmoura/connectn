using ConnectN.Disks;
using System;
using System.Collections.Generic;

namespace ConnectN.Boards
{
    public interface IBoard
    {
        int Width { get; }
        int Height { get; }
        int[] AvailablePositions { get; }
        Dictionary<(int, int), IDisk> Placements { get; }
        Action<IDisk> OnInsert { get; set; }

        void Insert(int columnIndex, IDisk disk);
    }
}
