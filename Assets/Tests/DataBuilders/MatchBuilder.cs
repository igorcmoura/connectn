using ConnectN;
using ConnectN.Boards;

namespace Tests.DataBuilders
{
    public class MatchBuilder : DataBuilder<Match>
    {
        private int _numberToConnect = 4;
        private Player[] _players;
        private Board _board;

        public MatchBuilder WithNumberToConnect(int numberToConnect)
        {
            _numberToConnect = numberToConnect;
            return this;
        }

        public MatchBuilder WithPlayers(params Player[] players)
        {
            _players = players;
            return this;
        }

        public MatchBuilder WithBoard(Board board)
        {
            _board = board;
            return this;
        }

        public override Match Build()
        {
            return new Match(
                _numberToConnect,
                _players ?? new Player[] { A.Player, A.Player },
                _board ?? A.Board
            );
        }
    }
}
