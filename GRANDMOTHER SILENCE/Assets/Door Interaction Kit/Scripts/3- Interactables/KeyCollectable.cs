using UnityEngine;

namespace DoorInteractionKit
{
    public class KeyCollectable : MonoBehaviour
    {
        [Header("Key ScriptableObject")]
        [SerializeField] private Key keyScriptable = null;

        [Header("Key Audio Clip")]
        [SerializeField] private Sound pickupSound = null;

        public void KeyPickup()
        {
            DoorInventory.instance.AddKey(keyScriptable);

            DoorAudioManager.instance.Play(pickupSound);
            gameObject.SetActive(false);
        }
    }
}
