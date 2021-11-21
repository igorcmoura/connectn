using ConnectN.Inputs;
using UnityEngine;

namespace ConnectN
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerInputs _placementInputs;

        private Match _match;
        private DiskPlacementController _placementController;

        public void Construct(Match match, DiskPlacementController placementController)
        {
            _match = match;
            _placementController = placementController;
        }

        private void Awake()
        {
            _placementInputs.PlacementController = _placementController;
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
