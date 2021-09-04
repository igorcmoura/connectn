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
                { (3, 0), player1.GetDisk() },
                { (4, 0), player2.GetDisk() }
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
                { (1, 0), player.GetDisk() },
                { (2, 0), player.GetDisk() },
                { (3, 0), player.GetDisk() }
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
                { (1, 0), player.GetDisk() },
                { (1, 1), player.GetDisk() },
                { (1, 2), player.GetDisk() }
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
                { (1, 0), player.GetDisk() },
                { (2, 1), player.GetDisk() },
                { (3, 2), player.GetDisk() }
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
                { (3, 0), player.GetDisk() },
                { (2, 1), player.GetDisk() },
                { (1, 2), player.GetDisk() }
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
                { (1, 0), player1.GetDisk() },
                { (2, 0), player2.GetDisk() },
                { (3, 0), player1.GetDisk() }
            };

            var hasWinner = LineChecker.CheckForAWinner(placements, 3, out Player winner);

            Assert.IsFalse(hasWinner);
            Assert.IsNull(winner);
        }
    }
}