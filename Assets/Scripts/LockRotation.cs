using UnityEngine;

public class LockRotation : MonoBehaviour
{
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
