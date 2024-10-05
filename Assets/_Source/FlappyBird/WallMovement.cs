using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{
    [SerializeField] private float wallSpeed = 2f; // —корость движени€ стен

    private void Update()
    {
        if (FlappyBirdGameManager.Instance.IsGameActive())
        {
            transform.Translate(Vector3.left * wallSpeed * Time.deltaTime);
        }

        if (transform.position.x < -10f) // ”ничтожаем стену, когда она уходит за пределы видимости
        {
            Destroy(gameObject);
        }
    }
}
