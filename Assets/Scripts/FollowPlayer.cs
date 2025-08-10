using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowPlayer : MonoBehaviour
{
    public GameObject target;
    public float followSpeed = 5f;
    public Vector3 offset = new Vector3(0f, 8f, -20f); 

    void LateUpdate() 
    {
        if (target != null)
        {
            // Calculate the desired position with offset
            Vector3 desiredPosition = target.transform.position + target.transform.TransformDirection(offset);

            // Smoothly move towards the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

            // Optional: Make the object look at the target
            transform.LookAt(target.transform);
        }
    }
}
