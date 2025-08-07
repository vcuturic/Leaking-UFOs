using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject owner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (owner != null)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), owner.GetComponent<SphereCollider>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player2") || other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.TakeDamage(3);
            }

            Destroy(gameObject);
        }
    }
}
