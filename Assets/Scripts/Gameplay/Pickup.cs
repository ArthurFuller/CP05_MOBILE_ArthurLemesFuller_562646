using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] private PickupType type;
    [SerializeField] private float rotationSpeed = 90f;

    private PickupFactory factory;

    public PickupType Type => type;

    public void Configure(PickupFactory owner)
    {
        factory = owner;
    }

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerStats player = other.GetComponent<PlayerStats>();
        if (player == null)
            return;

        ApplyEffect(player);

        if (factory != null)
            factory.Recycle(this);
        else
            gameObject.SetActive(false);
    }

    protected abstract void ApplyEffect(PlayerStats player);
}
