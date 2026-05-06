using UnityEngine;
using UHFPS.Runtime;

public class UnlockDoorOnPickup : MonoBehaviour
{
    [Header("Door References")]
    public DynamicObject door;
    public InteractableItem interactable;

    private bool triggered = false;

    public void OpenDoor()
    {
        //  Prevent spam / multiple calls
        if (triggered) return;
        triggered = true;

        Debug.Log("Flashlight triggered door unlock (ONCE)");

        if (door != null)
        {
            door.isLocked = false;
        }
        else
        {
            Debug.LogWarning("Door reference missing!");
        }

        // small delay helps UHFPS process state cleanly
        Invoke(nameof(TriggerDoorInteract), 0.15f);
    }

    private void TriggerDoorInteract()
    {
        if (interactable != null)
        {
            interactable.OnInteract();
        }
        else
        {
            Debug.LogWarning("InteractableItem missing on door!");
        }
    }
}