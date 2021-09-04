using ConnectN;
using ConnectN.Boards;
using ConnectN.Disks;
using NSubstitute;
using NUnit.Framework;
using System;
using Tests.DataBuilders;
using UnityEngine;

namespace Tests
{
    public class DiskPlacementControllerTest
    {
        public class StartPlacement
        {
            [Test]
            public void Starting_A_Placement_Places_The_Disk_At_The_Middle_Position()
            {
                IBoard board = An.IBoard
                    .WithHeight(10)
                    .WithAvailablePositions(2, 4, 5)
                    .Build();
                IDisk disk = An.IDisk.Build();
                DiskPlacementController placementController = A.DiskPlacementController.ForBoard(board);

                placementController.StartPlacement(disk, Vector3.zero);

                disk.Received().SetCurrentPosition(new Vector3(4, 10));
            }

            [Test]
            public void Starting_A_Placement_With_No_Available_Position_Throws_An_Exception()
            {
                IBoard board = An.IBoard
                    .WithAvailablePositions()
                    .Build();
                IDisk disk = An.IDisk.Build();
                DiskPlacementController placementController = A.DiskPlacementController.ForBoard(board);

                TestDelegate tryStartPlacement = () => { placementController.StartPlacement(disk, Vector3.zero); };

                Assert.Throws<InvalidOperationException>(tryStartPlacement);
            }

            [Test]
            public void Starting_A_Placement_Without_Finishing_Another_Started_Placement_Throws_An_Exception()
            {
                IDisk disk = An.IDisk.Build();
                DiskPlacementController placementController = A.DiskPlacementController;

                placementController.StartPlacement(disk, Vector3.zero);
                TestDelegate tryStartPlacement = () => { placementController.StartPlacement(disk, Vector3.zero); };

                Assert.Throws<InvalidOperationException>(tryStartPlacement);
            }
        }

        public class MovePlacement
        {
            [TestCase(1, 8)]
            [TestCase(-1, 5)]
            [TestCase(2, 10)]
            [TestCase(3, 11)]
            [TestCase(4, 12)]
            [TestCase(-2, 4)]
            [TestCase(-3, 2)]
            [TestCase(-4, 1)]
            public void Move_Placement_Moves_Disk_Delta_Available_Positions(int delta, int expectedPosition)
            {
                IBoard board = An.IBoard
                    .WithHeight(10)
                    .WithAvailablePositions(1, 2, 4, 5, 7, 8, 10, 11, 12)
                    .Build();
                IDisk disk = An.IDisk.Build();
                DiskPlacementController placementController = A.DiskPlacementController.ForBoard(board);

                placementController.StartPlacement(disk, Vector3.zero);
                placementController.MovePlacement(delta);

                disk.Received().MoveTo(new Vector3(expectedPosition, 10));
            }

            [TestCase(3, 1)]
            [TestCase(-3, 7)]
            [TestCase(4, 2)]
            [TestCase(-4, 5)]
            public void Move_Placement_Rotates_Back_To_The_Beginning_And_The_End_When_Delta_Goes_Out_Of_Bounds(int delta, int expectedPosition)
            {
                IBoard board = An.IBoard
                    .WithHeight(10)
                    .WithAvailablePositions(1, 2, 4, 5, 7)
                    .Build();
                IDisk disk = An.IDisk.Build();
                DiskPlacementController placementController = A.DiskPlacementController.ForBoard(board);

                placementController.StartPlacement(disk, Vector3.zero);
                placementController.MovePlacement(delta);

                disk.Received().MoveTo(new Vector3(expectedPosition, 10));
            }

            [Test]
            public void Move_Placement_Ensures_Order()
            {
                IBoard board = An.IBoard
                    .WithHeight(10)
                    .WithAvailablePositions(1, 7, 5, 4, 2)
                    .Build();
                IDisk disk = An.IDisk.Build();
                DiskPlacementController placementController = A.DiskPlacementController.ForBoard(board);

                placementController.StartPlacement(disk, Vector3.zero);
                placementController.MovePlacement(2);

                disk.Received().MoveTo(new Vector3(7, 10));
            }

            [Test]
            public void The_Disk_Is_Moved_Relative_To_The_Offset()
            {
                IBoard board = An.IBoard
                    .WithHeight(10)
                    .WithAvailablePositions(1, 2, 4, 5, 7)
                    .Build();
                IDisk disk = An.IDisk.Build();
                Vector3 offset = new Vector3(2, 4, 8);
                DiskPlacementController placementController = A.DiskPlacementController.ForBoard(board);

                placementController.StartPlacement(disk, offset);
                placementController.MovePlacement(2);

                disk.Received().MoveTo(new Vector3(2 + 7, 10 + 4, 8));
            }
        }

        public class EndPlacement
        {
            [Test]
            public void Ending_The_Placement_Drops_The_Disk()
            {
                IDisk disk = An.IDisk.Build();
                DiskPlacementController placementController = A.DiskPlacementController;

                placementController.StartPlacement(disk, Vector3.zero);
                placementController.EndPlacement();

                disk.Received().Drop();
            }

            [Test]
            public void Ending_The_Placement_Inserts_The_Disk_On_The_Board()
            {
                IBoard board = An.IBoard.WithAvailablePositions(1).Build();
                IDisk disk = An.IDisk.Build();
                DiskPlacementController placementController = A.DiskPlacementController.ForBoard(board);

                placementController.StartPlacement(disk, Vector3.zero);
                placementController.EndPlacement();

                board.Received().Insert(1, disk);
            }

            [Test]
            public void Moving_A_Placement_After_It_Has_Already_Finished_Does_Nothing()
            {
                IDisk disk = An.IDisk.Build();
                DiskPlacementController placementController = A.DiskPlacementController;

                placementController.StartPlacement(disk, Vector3.zero);
                placementController.EndPlacement();
                placementController.MovePlacement(1);

                disk.DidNotReceive().MoveTo(Arg.Any<Vector3>());
            }

            [Test]
            public void Ending_A_Placement_When_None_Has_Been_Started_Does_Nothing()
            {
                DiskPlacementController placementController = A.DiskPlacementController;

                TestDelegate tryEndPlacement = () => { placementController.EndPlacement(); };

                Assert.DoesNotThrow(tryEndPlacement);
            }
        }
    }
}