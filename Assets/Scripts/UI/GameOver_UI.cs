using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver_UI : MonoBehaviour
{
    [SerializeField] private AsyncLoader loader;
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject enemy;
    [SerializeField] private Damagable player_dmg;
    [SerializeField] private Damagable enemy_dmg;
    [SerializeField] private TextMeshProUGUI text;

    private string message; // Declare the message variable

    private void Awake()
    {
        // Ensure the game over UI is initially inactive
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        player_dmg = player.GetComponentInChildren<Damagable>();
        enemy_dmg = enemy.GetComponentInChildren<Damagable>();

        loader = GameObject.FindObjectOfType<AsyncLoader>();
        gameOver.SetActive(false);

    }

    private void Update()
    {
        // Check for game over conditions
        CheckDamagable();
    }

    private void CheckDamagable()
    {
        Debug.Log("Player's HealthBar: " + player_dmg.currentHealth + "\n");
        Debug.Log("Enemies's HealthBar: " + enemy_dmg.currentHealth + "\n");
        // If player's health is zero, game over, player loses
        if (player_dmg.currentHealth == 0)
        {
            message = "Game Over, You Lose";
            GameOver();
        }

        // If enemy's health is zero, game over, player wins
        if (enemy_dmg.currentHealth == 0)
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
        gameOver.SetActive(true);

        player.SetActive(false);
        enemy.SetActive(false);
    }


    public void Restart()
    {
        loader.LoadlevelBtn("Arena");
    }

    public void Menu()
    {
        loader.LoadlevelBtn("TitleScreen");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
