using UnityEngine;

public class PlayerVisualSync : MonoBehaviour
{
    [Header("References")]
    public Transform fpView; // Assign FPView (camera)
    public Transform playerVisual; // Assign your PlayerVisual (parent of the body model)

    void LateUpdate()
    {
        if (fpView == null || playerVisual == null) return;

        // Rotate PlayerVisual horizontally to match camera
        Vector3 euler = playerVisual.eulerAngles;
        euler.y = fpView.eulerAngles.y;
        playerVisual.eulerAngles = euler;

        // Optional: you can also rotate the body slightly with vertical camera rotation
        // Uncomment below if you want slight upper body tilt
        // Vector3 tilt = playerVisual.localEulerAngles;
        // tilt.x = fpView.localEulerAngles.x;
        // playerVisual.localEulerAngles = tilt;
    }
}
