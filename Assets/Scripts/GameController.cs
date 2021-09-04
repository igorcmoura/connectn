using ConnectN.Boards;
using ConnectN.Disks;
using ConnectN.Inputs;
using UnityEngine;

namespace ConnectN
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private int _numberToConnect = 4;
        [SerializeField] private int _boardColumns = 7;
        [SerializeField] private int _boardHeight = 6;
        [SerializeField] private Disk _diskPrefab;
        [SerializeField] private PlayerInputs _placementInputs;

        private Player[] _players;
        private IBoard _board;
        private Match _match;
        private DiskPlacementController _placementController;

        private void Awake()
        {

            _players = new Player[] {
                new Player(_diskPrefab, Color.red),
                new Player(_diskPrefab, Color.yellow)
            };
            _board = new Board(_boardColumns, _boardHeight);
            _match = new Match(_numberToConnect, _players, _board);
            _placementController = new DiskPlacementController(_board);
            _placementInputs.PlacementController = _placementController;
        }

        private void Start()
        {
            StartPlacement();
        }

        private void Update()
        {
            // go to placing disk state
            //
            //var disk = match.CurrentPlayer.GetDisk();

        }

        private void StartPlacement()
        {
            var disk = _match.CurrentPlayer.GetDisk();
            disk.OnFinishDropping += CheckFinished;
            _placementController.StartPlacement(disk, Vector3.zero);
        }

        private void CheckFinished()
        {
            if (!_match.IsFinished) {
                StartPlacement();
            }
        }
    }
}
