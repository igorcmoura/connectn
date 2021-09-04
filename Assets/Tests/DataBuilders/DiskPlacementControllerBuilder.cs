using ConnectN;
using ConnectN.Boards;

namespace Tests.DataBuilders
{
    public class DiskPlacementControllerBuilder : DataBuilder<DiskPlacementController>
    {
        private IBoard _board;

        public DiskPlacementControllerBuilder ForBoard(IBoard board)
        {
            _board = board;
            return this;
        }

        public override DiskPlacementController Build()
        {
            return new DiskPlacementController(_board ?? An.IBoard.Build());
        }
    }
}
