using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;

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
