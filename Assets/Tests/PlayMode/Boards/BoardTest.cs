using ConnectN.Boards;
using ConnectN.Disks;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Tests.DataBuilders;
using Tests.DataBuilders.Boards;

namespace Tests.Boards
{
    public class BoardTest
    {
        public class Insert
        {
            [Test]
            public void Insert_Places_The_Disk_On_The_Board()
            {
                Board board = A.Board;
                IDisk disk = An.IDisk.Build();

                board.Insert(0, disk);
                var placements = board.Placements;

                var expected = new Dictionary<(int, int), IDisk> { { (0, 0), disk } };
                Assert.AreEqual(expected, placements);
            }

            [Test]
            public void Insert_Calls_OnInsert_Action()
            {
                Board board = A.Board;
                IDisk disk = An.IDisk.Build();

                var action = Substitute.For<Action<IDisk>>();
                board.OnInsert += action;
                board.Insert(0, disk);

                action.Received().Invoke(disk);
            }

            [Test]
            public void Inserting_Two_Disks_In_The_Same_Position_Places_One_Disk_Above_The_Other()
            {
                Board board = A.Board;
                IDisk disk1 = An.IDisk.Build();
                IDisk disk2 = An.IDisk.Build();

                board.Insert(0, disk1);
                board.Insert(0, disk2);
                var placements = board.Placements;

                var expected = new Dictionary<(int, int), IDisk>
                {
                    { (0, 0), disk1 },
                    { (0, 1), disk2 }
                };
                Assert.AreEqual(expected, placements);
            }

            [Test]
            public void Inserting_In_A_Position_Below_Minimum_Throws_An_Exception()
            {
                Board board = A.Board;
                IDisk disk = An.IDisk.Build();

                TestDelegate tryInsert = () => { board.Insert(-1, disk); };

                Assert.Throws<ArgumentOutOfRangeException>(tryInsert);
            }

            [Test]
            public void Inserting_In_A_Position_Above_Maximum_Throws_An_Exception()
            {
                Board board = A.Board.WithWidth(2);
                IDisk disk = An.IDisk.Build();

                TestDelegate tryInsert = () => { board.Insert(2, disk); };

                Assert.Throws<ArgumentOutOfRangeException>(tryInsert);
            }

            [Test]
            public void Inserting_In_An_Already_Full_Column_Throws_An_Exception()
            {
                Board board = A.Board.WithHeight(1);
                IDisk disk1 = An.IDisk.Build();
                IDisk disk2 = An.IDisk.Build();

                board.Insert(0, disk1);
                TestDelegate tryInsert = () => { board.Insert(0, disk2); };

                Assert.Throws<ArgumentException>(tryInsert);
            }
        }

        public class DiskAtPosition
        {
            [Test]
            public void Getting_A_Disk_At_A_Position_Containing_A_Disk_Returns_The_Disk()
            {
                Board board = A.Board;
                IDisk disk = An.IDisk.Build();

                board.Insert(0, disk);
                var actual = board.DiskAtPosition(0, 0);

                Assert.AreEqual(disk, actual);
            }

            [Test]
            public void Getting_A_Disk_At_A_Position_Containing_Nothing_Returns_Null()
            {
                Board board = A.Board;

                var actual = board.DiskAtPosition(0, 0);

                Assert.IsNull(actual);
            }
        }

        public class AvailablePositions
        {
            [Test]
            public void Available_Position_Return_All_Positions_Which_Are_Not_Full()
            {
                IDisk disk = An.IDisk.Build();
                Board board = A.Board
                    .WithWidth(3)
                    .WithHeight(1)
                    .WithPlayedPositions((1, disk));

                var availablePositions = board.AvailablePositions;

                var expected = new int[] { 0, 2 };
                Assert.That(availablePositions, Is.EqualTo(expected));
            }
        }
    }
}
