using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates projectiles in a wave.
public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Wave wave;

    private float secondsElapsed;
    private int waveIndex;

    private void Start()
    {
        secondsElapsed = 0;
        waveIndex = 0;
    }

    private void Update()
    {
        secondsElapsed += Time.deltaTime;
        if (secondsElapsed >= wave.secondsBetweenSpawns)
        {
            secondsElapsed = 0;
            SpawnProjectile();
        }
    }

    private void SpawnProjectile()
    {
        GameObject newObject = Instantiate(projectilePrefab, wave.startPoints[waveIndex].position, Quaternion.identity);
        Projectile projectile = newObject.GetComponent<Projectile>();

        projectile.pointsInPath = new List<Transform>();
        projectile.pointsInPath.Add(wave.startPoints[waveIndex]);
        projectile.pointsInPath.Add(wave.endPoints[waveIndex]);

        ++waveIndex;
    }


}
