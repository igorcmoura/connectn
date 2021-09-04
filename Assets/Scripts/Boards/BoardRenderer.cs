using UnityEngine;

namespace ConnectN.Boards
{
    [RequireComponent(typeof(GameController))]
    public class BoardRenderer : MonoBehaviour
    {
        private GameController _gameController;
        [SerializeField] private GameObject _boardPiecePrefab;

        private IBoard board;

        private void Start()
        {
            //board = _gameController.Board;
            //for (int column = 0; column <= board.Width; column++) {
            //    for (int line = 0; line <= board.Height; line++) {
            //        var position = new Vector3(column, line, 0);
            //        Instantiate(_boardPiecePrefab, position, Quaternion.identity, transform);
            //    }
            //}
        }
    }
}
