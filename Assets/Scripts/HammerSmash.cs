using UnityEngine;

public class HammerSmash : MonoBehaviour
{
    public float duration = 0.3f;
    private float elapsed = 0f;
    private Quaternion startRotation;
    private Quaternion endRotation;

    void Start()
    {
        startRotation = Quaternion.Euler(-90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        endRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation = startRotation;
    }

    void Update()
    {
        if (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
        }
    }
}
