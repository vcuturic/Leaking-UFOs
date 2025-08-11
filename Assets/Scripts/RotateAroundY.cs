using UnityEngine;
using UnityEngine.UIElements;

public class RotateAroundY : MonoBehaviour
{
    private float rotationSpeed = 85f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotationVector = new Vector3(0, 1, 0);
        transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime);
    }
}
