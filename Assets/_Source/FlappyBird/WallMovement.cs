using UnityEngine;

public class WallMovement : MonoBehaviour
{
    [SerializeField] private float wallSpeed = 2f;

    private void Update()
    {
        if (FlappyBirdGameManager.Instance.IsGameActive())
        {
            transform.Translate(Vector3.left * wallSpeed * Time.deltaTime);
        }

        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}
