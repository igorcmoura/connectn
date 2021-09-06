using ConnectN.Boards;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConnectN
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Board _board;

        private void Start()
        {
            var boardCenter = _board.transform.position + (new Vector3(_board.Width, _board.Height) / 2);
            transform.position = new Vector3(boardCenter.x - 0.5f, boardCenter.y, transform.position.z);
        }
    }
}
