using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public abstract class Pickable : MonoBehaviour
{
    public float ScaleUpTime = 0.25f;
    public Vector3 MinScale = Vector3.one;
    public Vector3 MaxScale = Vector3.one;

    protected enum PickableState { Pickable, PickedUp, InTheAir, UmbrellaThrown /*quick hack hotfix*/ };
    protected PickableState currentState;

    protected PlayerController ownerCharacter;
	
	void Update () {
        // Self-destruct if the item gets too far away.
	    if((gameObject.transform.position - Camera.main.transform.position).magnitude > 500f)
        {
            Destroy(this.gameObject);
        }
	}

    public void Activate(Vector2 dir)
    {
        ActivateSpecialBehavior(dir);
		gameObject.tag = "Obstacle";
        StartCoroutine(ScaleUpItemCoroutine());
    }

    protected abstract void ActivateSpecialBehavior(Vector2 dir);

    protected virtual void OnTriggerEnter(Collider other)
    {
        PlayerController otherController = other.GetComponent<PlayerController>();

        if (otherController == null) return;

        if (currentState == PickableState.Pickable && !other.gameObject.GetComponentInChildren<Pickable>())
        {
            Debug.Log("Player picked up an item");
            currentState = PickableState.PickedUp;

            ownerCharacter = other.gameObject.GetComponent<PlayerController>();

            transform.position = other.GetComponent<PlayerController>().Hand.transform.position;
            transform.parent = other.transform;
        }
    }

    private IEnumerator ScaleUpItemCoroutine()
    {
        float ratio = 0f;

        while (ratio < 1f)
        {
            ratio += Time.deltaTime / ScaleUpTime;
            transform.localScale = Vector3.Lerp(MinScale, MaxScale, ratio);

            yield return null;
        }
    }
}
