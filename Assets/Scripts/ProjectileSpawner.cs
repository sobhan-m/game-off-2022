using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates projectiles in a wave.
public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] List<Wave> waves;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float secondsBetweenWaves;

    private int waveIndex;
    private int projectileIndex;
    private float secondsSinceWave;
    private float secondsSinceProjectile;
    private List<List<Transform>> paths;
    private Wave currentWave;

    private void Start()
    {
        waveIndex = 0;
        projectileIndex = 0;
        secondsSinceWave = 0;
        secondsSinceProjectile = 0;
        paths = new List<List<Transform>>();
        currentWave = waves[waveIndex];
        SpawnWave();
    }

    private void SpawnProjectile(List<Transform> path)
    {
        GameObject currentProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        currentProjectile.GetComponent<Pathfinding>().path = path;
    }
    
    private void SpawnWave()
    {
        List<Transform> wavePaths = currentWave.paths;
        
        int i = 0;
        foreach (Transform currentPath in wavePaths)
        {
            paths.Add(new List<Transform>());
            for (int j = 0; j < currentPath.childCount; ++j)
            {
                paths[i].Add(currentPath.GetChild(j));
            }
            ++i;
        }
    }

    private void Update()
    {
        secondsSinceProjectile += Time.deltaTime;

        if (IsCurrentWaveFinished())
        {
            secondsSinceWave += Time.deltaTime;
            if (secondsSinceWave > secondsBetweenWaves && ++waveIndex < waves.Count)
            {
                ResetWave();
                currentWave = waves[waveIndex];
                SpawnWave();
            }
        }

        if (!IsCurrentWaveFinished() && secondsSinceProjectile > currentWave.secondsBetweenProjectiles)
        {
            SpawnProjectile(paths[projectileIndex]);
            secondsSinceProjectile = 0;
            ++projectileIndex;
        }
    }

    private bool IsCurrentWaveFinished()
    {
        return projectileIndex >= currentWave.paths.Count;
    }

    private void ResetWave()
    {
        projectileIndex = 0;
        secondsSinceWave = 0;
        secondsSinceProjectile = 0;
        paths = new List<List<Transform>>();
    }

}
