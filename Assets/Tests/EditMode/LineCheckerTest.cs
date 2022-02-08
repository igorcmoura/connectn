using ConnectN;
using ConnectN.Disks;
using NUnit.Framework;
using System.Collections.Generic;
using Tests.DataBuilders;

namespace Tests
{
    public class LineCheckerTest
    {
        [Test]
        public void Returns_False_If_No_Player_Has_Connected()
        {
            Player player1 = A.Player;
            Player player2 = A.Player;
            var placements = new Dictionary<(int, int), IDisk> { 
                { (3, 0), A.Disk.ForPlayer(player1).Build() },
                { (4, 0), A.Disk.ForPlayer(player2).Build() }
            };

            var hasWinner = LineChecker.CheckForAWinner(placements, 4, out Player winner);

            Assert.IsFalse(hasWinner);
            Assert.IsNull(winner);
        }

        [Test]
        public void Returns_True_If_A_Player_Connected_The_Number_To_Connect_Horizontally()
        {
            Player player = A.Player;
            var placements = new Dictionary<(int, int), IDisk> {
                { (1, 0), A.Disk.ForPlayer(player).Build() },
                { (2, 0), A.Disk.ForPlayer(player).Build() },
                { (3, 0), A.Disk.ForPlayer(player).Build() }
            };

            var hasWinner = LineChecker.CheckForAWinner(placements, 3, out Player winner);

            Assert.IsTrue(hasWinner);
            Assert.AreEqual(player, winner);
        }

        [Test]
        public void Returns_True_If_A_Player_Connected_The_Number_To_Connect_Vertically()
        {
            Player player = A.Player;
            var placements = new Dictionary<(int, int), IDisk> {
                { (1, 0), A.Disk.ForPlayer(player).Build() },
                { (1, 1), A.Disk.ForPlayer(player).Build() },
                { (1, 2), A.Disk.ForPlayer(player).Build() }
            };

            var hasWinner = LineChecker.CheckForAWinner(placements, 3, out Player winner);

            Assert.IsTrue(hasWinner);
            Assert.AreEqual(player, winner);
        }

        [Test]
        public void Returns_True_If_A_Player_Connected_The_Number_To_Connect_In_An_Ascending_Diagonal()
        {
            Player player = A.Player;
            var placements = new Dictionary<(int, int), IDisk> {
                { (1, 0), A.Disk.ForPlayer(player).Build() },
                { (2, 1), A.Disk.ForPlayer(player).Build() },
                { (3, 2), A.Disk.ForPlayer(player).Build() }
            };

            var hasWinner = LineChecker.CheckForAWinner(placements, 3, out Player winner);

            Assert.IsTrue(hasWinner);
            Assert.AreEqual(player, winner);
        }

        [Test]
        public void Returns_True_If_A_Player_Connected_The_Number_To_Connect_In_An_Descending_Diagonal()
        {
            Player player = A.Player;
            var placements = new Dictionary<(int, int), IDisk> {
                { (3, 0), A.Disk.ForPlayer(player).Build() },
                { (2, 1), A.Disk.ForPlayer(player).Build() },
                { (1, 2), A.Disk.ForPlayer(player).Build() }
            };

            var hasWinner = LineChecker.CheckForAWinner(placements, 3, out Player winner);

            Assert.IsTrue(hasWinner);
            Assert.AreEqual(player, winner);
        }

        [Test]
        public void Returns_False_If_Another_Player_Breaks_The_Line()
        {
            Player player1 = A.Player;
            Player player2 = A.Player;
            var placements = new Dictionary<(int, int), IDisk> {
                { (1, 0), A.Disk.ForPlayer(player1).Build() },
                { (2, 0), A.Disk.ForPlayer(player2).Build() },
                { (3, 0), A.Disk.ForPlayer(player1).Build() }
            };

            var hasWinner = LineChecker.CheckForAWinner(placements, 3, out Player winner);

            Assert.IsFalse(hasWinner);
            Assert.IsNull(winner);
        }
    }
}