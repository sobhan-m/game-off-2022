using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ViewManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] TextMeshProUGUI playerNameText;
    [SerializeField] TextMeshProUGUI enemyNameText;
    [SerializeField] Slider playerSlider;
    [SerializeField] Slider enemySlider;

    Player player;
    Enemy enemy;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        enemy = FindObjectOfType<Enemy>();

        playerSlider.minValue = 0;
        playerSlider.maxValue = player.GetMaxHP();
        playerSlider.value = player.GetCurrentHP();
        playerNameText.text = player.GetPlayerName();

        enemySlider.minValue = 0;
        enemySlider.maxValue = enemy.GetMaxPoints();
        enemySlider.value = enemy.GetCurrentPoints();
        enemyNameText.text = enemy.GetEnemyName();

    }

    private void Start()
    {
        messageText.text = "Testing...";
    }

    public void SetMessage(string text)
    {
        messageText.text = text;
    }

    public void UpdatePlayerSlider()
    {
        playerSlider.value = player.GetCurrentHP();
    }

    public void UpdateEnemySlider()
    {
        enemySlider.value = enemy.GetCurrentPoints();
    }
}
