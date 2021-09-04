using ConnectN.Disks;
using System.Collections.Generic;

namespace ConnectN
{
    public static class LineChecker
    {
        private static readonly (int ColumnInc, int LineInc) HorizontalDirection = (1, 0);
        private static readonly (int ColumnInc, int LineInc) VerticalDirection = (0, 1);
        private static readonly (int ColumnInc, int LineInc) AscDiagonalDirection = (1, 1);
        private static readonly (int ColumnInc, int LineInc) DescDiagonalDirection = (1, -1);

        public static bool CheckForAWinner(Dictionary<(int Column, int Line), IDisk> placements, int numberToConnect, out Player winner)
        {
            foreach (var placement in placements) {
                var position = placement.Key;
                var disk = placement.Value;

                if (
                    CheckDirection(placements, position, HorizontalDirection, numberToConnect) || 
                    CheckDirection(placements, position, VerticalDirection, numberToConnect) ||
                    CheckDirection(placements, position, AscDiagonalDirection, numberToConnect) ||
                    CheckDirection(placements, position, DescDiagonalDirection, numberToConnect)
                    ) {
                    winner = disk.Owner;
                    return true;
                }
            }

            winner = null;
            return false;
        }

        private static bool CheckDirection(Dictionary<(int Column, int Line), IDisk> placements, (int Column, int Line) fromPosition, (int ColumnInc, int LineInc) direction, int numberToConnect)
        {
            var player = placements[fromPosition].Owner;
            for (int offset = 1; offset < numberToConnect; offset++) {
                var position = (
                    fromPosition.Column + (offset * direction.ColumnInc),
                    fromPosition.Line + (offset * direction.LineInc)
                );
                if (!placements.TryGetValue(position, out IDisk disk) || disk.Owner != player) {
                    return false;
                }
            }
            return true;
        }
    }
}
