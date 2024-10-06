using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AudioClip jumpSound; // Поле для звука прыжка

    private Rigidbody2D rb;
    private AudioSource audioSource; // Компонент AudioSource
    private bool isGameActive = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>(); // Получаем компонент AudioSource
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
        PlayJumpSound(); // Воспроизводим звук прыжка
    }

    private void PlayJumpSound()
    {
        if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
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
        return groundLayer == (groundLayer | (1 << layer));
    }

    private void GameOver()
    {
        isGameActive = false;
        FlappyBirdGameManager.Instance.RestartGame();
    }

    public void StopPlayerMovement()
    {
        isGameActive = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }

    public void ResumePlayerMovement()
    {
        isGameActive = true;
        rb.isKinematic = false;
    }
}
