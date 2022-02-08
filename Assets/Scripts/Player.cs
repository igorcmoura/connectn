using UnityEngine;

namespace ConnectN
{
    public class Player
    {
        public string Name { get; private set; }
        public Color Color { get; private set; }

        public Player(string name, Color color)
        {
            Name = name;
            Color = color;
        }
    }
}
