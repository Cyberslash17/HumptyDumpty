using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    static bool isFirstPlayerAssigned;
	// Constantes, en théorie
	public float m_jumpForce = 10f;
    public float m_jumpdelay;
	public float m_runSpeed = 10;
    public int m_playerNumber;

    public bool m_isInvulnerable;
    public float m_invulnerabiltyDelay = 1f;
    public GameObject Hand;
    public GameObject Head;
    public GameObject[] Lives;
	public ParticleEmitter damageParticles;

    bool m_isGrounded = false;
	bool m_isJumping = false;
    bool m_isThrowing = false;

    private string m_playerExtension = "";
    private string m_playerName = "";
    private float m_timerjump;
    private float m_catSlashDuration;

    //private PlayerSkinService m_skinService;

	private int jumpFrames = 0;
	public int maxJumpFrames = 20;

    Animator m_animator;

    public string PlayerExtension
    {
        get
        {
            return m_playerExtension;
        }
    }
    public string PlayerName
    {
        get
        {
            return m_playerName;
        }
        set
        {
            m_playerName = value;
        }
    }

	public int maxLuck = 5;
	public int _luck = 0;

    GameController m_gameController;
    Rigidbody m_rigidbody;

    public bool IsGrounded
    {
        get
        {
            return m_isGrounded;
        }
        set
        {
            if (value)
            {
                m_isJumping = false;
            }
            m_isGrounded = value;
        }
    }

    void Start()
    {
        Lives[2].SetActive(false);
        Lives[3].SetActive(false);
        m_isInvulnerable = false;
        m_timerjump = Time.time;
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        if (GameObject.FindGameObjectWithTag("GameController"))
        {
            m_gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            //m_skinService = m_gameController.GetComponentInParent<PlayerSkinService>();
        }
        AssignPlayer();
    }
	
	void Update () 
	{
        if(m_gameController == null || !m_gameController)
        {
            if (GameObject.FindGameObjectWithTag("GameController"))
            {
                m_gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
                //m_skinService = m_gameController.GetComponentInParent<PlayerSkinService>();
            }
        }
        if (m_playerName == "")
        {
           // if (m_skinService)
            {
                //m_skinService.AssignNextSkinWithName(this, "");
            }
        }
        
		if (m_isGrounded) {
			jumpFrames = 0;
		}
		
        if (m_catSlashDuration >= 0)
        {
            m_catSlashDuration -= Time.deltaTime;
        }
        else
        {
			if (Input.GetButton ("Jump" + m_playerExtension)) {
				HandleJump ();
			} else if (Input.GetButtonUp ("Jump" + m_playerExtension)) {
				StopJump ();
			}
            if (Input.GetButtonDown("Fire" + m_playerExtension))
            {
                float xAim = Input.GetAxis("AimRight" + m_playerExtension);
                float yAim = Input.GetAxis("AimUp" + m_playerExtension);

                ActivateItem(xAim, yAim);
            }

            //if (Input.GetButtonDown("NextSkin" + m_playerExtension))
            {
                //m_gameController.GetComponentInParent<PlayerSkinService>().AssignNextSkinWithName(this, m_playerName);
            }
            if (Input.GetButtonDown("PreviousSkin" + m_playerExtension))
            {
                // TODO
            }
            HandleAnimations();
        }

        if (!m_gameController.IsGameOver)
        {
            if (_luck < 0)
            {
                Debug.Log("loser is " + m_playerNumber);
                PlayerPrefs.SetInt("WinnerPlayer", m_playerNumber == 1 ? 2 : 1); // if player1 lose, store player 2 and vice versa
                isFirstPlayerAssigned = false;
                m_gameController.EndGame(gameObject);
            }
            // Lose 2 lucks if falling.
            if (transform.position.y < -6.5f)
            {
                _luck -= 2;
                if (_luck >= 0)
                {
                    Lives[_luck].SetActive(false);
                    Lives[_luck + 1].SetActive(false);
                    //m_invin = Time.time;
                    m_isInvulnerable = true;
                    Invoke("StopInvulnerability", m_invulnerabiltyDelay);
                    Vector3 newPos = gameObject.transform.position;
                    GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                    newPos.y = 7f;
                    gameObject.transform.position = newPos;
                    gameObject.GetComponent<Rigidbody>().useGravity = false;
                    m_isGrounded = true;
                }
            }
        }
    }

    void AssignPlayer()
    {
       // m_skinService.AssignNextSkinWithName(this, "");
        if (!isFirstPlayerAssigned)
        {
            isFirstPlayerAssigned = true;
            m_playerExtension = "_1";
            m_playerNumber = 1;
        }
        else
        {
            m_playerExtension = "_2";
            m_playerNumber = 2;
        }
    }

    void HandleAnimations()
    {
        m_animator.SetBool("isGrounded", m_isGrounded);
        m_animator.SetBool("isJumping", !m_isGrounded);
        m_animator.SetBool("isThrowing", m_isThrowing);
        if (m_isThrowing)
        {
            m_isThrowing = false;
        }
    }

    void HandleJump()

	{
		if (jumpFrames < maxJumpFrames) // && Time.time > m_timerjump
		{
			Jump ();
			m_isGrounded = false;
		} else if(m_isGrounded)
		{
            Jump ();
		}
	}

	void StopJump()
	{
		jumpFrames = maxJumpFrames;
		Vector3 _v = GetComponent<Rigidbody> ().velocity;
		_v.y = Mathf.Min (0.2f, _v.y);
		GetComponent<Rigidbody> ().velocity = _v;
	}

	void Jump()
	{
		jumpFrames++;
		Vector3 _v = GetComponent<Rigidbody> ().velocity;
		_v.y = m_jumpForce * Time.deltaTime * 1000 / Mathf.Max(5, jumpFrames + 2);
		GetComponent<Rigidbody> ().velocity = _v;
	}

    void ActivateItem(float xAim, float yAim)
    {
        Pickable item = transform.GetComponentInChildren<Pickable>();

        Vector2 dir = new Vector2(xAim, yAim).normalized;

        if (dir == Vector2.zero)
        {
            dir = Vector2.right;
        }

        if (item)
        {
            item.Activate(dir);
            m_isThrowing = true;
        }
    }

    void StopInvulnerability()
    {
        m_isInvulnerable = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    public void GetCatSlashed(float duration)
    {
        m_catSlashDuration = duration;
    }
    public void OnCollisionEnter(Collision collide)
    {
        if(collide.gameObject.tag=="Platform" && transform.position.y- collide.gameObject.transform.position.y<0.8)
        {
            float adj = collide.transform.localScale.y * Mathf.Cos(collide.transform.rotation.eulerAngles.z);
            float angle = collide.transform.rotation.eulerAngles.z;
            float touched= transform.position.y-adj;
            if ((angle>0 && angle<180 && transform.position.y - touched < 1.10 && (collide.transform.position.x-collide.transform.localScale.x/2)- transform.position.x>-0.1) || angle > 180 && transform.position.y + touched < 1.5 && (collide.transform.position.x - collide.transform.localScale.x / 2) - transform.position.x > -0.1)
            {
                GetComponent<CapsuleCollider>().isTrigger = true;
                Invoke("DeTrigger", 0.3f);
            }
        }
        
    }
    public void DeTrigger()
    {
        GetComponent<CapsuleCollider>().isTrigger = false;
	}

	public void IncreaseLuck(){
		_luck = Mathf.Min(_luck+1, maxLuck);
        Lives[_luck-1].SetActive(true);
	}

	public void DecreaseLuck(){
		
		_luck = Mathf.Max (_luck - 1, -1);
        if (_luck >= 0)
        {
            Lives[_luck].SetActive(false);
        }
		if (gameObject) {
			ParticleEmitter particles = Instantiate (damageParticles, gameObject.transform.position, Quaternion.identity) as ParticleEmitter;
			Destroy (particles, 2);
		}
    }

	public int GetLuck(){
		return _luck;
	}
}
