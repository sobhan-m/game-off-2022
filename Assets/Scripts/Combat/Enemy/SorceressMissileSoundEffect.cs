using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorceressMissileSoundEffect : MonoBehaviour
{
    [SerializeField] private AudioClip soundEffect;

    private void Start()
    {
        AudioSource.PlayClipAtPoint(soundEffect, transform.position);
    }
}
