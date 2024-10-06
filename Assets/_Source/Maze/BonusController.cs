using UnityEngine;

public class BonusController : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            BonusManager.Instance.BonusCollected();
            Destroy(gameObject);
        }
    }
}
