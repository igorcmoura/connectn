using ConnectN.Disks;
using UnityEngine;

namespace ConnectN
{
    public class Player
    {
        public Color Color { get; private set; }
        private IDisk _diskPrefab;

        public Player(IDisk diskPrefab, Color color)
        {
            Color = color;
            _diskPrefab = diskPrefab;
        }

        public IDisk GetDisk()
        {
            return _diskPrefab.Instantiate(this);
        }

        public void ChoosePlacement()
        {

        }
    }
}
