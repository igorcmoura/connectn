using ConnectN;
using ConnectN.Disks;
using NSubstitute;

namespace Tests.DataBuilders.Disks
{
    public class IDiskBuilder
    {
        private Player _owner;

        public IDiskBuilder ForPlayer(Player owner)
        {
            _owner = owner;
            return this;
        }

        public IDisk Build()
        {
            var disk = Substitute.For<IDisk>();
            disk.Owner.Returns(_owner ?? A.Player);
            disk.Instantiate(Arg.Any<Player>()).Returns(arg => An.IDisk.ForPlayer((Player) arg[0]).Build());
            return disk;
        }
    }
}
