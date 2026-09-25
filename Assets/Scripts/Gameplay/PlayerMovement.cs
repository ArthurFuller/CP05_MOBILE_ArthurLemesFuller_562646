using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6f;

    private Rigidbody body;
    private Vector2 input;
    private Vector2 touchStart;

    public Vector3 Velocity => body.linearVelocity;

    private void Awake() => body = GetComponent<Rigidbody>();

    private void Update()
    {
        input = ReadKeyboard();
        if (input.sqrMagnitude < 0.01f)
            input = ReadTouch();
    }

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;
        body.linearVelocity = direction * speed;
    }

    private static Vector2 ReadKeyboard()
    {
        if (Keyboard.current == null)
            return Vector2.zero;

        float x = 0f;
        float y = 0f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) x--;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) x++;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) y--;
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) y++;
        return new Vector2(x, y);
    }

    private Vector2 ReadTouch()
    {
        if (Touchscreen.current == null)
            return Vector2.zero;

        var touch = Touchscreen.current.primaryTouch;
        if (touch.press.wasPressedThisFrame)
            touchStart = touch.position.ReadValue();

        if (!touch.press.isPressed)
            return Vector2.zero;

        return Vector2.ClampMagnitude((touch.position.ReadValue() - touchStart) / 90f, 1f);
    }
}
