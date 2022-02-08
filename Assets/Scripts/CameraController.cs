using ConnectN.Boards;
using UnityEngine;

namespace ConnectN
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Board _board;

        private void Start()
        {
            UpdateCamera();
            _board.OnSizeChange += UpdateCamera;
        }

        private void UpdateCamera()
        {
            var boardCenter = _board.transform.position + (new Vector3(_board.Width, _board.Height) / 2);
            // Magical numbers found through experimentation
            var zPosition = Mathf.Min(-(0.147f + 1.053f * _board.Width), -(2.1313f + 1.8687f * _board.Height));
            transform.position = new Vector3(boardCenter.x - 0.5f, boardCenter.y, zPosition);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T)) {
                UpdateCamera();
            }
        }
    }
}
