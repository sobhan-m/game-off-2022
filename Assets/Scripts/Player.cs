using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float maxHealth;

    [SerializeField] int initialPositionIndex;
    [SerializeField] List<Transform> initialTrackPositions;

    public Health playerHealth { get; private set; }
    public Track playerTrack { get; private set; }

    private void Awake()
    {
        playerHealth = new Health(maxHealth);
        playerTrack = new Track(initialTrackPositions, initialPositionIndex);

        this.transform.position = playerTrack.CurrentPosition().position;
    }

}
