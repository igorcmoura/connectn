using ConnectN;
using ConnectN.Disks;
using NUnit.Framework;
using Tests.DataBuilders;
using UnityEngine;

namespace Tests
{
    public class DiskTest
    {
        [Test]
        public void When_A_Disk_Is_Instantiated_It_Sets_The_New_Disk_Owner()
        {
            Player player = A.Player;
            Disk disk = A.Disk;

            var newDisk = disk.Instantiate(player);

            Assert.That(newDisk.Owner, Is.EqualTo(player));
        }
    }
}
