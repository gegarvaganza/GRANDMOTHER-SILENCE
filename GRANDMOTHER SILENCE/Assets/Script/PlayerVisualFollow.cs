using UnityEngine;

public class PlayerVisualFollow : MonoBehaviour
{
    public Transform fpView; // HeroPlayer → FPView (camera)

    void LateUpdate()
    {
        // Only rotate Y axis (horizontal rotation)
        Vector3 euler = transform.eulerAngles;
        euler.y = fpView.eulerAngles.y;
        transform.eulerAngles = euler;
    }
}
