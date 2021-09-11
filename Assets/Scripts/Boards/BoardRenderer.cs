using System.Collections.Generic;
using UnityEngine;

namespace ConnectN.Boards
{
    public class BoardRenderer
    {
        private GameObject _boardPiecePrefab;
        private GameObject _boardBasePiecePrefab;

        private Board _board;

        private List<GameObject> _boardPieces;

        public BoardRenderer(Board board, GameObject boardPiecePrefab, GameObject boardBasePiecePrefab)
        {
            _board = board;
            _boardPiecePrefab = boardPiecePrefab;
            _boardBasePiecePrefab = boardBasePiecePrefab;
        }

        public void Render()
        {
            CleanBoardPieces();

            for (int column = 0; column < _board.Width; column++) {
                var basePosition = new Vector3(column, -1, 0);
                var basePiece = Object.Instantiate(_boardBasePiecePrefab, basePosition, Quaternion.identity, _board.transform);
                _boardPieces.Add(basePiece);

                for (int line = 0; line < _board.Height; line++) {
                    var position = new Vector3(column, line, 0);
                    var piece = Object.Instantiate(_boardPiecePrefab, position, Quaternion.identity, _board.transform);
                    _boardPieces.Add(piece);
                }
            }
        }

        public void Destroy() => CleanBoardPieces();

        private void CleanBoardPieces()
        {
            if (_boardPieces != null) {
                foreach (var piece in _boardPieces) {
                    Object.Destroy(piece);
                }
            }
            _boardPieces = new List<GameObject>();
        }
    }
}
