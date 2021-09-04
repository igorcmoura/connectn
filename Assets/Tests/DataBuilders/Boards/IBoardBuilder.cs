using ConnectN.Boards;
using NSubstitute;

namespace Tests.DataBuilders.Boards
{
    public class IBoardBuilder
    {
        private int _width = 7;
        private int _height = 6;
        private int[] _availablePositions;

        public IBoardBuilder WithWidth(int width)
        {
            _width = width;
            return this;
        }

        public IBoardBuilder WithHeight(int height)
        {
            _height = height;
            return this;
        }

        public IBoardBuilder WithAvailablePositions(params int[] availablePositions)
        {
            _availablePositions = availablePositions;
            return this;
        }

        public IBoard Build()
        {
            var iBoard = Substitute.For<IBoard>();
            iBoard.Width.Returns(_width);
            iBoard.Height.Returns(_height);
            iBoard.AvailablePositions.Returns(_availablePositions ?? new int[] { 1 });
            return iBoard;
        }
    }
}
