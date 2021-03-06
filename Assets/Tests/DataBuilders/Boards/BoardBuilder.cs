using ConnectN.Boards;
using ConnectN.Disks;
using NSubstitute;
using UnityEditor;
using UnityEngine;

namespace Tests.DataBuilders.Boards
{
    public class BoardBuilder : DataBuilder<Board>
    {
        private int _width = 7;
        private int _height = 6;
        private (int, IDisk)[] _playedPositions;

        public BoardBuilder WithWidth(int width)
        {
            _width = width;
            return this;
        }

        public BoardBuilder WithHeight(int height)
        {
            _height = height;
            return this;
        }

        public BoardBuilder WithDimensions(int width, int height)
        {
            return WithWidth(width).WithHeight(height);
        }

        public BoardBuilder WithPlayedPositions(params (int, IDisk)[] playedPositions)
        {
            _playedPositions = playedPositions;
            return this;
        }

        public override Board Build()
        {
            var board = new GameObject().AddComponent<Board>();
            var renderer = new BoardRenderer(board, new GameObject(), new GameObject());
            board.Construct(_width, _height, renderer);

            foreach (var (columnIndex, disk) in _playedPositions ?? new (int, IDisk)[0]) {
                board.Insert(columnIndex, disk);
            }
            return board;
        }
    }
}
