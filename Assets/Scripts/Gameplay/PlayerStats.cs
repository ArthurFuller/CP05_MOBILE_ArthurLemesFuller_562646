using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;

    public event Action<int> ScoreChanged;
    public event Action<int> HealthChanged;

    public int Score { get; private set; }
    public int Health { get; private set; }

    private void Awake() => Health = maxHealth;

    public void AddScore(int value)
    {
        Score += value;
        ScoreChanged?.Invoke(Score);
    }

    public void Heal(int value)
    {
        Health = Mathf.Min(maxHealth, Health + value);
        HealthChanged?.Invoke(Health);
    }

    public void Damage(int value)
    {
        Health = Mathf.Max(0, Health - value);
        HealthChanged?.Invoke(Health);
    }
}
