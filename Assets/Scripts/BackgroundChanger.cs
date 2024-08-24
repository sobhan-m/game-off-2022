using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BackgroundChanger : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private Image background;

    private void Awake()
    {
        background = GetComponent<Image>();
    }

    private void Start()
    {
        RandomizeBackground();
    }

    private void ChangeBackground(Sprite sprite)
    {
        background.sprite = sprite;
    }

    private void RandomizeBackground()
    {
        int i = Random.Range(0, sprites.Length);
        ChangeBackground(sprites[i]);
    }
}
