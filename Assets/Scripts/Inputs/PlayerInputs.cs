using UnityEngine;

namespace ConnectN.Inputs
{
    public class PlayerInputs : MonoBehaviour, IPlacementInputs
    {
        public DiskPlacementController PlacementController { get; set; }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                PlacementController?.MovePlacement(1);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                PlacementController?.MovePlacement(-1);
            }
            if (Input.GetKeyDown(KeyCode.Space)) {
                PlacementController?.EndPlacement();
            }
        }
    }
}
