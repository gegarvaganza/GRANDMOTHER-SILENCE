using UnityEngine;
using UHFPS.Runtime;

public class BathroomDoorProximity : MonoBehaviour
{
    public DynamicObject door;

    private bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            OpenDoorSlight();
        }
    }

    void OpenDoorSlight()
    {
        if (door != null)
        {
            door.isLocked = false; // unlock
            door.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
        }
    }
}