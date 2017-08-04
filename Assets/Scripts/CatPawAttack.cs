using UnityEngine;
using System.Collections;

public class CatPawAttack : MonoBehaviour {
    public RatioProgression LeftDiagonalSlashClass;
    public RatioProgression RightDiagonalSlashClass;
    public float SlashProgressionDuration = 0.25f;

    private float attackDuration;
    private float elapsedTime;
    private float currentSlashRatio;
    private bool isRightSlash;

    private RatioProgression currentSlash;
    private RatioProgression previousSlash;

    void Start()
    {
        currentSlash = GameObject.Instantiate(LeftDiagonalSlashClass, transform.position, LeftDiagonalSlashClass.transform.rotation) as RatioProgression;
        currentSlash.transform.parent = transform;
        currentSlash.SetCompletedRatio(0f);
        currentSlash.SetCompletedAlpha(1f);
    }

	void Update ()
    {
        elapsedTime += Time.deltaTime;
        currentSlashRatio += Time.deltaTime / 0.25f;

        currentSlash.SetCompletedRatio(currentSlashRatio);

        if (previousSlash != null)
        {
            previousSlash.SetCompletedAlpha(Mathf.Clamp(1 - currentSlashRatio, 0f, 1f));
        }
        
        if (currentSlashRatio >= 1f)
        {
            if (previousSlash != null)
            {
                Destroy(previousSlash.gameObject);
            }

            previousSlash = currentSlash;

            isRightSlash = !isRightSlash;
            RatioProgression slashClass = isRightSlash ? RightDiagonalSlashClass : LeftDiagonalSlashClass;
            currentSlash = GameObject.Instantiate(slashClass, transform.position, slashClass.transform.rotation) as RatioProgression;
            currentSlash.transform.parent = transform;

            currentSlash.SetCompletedRatio(0f);
            currentSlash.SetCompletedAlpha(1f);
            currentSlashRatio = 0f;
        }

        if (elapsedTime >= attackDuration)
        {
            Destroy(gameObject);
        }
    }

    public void SetAttackDuration(float duration)
    {
        attackDuration = duration;
    }

    public void SetParent(Transform newParent)
    {
        transform.parent = newParent;
    }
}
