using UnityEngine;

namespace DoorInteractionKit
{
    public class DoorInteractor : MonoBehaviour
    {
        [Header("Raycast Features")]
        [SerializeField] private float rayDistance = 5;

        [Header("Raycast Features")]
        [SerializeField] private KeyCode interactionKey;

        private DoorItem doorItem;
        private Camera _camera;

        void Start()
        {
            _camera = GetComponent<Camera>();
        }

        void Update()
        {
            if (Physics.Raycast(_camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f)), transform.forward, out RaycastHit hit, rayDistance))
            {
                var _doorItem = hit.collider.GetComponent<DoorItem>();
                if (_doorItem != null)
                {
                    doorItem = _doorItem;
                    doorItem.ShowObjectName(true);
                    HighlightCrosshair(true);
                }
                else
                {
                    ClearItem();
                }
            }
            else
            {
                ClearItem();
            }

            if (doorItem != null)
            {
                if (Input.GetKeyDown(interactionKey))
                {
                    doorItem.ObjectInteraction();
                }
            }
        }

        private void ClearItem()
        {
            if (doorItem != null)
            {
                doorItem.ShowObjectName(false);
                doorItem = null;
                HighlightCrosshair(false);
            }
        }

        void HighlightCrosshair(bool on)
        {
            DoorUIManager.instance.HighlightCrosshair(on);
        }
    }
}
