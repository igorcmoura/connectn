using ConnectN.Boards;
using ConnectN.Disks;
using System.Collections;
using UnityEngine;

namespace ConnectN
{
    public class TestPlacement : MonoBehaviour
    {
        public float waitTime = 1f;
        public Disk diskPrefab;
        private DiskPlacementController _pc;

        private void Awake()
        {
            Board board = new Board(7, 6);
            _pc = new DiskPlacementController(board);
        }

        private void Start()
        {
            StartPlacement();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                _pc.MovePlacement(1);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                _pc.MovePlacement(-1);
            }
            if (Input.GetKeyDown(KeyCode.Space)) {
                _pc.EndPlacement();
                StartCoroutine(WaitAndRestartPlacement());
            }
        }

        private IEnumerator WaitAndRestartPlacement()
        {
            yield return new WaitForSeconds(waitTime);
            StartPlacement();
        }

        private void StartPlacement()
        {
            var disk = Instantiate(diskPrefab);
            _pc.StartPlacement(disk, transform.position);
        }
    }
}
