using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public float beamLength = 40.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z > beamLength)
        {
            Destroy(gameObject);
        }
    }
}
