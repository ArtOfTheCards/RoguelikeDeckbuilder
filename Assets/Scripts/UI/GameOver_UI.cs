using System.Collections;
using TMPro;
using UnityEngine;

public class GameOver_UI : MonoBehaviour
{
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private Damagable player;
    [SerializeField] private Damagable enemy;
    [SerializeField] private TextMeshProUGUI text;

    private string message; // Declare the message variable

    private void Awake()
    {
        // Ensure the game over UI is initially inactive
        gameObject.SetActive(false);
    }

    private void Update()
    {
        // Check for game over conditions
        CheckDamagable();
    }

    private void CheckDamagable()
    {
        // If player's health is zero, game over, player loses
        if (player.currentHealth == 0)
        {
            message = "Game Over, You Lose";
            GameOver();
        }

        // If enemy's health is zero, game over, player wins
        if (enemy.currentHealth == 0)
        {
            message = "You Win";
            GameOver();
        }
    }

    private void GameOver()
    {
        // Display the appropriate message
        text.text = message;

        // Deactivate the option menu
        optionMenu.SetActive(false);

        // Pause the game
        Time.timeScale = 0;

        // Ensure the game over UI is visible
        gameObject.SetActive(true);
    }
}
