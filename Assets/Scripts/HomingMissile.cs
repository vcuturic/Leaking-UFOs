using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HomingMissile : MonoBehaviour
{
    public float turnSpeed = 10;
    public float rocketVelocity = 15;
    public float lifeTime = 30f;
    private float missileRange = 2f;
    private float missileDamage = 20f;

    private GameObject enemyPlayer;
    private Rigidbody rb;

    public void Initialize(GameObject enemyPlayer)
    {
        this.enemyPlayer = enemyPlayer;

        rb = GetComponent<Rigidbody>();

        Destroy(gameObject, lifeTime);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * rocketVelocity;

        var rocketTargetRotation = Quaternion.LookRotation(enemyPlayer.transform.position - transform.position);

        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rocketTargetRotation, turnSpeed));

        if(IsMissileInRangeOf(enemyPlayer, missileRange))
        {
            Debug.Log("Enemy hit!");
            PlayerController enemyScript = enemyPlayer.GetComponent<PlayerController>();
            enemyScript.TakeDamage(missileDamage);
            Destroy(gameObject);
        }
    }

    bool IsMissileInRangeOf(GameObject target, float range)
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance <= range;
    }
}
