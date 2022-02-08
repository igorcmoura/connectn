using UnityEngine;

namespace ConnectN
{
    [CreateAssetMenu(fileName = "NewMatchConfigurations", menuName = "Scene Data/Match Configurations")]
    public class MatchConfigurations : ScriptableObject
    {
        public int boardWidth;
        public int boardHeight;
        public int numberOfConnections;

        public Player[] players;
    }
}
