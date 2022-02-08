using ConnectN;
using UnityEngine;

namespace Tests.DataBuilders
{
    public class PlayerBuilder : DataBuilder<Player>
    {
        private string _name = "Player";
        private Color _color = Color.red;

        public PlayerBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public PlayerBuilder WithColor(Color color)
        {
            _color = color;
            return this;
        }

        public override Player Build()
        {
            return new Player(_name, _color);
        }
    }
}
