using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float turnSpeed;
    public GameObject projectilePrefab;
    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private Transform[] gunSpawnPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
        transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);

        if(Input.GetKey(KeyCode.Space))
        {
            foreach(var spawnPoint in gunSpawnPoints)
            {
                Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }
}
