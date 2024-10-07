using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("AnimationPlayed"))
        {
            animator.Play("YourAnimationName");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !animator.IsInTransition(0))
        {
            PlayerPrefs.SetInt("AnimationPlayed", 1);
            PlayerPrefs.Save();
            Destroy(gameObject);
        }
    }
}
