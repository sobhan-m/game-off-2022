using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates projectiles in a wave.
public class ProjectileSpawner : MonoBehaviour
{
    [Header("Projectiles")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] public float projectileSpeed;


    [Header("Waves")]
    [SerializeField] List<Wave> waves;
    [SerializeField] List<Transform> pathWrappers;
    [SerializeField] float secondsBetweenWaves;

    private int currentWaveIndex;
    private float secondsSinceWaveStarted;

    private void Start()
    {
        currentWaveIndex = 0;
        secondsSinceWaveStarted = 0;
    }

    private void Update()
    {
        if (IsCurrentWaveFinished() && currentWaveIndex < waves.Count)
        {
            SpawnWave(waves[currentWaveIndex]);
            currentWaveIndex++;
        }

        secondsSinceWaveStarted += Time.deltaTime;
    }

    private void SpawnProjectile(List<Transform> path)
    {
        GameObject currentProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Pathfinding pathfinding = currentProjectile.GetComponent<Pathfinding>();
        pathfinding.BeginPath(path, projectileSpeed);
    }
    
    private void SpawnWave(Wave wave)
    {
        // Debug.Log("ProjectileSpawner.SpawnWave(): Seconds Since Last Wave " + secondsSinceWaveStarted + " s.");

        for (int i = 0; i < wave.doesTrackHaveProjectile.Count; ++i)
        {
            if (wave.doesTrackHaveProjectile[i])
            {
                SpawnProjectile(ConstructPath(pathWrappers[i]));
            }
        }

        secondsSinceWaveStarted = 0;
    }

    private List<Transform> ConstructPath(Transform pathWrapper)
    {
        List<Transform> path = new List<Transform>();
        
        for (int i = 0; i < pathWrapper.childCount; ++i)
        {
            path.Add(pathWrapper.GetChild(i));
        }

        return path;
    }


    private bool IsCurrentWaveFinished()
    {
        return secondsSinceWaveStarted >= secondsBetweenWaves;
    }
}
