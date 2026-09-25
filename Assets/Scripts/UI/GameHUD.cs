using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private PlayerStats player;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text healthText;
    [SerializeField] private int targetScore = 80;

    private void OnEnable()
    {
        player.ScoreChanged += UpdateScore;
        player.HealthChanged += UpdateHealth;
    }

    private void Start()
    {
        UpdateScore(player.Score);
        UpdateHealth(player.Health);
    }

    private void UpdateScore(int score) => scoreText.text = $"Pontos: {score} / {targetScore}";
    private void UpdateHealth(int health) => healthText.text = $"Vidas: {health}";

    private void OnDisable()
    {
        player.ScoreChanged -= UpdateScore;
        player.HealthChanged -= UpdateHealth;
    }
}
