using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private EnemyMovementStrategy strategy;
    [SerializeField] private float speed = 2.8f;
    [SerializeField] private float hitCooldown = 1f;

    private Rigidbody body;
    private float nextHit;

    private void Awake() => body = GetComponent<Rigidbody>();

    private void FixedUpdate()
    {
        body.linearVelocity = strategy.GetDirection(transform, player) * speed;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Time.time < nextHit || !collision.gameObject.TryGetComponent(out PlayerStats stats))
            return;

        nextHit = Time.time + hitCooldown;
        stats.Damage(1);
    }
}
