using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float maxHealth;

    [Header("Track")]
    [SerializeField] int initialPositionIndex = 2;
    [SerializeField] List<Transform> initialTrackPositions;

    public Health playerHealth { get; private set; }
    public Track playerTrack { get; private set; }

    private void Awake()
    {
        playerHealth = new Health(maxHealth);
        playerTrack = new Track(initialTrackPositions, initialPositionIndex);

        this.transform.position = playerTrack.CurrentPosition().position;
    }

    private void Update()
    {
        if (playerHealth.IsDead())
        {
            Die();
        }
    }

    private void Die()
    {
        PlayerMovement playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerMovement.enabled = false;

        SceneController sceneController = FindObjectOfType<SceneController>();
        sceneController.LoadGameOver();
    }

}
