using UnityEngine;
using UnityEngine.UI;

// OBSERVER: a UI se inscreve nos eventos de PlayerStats e GameSessionController.
// Ela atualiza a apresentacao sem ser chamada diretamente pela logica do jogador.
public class UIController : MonoBehaviour
{
    private PlayerStats player;
    private GameSessionController session;
    private Text scoreText;
    private Text healthText;
    private Text timeText;

    public void Initialize(
        PlayerStats playerStats,
        GameSessionController gameSession,
        Text score,
        Text health,
        Text time)
    {
        player = playerStats;
        session = gameSession;
        scoreText = score;
        healthText = health;
        timeText = time;

        player.ScoreChanged += UpdateScore;
        player.HealthChanged += UpdateHealth;
        session.TimeChanged += UpdateTime;

        UpdateScore(player.Score);
        UpdateHealth(player.Health);
        UpdateTime(session.TimeRemaining);
    }

    private void UpdateScore(int value)
    {
        scoreText.text = $"CRISTAIS  {value}/{session.TargetScore}";
    }

    private void UpdateHealth(int value)
    {
        healthText.text = $"VIDAS  {new string('♥', value)}";
    }

    private void UpdateTime(float value)
    {
        timeText.text = $"TEMPO  {Mathf.CeilToInt(value):00}";
    }

    private void OnDestroy()
    {
        if (player != null)
        {
            player.ScoreChanged -= UpdateScore;
            player.HealthChanged -= UpdateHealth;
        }

        if (session != null)
            session.TimeChanged -= UpdateTime;
    }
}
