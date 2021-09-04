using ConnectN;
using ConnectN.Disks;
using UnityEngine;

namespace Tests.DataBuilders.Disks
{
    public class DiskBuilder : DataBuilder<Disk>
    {
        private Player _player;

        public DiskBuilder ForPlayer(Player player)
        {
            _player = player;
            return this;
        }

        public override Disk Build()
        {
            var diskObj = new GameObject();
            diskObj.AddComponent<MeshRenderer>();
            var disk = diskObj.AddComponent<Disk>();
            if (_player != null)
                disk = (Disk)disk.Instantiate(_player);
            return disk;
        }
    }
}