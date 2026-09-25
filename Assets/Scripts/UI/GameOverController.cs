using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// OBSERVER: reage ao evento GameEnded sem o GameSessionController conhecer esta tela.
public class GameOverController : MonoBehaviour
{
    private GameSessionController session;
    private GameObject panel;
    private Text titleText;
    private Text resultText;
    private bool canRestart;
    private float unscaledOpenedAt;

    public void Initialize(GameSessionController gameSession, GameObject gameOverPanel, Text title, Text result)
    {
        session = gameSession;
        panel = gameOverPanel;
        titleText = title;
        resultText = result;

        panel.SetActive(false);
        session.GameEnded += ShowResult;
    }

    private void Update()
    {
        if (!canRestart || Time.unscaledTime - unscaledOpenedAt < 0.3f)
            return;

        bool keyboardRestart = Keyboard.current != null &&
            (Keyboard.current.rKey.wasPressedThisFrame || Keyboard.current.enterKey.wasPressedThisFrame);

        bool touchRestart = Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame;

        if (keyboardRestart || touchRestart)
            Restart();
    }

    private void ShowResult(bool victory, int score)
    {
        titleText.text = victory ? "VOCÊ VENCEU!" : "FIM DE JOGO";
        resultText.text = victory
            ? $"Meta alcançada com {score} pontos.\n\nToque na tela ou pressione R para jogar novamente."
            : $"Pontuação final: {score}\n\nToque na tela ou pressione R para tentar novamente.";

        panel.SetActive(true);
        canRestart = true;
        unscaledOpenedAt = Time.unscaledTime;
        Time.timeScale = 0f;
    }

    private void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        if (session != null)
            session.GameEnded -= ShowResult;
    }
}
