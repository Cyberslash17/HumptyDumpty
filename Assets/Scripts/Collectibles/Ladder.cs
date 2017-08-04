using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ladder : Pickable
{
    public GameObject LadderFront;
    public GameObject LadderBack;
    public Vector3 FrontDestroyedPosition;
    public Vector3 BackDestroyedPosition;
    public float FrontDestroyedAngle;
    public float BackDestroyedAngle;
    public float DestroyAnimationDuration = 1f;
    public float FadeDuration = 0.5f;
    public Vector3 DeployOffset;
    public LayerMask RaycastIgnoreMask;
    public float PlatformRaycastLength = 1.9f;

    private Material frontMaterial;
    private Material backMaterial;

    void Awake()
    {
        if (LadderFront.GetComponent<Renderer>() != null)
        {
            frontMaterial = LadderFront.GetComponent<Renderer>().material;
        }

        if (LadderBack.GetComponent<Renderer>() != null)
        {
            backMaterial = LadderBack.GetComponent<Renderer>().material;
        }
    }
	
	void Update ()
    {
        if (currentState == PickableState.InTheAir &&
            Physics.Raycast(transform.position, Vector3.down, PlatformRaycastLength, RaycastIgnoreMask))
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

	protected override void ActivateSpecialBehavior(Vector2 dir)
	{
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
		rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;

		BoxCollider bc = gameObject.GetComponent<BoxCollider> ();
		bc.size = new Vector3(2.6f, 5.43f, 1);
		bc.isTrigger = true;

        transform.parent = null;
        //transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
		if(ownerCharacter!=null)
			transform.position = ownerCharacter.transform.position + DeployOffset;
        currentState = PickableState.InTheAir;
	}

	protected override void OnTriggerEnter(Collider other)
	{
        base.OnTriggerEnter(other);

		if (other.GetComponent<PlayerController>() == null) return;

		if(currentState == PickableState.InTheAir){
			Debug.Log("Player got hit by a Ladder");
			GetComponent<Collider>().enabled = false;

            StartCoroutine(DestroyLadder(other.GetComponent<PlayerController>()));
		}
	}

    private IEnumerator DestroyLadder(PlayerController victim)
    {
        bool hasBeenAffectedYet = false;

        float ratio = 0f;

        Vector3 frontInitialPos = LadderFront.transform.localPosition;
        Vector3 backInitialPos = LadderBack.transform.localPosition;
        float frontInitialAngle = LadderFront.transform.localRotation.z;
        float backInitialAngle = LadderBack.transform.localRotation.z;

        while (ratio < 1f)
        {
            ratio += Time.deltaTime / DestroyAnimationDuration;

            float smoothRatio = Mathf.Pow(ratio, 4);

            LadderFront.transform.localPosition = Vector3.Lerp(frontInitialPos, FrontDestroyedPosition, smoothRatio);
            LadderBack.transform.localPosition = Vector3.Lerp(backInitialPos, BackDestroyedPosition, smoothRatio);

            if (ratio >= 0.3f && !hasBeenAffectedYet)
            {
                victim.DecreaseLuck();
                AudioManager.Instance().PlayInstant(3);

                hasBeenAffectedYet = true;
            }

            float newFrontAngle = Mathf.Lerp(frontInitialAngle, FrontDestroyedAngle, smoothRatio);
            LadderFront.transform.localEulerAngles = new Vector3(LadderFront.transform.localEulerAngles.x, LadderFront.transform.localEulerAngles.y, newFrontAngle);

            float newBackAngle = Mathf.Lerp(backInitialAngle, BackDestroyedAngle, smoothRatio);
            LadderBack.transform.localEulerAngles = new Vector3(LadderBack.transform.localEulerAngles.x, LadderBack.transform.localEulerAngles.y, newBackAngle);

            yield return null;
        }

        ratio = 0f;

        while (ratio < 1f)
        {
            ratio += Time.deltaTime / FadeDuration;

            float alpha = Mathf.Lerp(1f, 0f, ratio);

            Color frontTint = frontMaterial.GetColor("_RemainingColor");
            frontTint.a = alpha;

            Color backTint = backMaterial.GetColor("_RemainingColor");
            backTint.a = alpha;

            frontMaterial.SetColor("_RemainingColor", frontTint);
            backMaterial.SetColor("_RemainingColor", backTint);

            yield return null;
        }

        Destroy(gameObject);
    }
}
