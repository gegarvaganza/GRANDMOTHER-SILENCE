using UnityEngine;

namespace DoorInteractionKit
{
    public class DoorItem : MonoBehaviour
    {
        public enum DoorType
        {
            None,
            DoorOrDrawer,
            Key,
            Plank,
        }

        [Space(5)]
        [SerializeField] private bool enablePrompt = true;

        [Header("Icon Parameters")]
        [SerializeField] private bool showIcon = true;

        [Header("Key Prompt Parameters")]
        [SerializeField] private bool showKeyPrompt = true;
        [SerializeField] private string keyPromptText = "[LMB]";

        [Header("Interaction Name Parameters")]
        [SerializeField] private bool showInteractName = true;
        [SerializeField] private string interactionName = "Interact";

        [Header("Item Type")]
        [SerializeField] private DoorType doorType = DoorType.None;

        private DoorInteractable doorController;
        private KeyCollectable keyCollectable;
        private PlankInteractable plankInteractable;

        private void Awake()
        {
            switch (doorType)
            {
                case DoorType.DoorOrDrawer:
                    doorController = GetComponent<DoorInteractable>();
                    break;
                case DoorType.Key:
                    keyCollectable = GetComponent<KeyCollectable>();
                    break;
                case DoorType.Plank:
                    plankInteractable = GetComponent<PlankInteractable>();
                    break;
            }
        }

        public void ObjectInteraction()
        {
            switch (doorType)
            {
                case DoorType.DoorOrDrawer:
                    doorController?.CheckDoor();
                    break;
                case DoorType.Key:
                    keyCollectable?.KeyPickup();
                    break;
                case DoorType.Plank:
                    plankInteractable?.RemovePlank();
                    break;
            }
        }
        public void ShowObjectName(bool isActive)
        {
            if (enablePrompt)
            {
                DoorUIManager.instance.ShowName(isActive, showIcon, showKeyPrompt, showInteractName, keyPromptText, interactionName);
            }
        }
    }
}

