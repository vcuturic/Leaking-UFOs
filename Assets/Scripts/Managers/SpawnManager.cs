using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public float powerUpSpawnTimeSeconds = 4f;
    private float floorXRange = 45f;
    private float floorZRange = 24f;
    private float powerUpHeight = 1.2f;
    private PlayerController[] players;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        players = FindObjectsByType<PlayerController>(FindObjectsSortMode.None);

        foreach(var player in players)
        {
            player.OnPowerUpPicked += StartCountdownForNextPickupSpawn;
        }

        Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartCountdownForNextPickupSpawn(object sender, PowerUpEventArgs pupArgs)
    {
        StartCoroutine(PowerUpSpawnCountdown());
    }

    IEnumerator PowerUpSpawnCountdown()
    {
        yield return new WaitForSeconds(powerUpSpawnTimeSeconds);
        Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-floorXRange, floorXRange);
        float spawnPosZ = Random.Range(-floorZRange, floorZRange);

        Vector3 randomPos = new Vector3(spawnPosX, powerUpHeight, spawnPosZ);

        return randomPos;
    }

    private void OnDestroy()
    {
        if (players == null) return;

        foreach (var player in players)
        {
            player.OnPowerUpPicked -= StartCountdownForNextPickupSpawn;
        }
    }
}
