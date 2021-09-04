using ConnectN;
using NUnit.Framework;
using Tests.DataBuilders;

namespace Tests
{
    public class PlayerTest
    {
        [Test]
        public void Get_Disk_Returns_A_Disk_From_The_Player()
        {
            Player player = A.Player;

            var disk = player.GetDisk();

            Assert.AreEqual(player, disk.Owner);
        }
    }
}
