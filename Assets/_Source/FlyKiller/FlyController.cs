using UnityEngine;

public class FlyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private GameObject bloodStainPrefab;

    private Vector2 targetPosition;

    private void Start()
    {
        SetRandomTargetPosition();
    }

    private void Update()
    {
        MoveFly();
    }

    private void MoveFly()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    private void SetRandomTargetPosition()
    {
        float randomX = Random.Range(-8f, 8f);
        float randomY = Random.Range(-4f, 4f);
        targetPosition = new Vector2(randomX, randomY);
    }

    private void OnMouseDown()
    {
        KillFly();
    }

    private void KillFly()
    {
        Instantiate(bloodStainPrefab, transform.position, Quaternion.identity);

        FlyKillerGameManager.Instance.FlyKilled();

        Destroy(gameObject);
    }
}
