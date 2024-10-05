using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 movement;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Метод для перемещения игрока
    private void Move(Vector2 direction)
    {
        movement = direction.normalized * moveSpeed;
    }

    private void FixedUpdate()
    {
        rb.velocity = movement;
    }

    public void SetMovement(Vector2 direction)
    {
        Move(direction);
    }

    public void StopMovement()
    {
        Move(Vector2.zero);
    }
}
