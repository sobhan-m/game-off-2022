using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] float damageAmount;
    [SerializeField] float secondsUntilDamage;
    [SerializeField] float secondsUntilDestroy;
    [SerializeField] Sprite[] sprites;
    [SerializeField] SpriteRenderer background;
    [SerializeField] SpriteRenderer lightning;
    private float secondsElapsed;
    private bool isDone = false;

    // Audio.
    [SerializeField] private AudioClip soundEffect;

    void Update()
    {
        if (!isDone)
        {
            secondsElapsed += Time.deltaTime;
            IncreaseGlow();
        }

        if (secondsElapsed > secondsUntilDamage)
        {
            DoLightning();
            Destroy(gameObject, secondsUntilDestroy);
            secondsElapsed = 0;
            isDone = true;
        }
    }

    private void IncreaseGlow()
    {
        float fraction = secondsElapsed / secondsUntilDamage;
        float fractionSquared = fraction * fraction;
        ChangeGlow(fractionSquared);
    }

    private void ChangeGlow(float alpha)
    {
        Color color = background.color;
        background.color = new Color(color.r, color.g, color.b, alpha);
    }

    private void PlayLightningSoundEffect()
    {
        AudioSource.PlayClipAtPoint(soundEffect, this.transform.position);
    }

    private void DoLightning()
    {
        PlayLightningSoundEffect();
        lightning.sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];
        TryToDealDamage();
    }

    private void TryToDealDamage()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(gameObject.transform.position, new Vector2(1, 12), 0, new Vector2(0, 0));
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.rigidbody.gameObject.TryGetComponent(out PlayerHealthManager player))
            {
                player.RetrieveHealth().Damage(damageAmount);
            }
        }
    }

}
