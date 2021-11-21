using ConnectN.Boards;
using ConnectN.Disks;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Tests.DataBuilders;

namespace Tests.Boards
{
    public class BoardColumnTest
    {
        public class Insert
        {
            [Test]
            public void Insert_Places_The_Disk_On_The_Column()
            {
                Disk disk = A.Disk;
                BoardColumn column = A.BoardColumn;

                column.Insert(disk);

            }
        }

        public class IsFull
        {
            [Test]
            public void IsFull_Returns_False_If_Not_Full()
            {
                BoardColumn column = A.BoardColumn;

                Assert.That(column.IsFull(), Is.False);
            }
        }
    }
}
