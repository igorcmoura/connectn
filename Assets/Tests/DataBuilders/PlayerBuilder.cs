using ConnectN;
using ConnectN.Disks;
using UnityEngine;

namespace Tests.DataBuilders
{
    public class PlayerBuilder : DataBuilder<Player>
    {
        private IDisk _diskPrefab;

        public PlayerBuilder WithDiskPrefab(IDisk diskPrefab)
        {
            _diskPrefab = diskPrefab;
            return this;
        }

        public override Player Build()
        {
            return new Player(_diskPrefab ?? An.IDisk.Build(), Color.red);
        }
    }
}
