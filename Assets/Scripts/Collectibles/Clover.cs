using UnityEngine;
using System.Collections;

public class Clover : Pickable
{
    public float PopTransitionDuration = 0.25f;
    public float FadeTransitionDuration = 2f;

    private Material material;
    bool m_activated = false;

    void Awake()
    {
        if (GetComponent<Renderer>() != null)
        {
            material = GetComponent<Renderer>().material;
        }
        m_activated = false;
    }

    protected override void ActivateSpecialBehavior(Vector2 dir)
    {
        if (!m_activated)
        {
            m_activated = true;
            AudioManager.Instance().PlayInstant(5);
            ownerCharacter.IncreaseLuck();
            GetComponent<Collider>().enabled = false;
            GameObject animation = Instantiate(Resources.Load("CloverAnimation"), ownerCharacter.Head.transform.position, Quaternion.identity) as GameObject;
            animation.transform.parent = ownerCharacter.Head.transform;
            Destroy(gameObject);
        }
    }

    private IEnumerator SendCloverOverHead()
    {
        // TODO: Pop particle effect

        float ratio = 0f;

        Vector3 initialPosition = transform.localPosition;
        Vector3 finalPosition = ownerCharacter.Head.transform.position - ownerCharacter.transform.position;

        while (ratio < 1f)
        {
            ratio += Time.deltaTime / PopTransitionDuration;
            transform.localPosition = Vector3.Lerp(initialPosition, finalPosition, ratio);

            yield return null;
        }
        
        //StartCoroutine(FadeOutClover());
    }

    private IEnumerator FadeOutClover()
    {
        float ratio = 0f;

        while (ratio < 1f)
        {
            ratio += Time.deltaTime / FadeTransitionDuration;

            float alpha = Mathf.Lerp(1f, 0f, ratio);

            Color newColor = material.GetColor("_RemainingColor");
            newColor.a = alpha;

            material.SetColor("_RemainingColor", newColor);

            yield return null;
        }

        Destroy(gameObject);
    }
}
