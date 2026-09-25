using System;
using UnityEngine;

// OBSERVER: observa PlayerStats para encerrar a partida e publica eventos da sessao.
public class GameSessionController : MonoBehaviour
{
    public event Action<float> TimeChanged;
    public event Action<bool, int> GameEnded;

    [SerializeField] private PlayerStats player;
    [SerializeField] private int targetScore = 100;
    [SerializeField] private float matchDuration = 60f;

    public int TargetScore => targetScore;
    public float TimeRemaining { get; private set; }
    public bool IsFinished { get; private set; }

    private int lastDisplayedSecond = -1;

    private void Awake()
    {
        Time.timeScale = 1f;
        TimeRemaining = matchDuration;
    }

    private void OnEnable()
    {
        if (player == null)
            return;

        player.ScoreChanged += OnScoreChanged;
        player.HealthChanged += OnHealthChanged;
    }

    private void Start()
    {
        TimeChanged?.Invoke(TimeRemaining);
    }

    private void Update()
    {
        if (IsFinished)
            return;

        TimeRemaining = Mathf.Max(0f, TimeRemaining - Time.deltaTime);

        int currentSecond = Mathf.CeilToInt(TimeRemaining);
        if (currentSecond != lastDisplayedSecond)
        {
            lastDisplayedSecond = currentSecond;
            TimeChanged?.Invoke(TimeRemaining);
        }

        if (TimeRemaining <= 0f)
            Finish(false);
    }

    private void OnScoreChanged(int score)
    {
        if (!IsFinished && score >= targetScore)
            Finish(true);
    }

    private void OnHealthChanged(int health)
    {
        if (!IsFinished && health <= 0)
            Finish(false);
    }

    private void Finish(bool victory)
    {
        if (IsFinished)
            return;

        IsFinished = true;
        GameEnded?.Invoke(victory, player.Score);
    }

    private void OnDisable()
    {
        if (player == null)
            return;

        player.ScoreChanged -= OnScoreChanged;
        player.HealthChanged -= OnHealthChanged;
    }
}
