using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum Kind { Crystal, Health }

    [SerializeField] private Kind kind;
    [SerializeField] private int value = 10;
    [SerializeField] private float rotationSpeed = 70f;

    private void Update() => transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out PlayerStats player))
            return;

        if (kind == Kind.Crystal) player.AddScore(value);
        else player.Heal(value);

        gameObject.SetActive(false);
    }
}
