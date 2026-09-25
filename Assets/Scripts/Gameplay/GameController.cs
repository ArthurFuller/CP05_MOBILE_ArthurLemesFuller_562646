using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerStats player;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private Text resultText;
    [SerializeField] private int targetScore = 80;

    private bool finished;

    private void OnEnable()
    {
        player.ScoreChanged += CheckScore;
        player.HealthChanged += CheckHealth;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        resultPanel.SetActive(false);
    }

    private void Update()
    {
        if (!finished)
            return;

        bool keyboard = Keyboard.current != null &&
            (Keyboard.current.rKey.wasPressedThisFrame || Keyboard.current.enterKey.wasPressedThisFrame);
        bool touch = Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame;

        if (keyboard || touch)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void CheckScore(int score)
    {
        if (score >= targetScore)
            Finish("VOCE VENCEU!\n\nPressione R ou toque para jogar novamente.");
    }

    private void CheckHealth(int health)
    {
        if (health <= 0)
            Finish("FIM DE JOGO\n\nPressione R ou toque para tentar novamente.");
    }

    private void Finish(string message)
    {
        if (finished)
            return;

        finished = true;
        resultText.text = message;
        resultPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        player.ScoreChanged -= CheckScore;
        player.HealthChanged -= CheckHealth;
    }
}
