using ConnectN;
using ConnectN.Boards;
using NUnit.Framework;
using Tests.DataBuilders;

namespace Tests
{
    public class MatchTest
    {
        [Test]
        public void A_Match_Starts_With_A_Player_Selected()
        {
            Player player1 = A.Player;
            Player player2 = A.Player;

            Match match = A.Match.WithPlayers(player1, player2);

            Assert.That(match.CurrentPlayer, Is.EqualTo(player1));
        }

        [Test]
        public void Creating_A_Match_With_Less_Than_2_Players_Throws_An_Exception()
        {
            Player player = A.Player;
            MatchBuilder matchBuilder = A.Match.WithPlayers(player);
            Assert.That(() => { matchBuilder.Build(); }, Throws.ArgumentException);
        }

        public class Play
        {
            [Test]
            public void When_A_Player_Plays_The_Next_Player_Is_Selected()
            {
                Player player1 = A.Player;
                Player player2 = A.Player;
                Board board = A.Board;
                Match match = A.Match
                    .WithPlayers(player1, player2)
                    .WithBoard(board);

                board.Insert(0, player1.GetDisk());

                Assert.That(match.CurrentPlayer, Is.EqualTo(player2));
            }

            [Test]
            public void When_The_Last_Player_On_The_List_Plays_The_First_Player_Is_Selected_Again()
            {
                Player player1 = A.Player;
                Player player2 = A.Player;
                Player player3 = A.Player;
                Board board = A.Board;
                Match match = A.Match
                    .WithPlayers(player1, player2, player3)
                    .WithBoard(board);

                Assert.That(match.CurrentPlayer, Is.EqualTo(player1));
                board.Insert(0, player1.GetDisk());
                Assert.That(match.CurrentPlayer, Is.EqualTo(player2));
                board.Insert(0, player2.GetDisk());
                Assert.That(match.CurrentPlayer, Is.EqualTo(player3));
                board.Insert(0, player3.GetDisk());
                Assert.That(match.CurrentPlayer, Is.EqualTo(player1));
            }
        }

        public class IsFinished
        {
            [Test]
            public void The_Match_Is_Not_Finished_If_No_Player_Satisfied_The_Winning_Condition()
            {
                Board board = A.Board;
                Match match = A.Match.WithBoard(board);

                board.Insert(0, match.CurrentPlayer.GetDisk());

                Assert.That(match.IsFinished, Is.False);
            }

            [Test]
            public void The_Match_Finishes_If_A_Players_Connects_The_Required_Number_Of_Disks()
            {
                Player player = A.Player;
                Board board = A.Board.WithPlayedPositions(
                    (1, player.GetDisk()),
                    (2, player.GetDisk())
                );
                Match match = A.Match
                    .WithNumberToConnect(3)
                    .WithPlayers(player, A.Player)
                    .WithBoard(board);

                board.Insert(3, player.GetDisk());

                Assert.That(match.IsFinished, Is.True);
                Assert.That(match.Winner, Is.EqualTo(player));
            }

            [Test]
            public void The_Match_Finishes_If_There_Is_No_Available_Position_To_Play()
            {
                Board board = A.Board
                    .WithDimensions(1, 1);
                Match match = A.Match
                    .WithNumberToConnect(2)
                    .WithBoard(board);

                board.Insert(0, match.CurrentPlayer.GetDisk());

                Assert.That(match.IsFinished, Is.True);
                Assert.That(match.Winner, Is.Null);
            }

            [Test]
            public void Trying_To_Play_After_The_Match_Has_Finished_Throws_An_Exception()
            {
                Player player = A.Player;
                Board board = A.Board.WithPlayedPositions(
                    (1, player.GetDisk()),
                    (2, player.GetDisk())
                );
                Match match = A.Match
                    .WithNumberToConnect(3)
                    .WithPlayers(player, A.Player)
                    .WithBoard(board);

                board.Insert(3, match.CurrentPlayer.GetDisk());
                TestDelegate tryPlay = () => { board.Insert(3, match.CurrentPlayer.GetDisk()); };

                Assert.That(tryPlay, Throws.InvalidOperationException);
            }

            [Test]
            public void If_The_Disk_Played_Is_Not_From_The_Current_Playing_Player_And_Exception_Is_Thrown()
            {
                Player player2 = A.Player;
                Board board = A.Board;
                Match match = A.Match
                    .WithPlayers(A.Player, player2)
                    .WithBoard(board);

                TestDelegate tryPlay = () => { board.Insert(0, player2.GetDisk()); };

                Assert.That(tryPlay, Throws.InvalidOperationException);
            }
        }
    }
}
