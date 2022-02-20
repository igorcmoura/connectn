using ConnectN.Boards;

namespace Tests.DataBuilders.Boards
{
    public class BoardColumnBuilder : DataBuilder<BoardColumn>
    {
        public override BoardColumn Build()
        {
            return new BoardColumn();
        }
    }
}
