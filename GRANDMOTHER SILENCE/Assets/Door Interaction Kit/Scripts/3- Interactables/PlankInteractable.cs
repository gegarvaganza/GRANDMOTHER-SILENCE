using UnityEngine;

namespace DoorInteractionKit
{
    public class PlankInteractable : MonoBehaviour
    {
        [Header("Door Controller Reference")]
        [SerializeField] private DoorInteractable doorObject;

        [Header("Removing Sound Effect")]
        [SerializeField] private Sound removePlankSound;

        public void RemovePlank()
        {
            doorObject.RemovePlank();

            DoorAudioManager.instance.Play(removePlankSound);
            gameObject.SetActive(false);
        }
    }
}

