using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f; // Сила прыжка
    [SerializeField] private LayerMask groundLayer; // Слой для определения земли, стен и потолка

    private Rigidbody2D rb;
    private bool isGameActive = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isGameActive && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsCollisionWithWallOrGround(collision.gameObject.layer))
        {
            GameOver();
        }
    }

    private bool IsCollisionWithWallOrGround(int layer)
    {
        return groundLayer == (groundLayer | (1 << layer)); // Проверяем, является ли объект на слое groundLayer
    }

    private void GameOver()
    {
        isGameActive = false;
        FlappyBirdGameManager.Instance.RestartGame(); // Перезапуск через GameManager
    }

    public void StopPlayerMovement()
    {
        isGameActive = false;  // Останавливаем игру
        rb.velocity = Vector2.zero;  // Останавливаем игрока
        rb.isKinematic = true;  // Отключаем физику игрока
    }

    public void ResumePlayerMovement()
    {
        isGameActive = true; // Включаем игру после перезапуска
        rb.isKinematic = false;  // Включаем физику
    }
}
