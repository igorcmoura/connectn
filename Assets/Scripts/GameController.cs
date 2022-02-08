using ConnectN.Boards;
using ConnectN.Disks;
using ConnectN.Inputs;
using UnityEngine;

namespace ConnectN
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerInputs _placementInputs;
        [SerializeField] private Disk _diskPrefab;
        [SerializeField] private MatchConfigurations _matchConfigurations;
        [SerializeField] private Board _board;

        private Match _match;
        private DiskPlacementController _placementController;

        private void Awake()
        {
            _board.Construct(_matchConfigurations.boardWidth, _matchConfigurations.boardHeight);
            _placementController = new DiskPlacementController(_board);
            _placementInputs.PlacementController = _placementController;
            _match = new Match(_matchConfigurations.numberOfConnections, _matchConfigurations.players, _board);
        }

        private void Start()
        {
            StartPlacement();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Application.Quit();
            }
        }

        private void StartPlacement()
        {
            var disk = _diskPrefab.Instantiate(_match.CurrentPlayer);
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
