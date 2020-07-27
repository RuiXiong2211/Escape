using System.Collections;
using UnityEngine;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
using System.Security.Cryptography.X509Certificates;
using System;
//using UnityEngine.Experimental.Input;

public class Movement : MonoBehaviour
{
    public Collision coll;
    public Rigidbody2D player;
    //public Rigidbody2D platformvelocity;
    public GameObject currentCheckpoint;
    public GhostTrail ghost;

    [Space]
    [Header("Stats")]
    public float moveSpeed = 7;
    public float jumpForce = 11;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 40;
    public float defaultGravityScale = 4;
    public float stamina;
    public float defaultStamina;
    public float playervelocity;
    public float BubbleSpeed;
    public float BubbleTime;
    public float defaultBubbleTime = 4f;
    public float direction;
    public float maxSpeed;
    public int currentLoadNum;
    public GameObject FirstfirstCheckpoint;

    [Space]
    [Header("Booleans")]
    public bool canMove = true;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;
    public bool canClimb;
    public bool pushingWall;
    public bool inBubble;
    public bool isAlive;
    public bool airResistance = false;
    public bool speedUp = false;
    public bool isMoving;
    public bool map2Boss;
    public bool soundplayed = false;
    public bool inMap3;

    [Space]
    public bool groundTouch;
    public bool hasDashed;

    [Space]
    [Header("Animation")]
    public static int side;
    public Animator anim;
    private int previousSide;

    [Space]
    [Header("Particles")]
    public ParticleSystem jumpParticle;
    public ParticleSystem dashParticle;
    public ParticleSystem wallJumpParticle;
    public ParticleSystem slideParticle;
    public ParticleSystem deathParticle;

    [Space]
    [Header("Sound")]
    public AudioSource run;
    public AudioSource dashSound;
    public AudioSource jumpsound;
    public AudioSource wallclimb;
    public AudioSource bubble;
    public AudioSource inbubblesound;
    public OptionsMenu setSound;

    [Space]
    [Header("Camera")]
    public CameraShake currentCam;

    [Space]
    [Header("For boss fight")]
    public GameObject Bossmap2;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collision>();
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player.gravityScale = defaultGravityScale;
        side = 1;
        defaultStamina = stamina;
        isAlive = true;
        map2Boss = false;
        setSound.SetData();
        map2Boss = PlayerPrefs.GetInt("Map2Boss", 0) == 1;
        if (Bossmap2 != null)
        {
            Bossmap2.SetActive(PlayerPrefs.GetInt("Map2Boss", 0) == 1);
        }
        currentLoadNum = PlayerPrefs.GetInt("CurrentLoadNum", 0);
        Debug.Log(currentLoadNum);
        PlayerPrefs.SetInt("Load" + currentLoadNum + "SceneNum", SceneManager.GetActiveScene().buildIndex);
        
        if (PlayerPrefs.GetInt("NewGame", 1) == 1)
        {
            CreateNewPlayer();
            SavePlayer();
        }
        else
        {
            LoadPlayer();
        }

    }

    void Update()
    {
        //Fixes the Z position of player in order to prevent transportation bugs.
        if (inMap3)
        {
            Vector3 newPosition = new Vector3(
            this.gameObject.transform.position.x,
            this.gameObject.transform.position.y, 1f);
            this.gameObject.transform.localPosition = newPosition;
            PlayerPrefs.SetInt("Map2Boss", 0);
        }
        //?
        currentCam = FindObjectOfType<CameraShake>();
        
        // Record User Inputs
        float x = Input.GetAxis("Horizontal");
        direction = x;
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);
        playervelocity = player.velocity.y;

        if (x != 0 || y != 0)
        {
            isMoving = true;
        } else
        {
            isMoving = false;
        }

        // Always walk
        if (!inBubble)
        {
            Walk(dir);
            // If player is not in bubble and not dashing, when the player is falling from a certain height, 
            // the player will reach a terminal velocity limited by the maxSpeed.
            if (!isDashing && player.velocity.y < 0)
            {
                player.velocity = Vector3.ClampMagnitude(player.velocity, maxSpeed);
            }
        }
       
        // Animation
        anim.SetFloat("Speed", Mathf.Abs(player.velocity.x));
        anim.SetFloat("Jump.y", player.velocity.y);
        anim.SetBool("onGround", coll.onGround);
        anim.SetBool("Climb", coll.onWall && wallGrab && canClimb);
        anim.SetBool("wallSlide", wallSlide);
        anim.SetBool("inBubble", inBubble);

        // Check if collision radius is in contct with wall and wall grab button
        // held, and the player still has stamina remaining.
        if (coll.onWall && Input.GetButton("Fire3") && canMove) {
            wallGrab = true;
            wallSlide = false;
            previousSide = coll.wallSide;
        }

        // Check if player releases wall grab button or is not in contact with wall
        // or has no stamina left
        if (Input.GetButtonUp("Fire3") || !coll.onWall || !canMove) {
            wallGrab = false;
            wallSlide = false;
        }

        if (coll.onGround && !isDashing)
        {
            wallJumped = false;
            GetComponent<BetterJump>().enabled = true;
        }

        // Vertical wall climb, alters velocity accordng to direction
        if (wallGrab && !isDashing && canClimb)
        {
            //wallclimb.Play();
            player.gravityScale = 0;
            // hanging on left wall or right wall
            if (x > .2f || x < -.2f)
                player.velocity = new Vector2(0, 0);

            float speedModifier = y > 0 ? .5f : 1;

            player.velocity = new Vector2(0, y * (moveSpeed * speedModifier));
            stamina -= Time.deltaTime;
            if (stamina <= 0)
            {
                canClimb = false;
                player.gravityScale = defaultGravityScale;
            }
        }

        if (!wallGrab && !isDashing)
        {
            player.gravityScale = defaultGravityScale;
        }

        if (coll.onWall && !coll.onGround)
        {
            if (x != 0 && !wallGrab && !pushingWall && player.velocity.y < 0)
            {
                wallSlide = true;
                WallSlide();
            }
        }

        if (/*!coll.onWall || */coll.onGround /*|| coll.wallSide == -previousSide*/ || (coll.onGround && coll.onWall)) {
            //stamina = defaultStamina;
            //canClimb = true;
            wallSlide = false;
            //wallJumped = false;
            //pushingWall = false;
        }

        //reset Climbstamina
        if (coll.onGround && !canClimb)
         {
            Debug.Log("...");
            ResetClimb();
        }

        // Jump
        if (Input.GetButtonDown("Jump") && canMove) {
            //jump.Play();
            if (coll.onGround) {
                anim.SetTrigger("Jump");
                Jump(Vector2.up, false);
                jumpParticle.Play();
            }
            if (coll.onGround && coll.onWall)
            {
                anim.SetTrigger("Jump");
                Jump(Vector2.up, false);
                jumpParticle.Play();
            }
            if (coll.onWall && !coll.onGround) {
                anim.SetTrigger("Jump");
                WallJump();
            }
        }

        // Dash
        if (Input.GetButtonDown("Fire1") && !hasDashed && canMove)
        {
            Dash(xRaw, yRaw);
            //Gamepad.current.SetMotorSpeeds(0.25f, 0.75f);
        }

        // BubbleFlight
        if (inBubble)
        {
            player.velocity = new Vector2(dir.x * BubbleSpeed, dir.y * BubbleSpeed);
            Bubbletime();
            //StartCoroutine(BubbleWait());
            //player.velocity = new Vector2(dir.x * BubbleSpeed, dir.y * BubbleSpeed);
        }

        // Check if player is touching the ground
        if (coll.onGround && !groundTouch)
        {
            stamina = defaultStamina;
            GroundTouch();
            groundTouch = true;
        }

        if (!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }

        if (wallGrab || wallSlide || !canMove) {
            return;
        }
        
        if (player.velocity.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        } 
        else if (player.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }


        if (x > 0)
        {
            side = 1;
        }
        if (x < 0)
        {
            side = -1; 
        }

    }

    // Movement functions
    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;

        if (wallGrab)
            return;

        if (airResistance)
        {
            if (!wallJumped)
            {
                player.velocity = new Vector2(dir.x * (moveSpeed / 2), player.velocity.y);
            }
            else
            {
                player.velocity = Vector2.Lerp(player.velocity, (new Vector2(dir.x * moveSpeed, player.velocity.y)), wallJumpLerp * Time.deltaTime);
            }
        }
        else if (speedUp)
        {
            if (!wallJumped)
            {
                player.velocity = new Vector2(dir.x * (moveSpeed * 2), player.velocity.y);
            }
            else
            {
                player.velocity = Vector2.Lerp(player.velocity, (new Vector2(dir.x * moveSpeed, player.velocity.y)), wallJumpLerp * Time.deltaTime);
            }
        }    
        else
        {
            if (!wallJumped)
            {
                player.velocity = new Vector2(dir.x * moveSpeed, player.velocity.y);
            }
            else
            {
                player.velocity = Vector2.Lerp(player.velocity, (new Vector2(dir.x * moveSpeed, player.velocity.y)), wallJumpLerp * Time.deltaTime);
            }
        }
    }

    public void WindWalk(float direction)
    {
        //player.velocity = new Vector2(direction * moveSpeed, player.velocity.y);
        Vector2 tempVect = new Vector2(direction * 40, 0);
        player.AddForce(tempVect);
    }

    public void Jump(Vector2 dir, bool wall) 
    {   
        ParticleSystem particle = coll.onWall ? wallJumpParticle : jumpParticle;
        if (transform.parent != null)
        {
            if (transform.parent.GetComponent<Moving>().direction > 0)
            {
                player.velocity = Vector2.up * transform.parent.GetComponent<Moving>().moveSpeed;
                player.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                //player.velocity += dir * (jumpForce + 0.5);
            }
            else
            {
                player.velocity = new Vector2(player.velocity.x, 0);
                player.velocity += dir * jumpForce;
            }
        }
        else
        {
            player.velocity = new Vector2(player.velocity.x, 0);
            player.velocity += dir * jumpForce;
        }
        particle.Play();
    }

    private void Dash(float x, float y)
    {   
        anim.SetTrigger("Dash");
        hasDashed = true;

        player.velocity = Vector2.zero;

        if (x == 0 && y == 0) {
            Vector2 dir = new Vector2(side, 0);
            player.velocity += dir.normalized * dashSpeed * 1.8f;
        } else if (x != 0 && y == 0) {
            Vector2 dir = new Vector2(x, 0);
            player.velocity += dir.normalized * dashSpeed * 1.8f;
        } else if (x == 0 && y != 0) {
            Vector2 dir = new Vector2(0, y);
            player.velocity += dir.normalized * dashSpeed;
        } else {
            Vector2 dir = new Vector2(x, y);
            player.velocity += dir.normalized * dashSpeed;
        }
        //dash.Play();
        currentCam.ShakeCamera(4f, .2f);
        StartCoroutine(DashWait());
    }

    public void spring(float direc, float force)
    {
        player.velocity = Vector2.zero;
        Vector2 dir = new Vector2(direc, 0);
        player.velocity += dir.normalized * force;
        StartCoroutine(SpringWait());
    }

    void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;
    }

    IEnumerator DashWait()
    {
        //GhostTrail[] ghosts = FindObjectsOfType<GhostTrail>();
        //Array.ForEach<GhostTrail>(ghosts, g => g.ShowGhost());
        //FindObjectOfType<GhostTrail>().ShowGhost();
        ghost.ShowGhost();
        StartCoroutine(GroundDash());
        DOVirtual.Float(14, 0, .8f, RigidbodyDrag);
        //DOVirtual.Float(14, 0, .3f, RigidbodyDrag);
        //player.drag = 14;
        dashParticle.Play();
        player.gravityScale = 0;
        GetComponent<BetterJump>().enabled = false;
        wallJumped = true;
        isDashing = true;
        yield return new WaitForSeconds(0.05f);
        DOVirtual.Float(0, defaultGravityScale, 0.1f, gravity);
        //player.AddForce(transform.right * 10, ForceMode2D.Impulse); 
        yield return new WaitForSeconds(.2f);
        //player.AddForce(transform.right * 10, ForceMode2D.Impulse);
        //DOVirtual.Float(14, 0, .3f, RigidbodyDrag);
        player.gravityScale = defaultGravityScale;
        GetComponent<BetterJump>().enabled = true;
        wallJumped = false;
        isDashing = false;
    }

    IEnumerator SpringWait()
    {
        StartCoroutine(GroundDash());
        StartCoroutine(DisableMovement(0.25f));
        DOVirtual.Float(14, 0, .8f, RigidbodyDrag);
        GetComponent<BetterJump>().enabled = false;
        wallJumped = true;
        isDashing = true;
        yield return new WaitForSeconds(0.25f);
        GetComponent<BetterJump>().enabled = true;
        wallJumped = false;
        isDashing = false;
    }
    void Bubbletime()
    {   
        if (!soundplayed)
        {
            bubble.Play();
            soundplayed = true;
        }
        hasDashed = true;
        wallSlide = false;
        player.gravityScale = 0f;
        BubbleTime -= Time.deltaTime;
        if (BubbleTime <= 0)
        {
            hasDashed = false;
            inBubble = false;
            player.gravityScale = defaultGravityScale;
            BubbleTime = defaultBubbleTime;
            soundplayed = false;
        }
    }

    //IEnumerator BubbleWait()
    //{
    //    player.gravityScale = 0f;
    //    yield return new WaitForSeconds(4f);
    //   inBubble = false;
    //    player.gravityScale = defaultGravityScale;
    //}

    void gravity(float x)
    {
        player.gravityScale = x;
    }

    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
            hasDashed = false;
    }

    private void WallJump() {
        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));
        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up * 2f / 1.8f + wallDir * 2f / 1.8f), true);
        if (coll.onRightWall)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            side = -1;
        } else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            side = 1;
        }

        wallJumped = true;
    }

     private void WallSlide()
    {
        // makes player cannot wall slide when stamina just finished
        /*if (!canMove)
            return;*/

        pushingWall = false;
        if((player.velocity.x > 0 && coll.onRightWall) || (player.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : player.velocity.x;

        player.velocity = new Vector2(push, -slideSpeed);
        slideParticle.Play();
        pushingWall = false;
    }
    
    public void ResetClimb()
    {
        StartCoroutine(Climbdelay());
        canClimb = true;
    }

    public IEnumerator Climbdelay()
    {
        yield return new WaitForSeconds(.2f);
    }

    public IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    public IEnumerator DisableNStopMovement(float time)
    {
        player.velocity = Vector2.zero;
        player.gravityScale = 0;
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
        player.gravityScale = defaultGravityScale;
    }

    void RigidbodyDrag(float x)
    {
        player.drag = x;
    }

    void gravityDrag(float x)
    {
        player.gravityScale = x;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "movingplatform")
        {
            transform.parent = collision.transform;
            Debug.Log("Bug");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "movingplatform")
        {
            transform.parent = null;
            Debug.Log("Buggy");
        }
    }

    public void SavePlayer()
    {
        currentLoadNum = PlayerPrefs.GetInt("CurrentLoadNum", 0);
        //SaveSystem.SavePlayer(this);
        PlayerPrefs.SetFloat("Load" + currentLoadNum + "-position.x", currentCheckpoint.transform.position.x);
        PlayerPrefs.SetFloat("Load" + currentLoadNum + "-position.y", currentCheckpoint.transform.position.y);
        PlayerPrefs.SetFloat("Load" + currentLoadNum + "-position.z", currentCheckpoint.transform.position.z);
        Debug.Log("Saving scene num" + SceneManager.GetActiveScene().buildIndex + "/" + currentLoadNum);
        PlayerPrefs.SetInt("Load" + currentLoadNum + "SceneNum", SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadPlayer()
    {
        //PlayerData data = SaveSystem.LoadPlayer();
        //currentCheckpoint = data.savedCheckpoint;
        //MoveToCurrentCheckpoint();
        Debug.Log("ran");
        Vector3 position;
        position.x = PlayerPrefs.GetFloat("Load" + currentLoadNum + "-position.x");
        position.y = PlayerPrefs.GetFloat("Load" + currentLoadNum + "-position.y");
        position.z = PlayerPrefs.GetFloat("Load" + currentLoadNum + "-position.z");
        player.transform.position = position;
    }

    public void CreateNewPlayer()
    {
        currentCheckpoint = FirstfirstCheckpoint;
        MoveToCurrentCheckpoint();
    }

    public void MoveToCurrentCheckpoint()
    {
        player.transform.position = currentCheckpoint.transform.position;
    }

    public void runSound()
    {
        run.Play();
    }

    public void jumpSound()
    {
        jumpsound.Play();
    }

    public void dashsound()
    {
        dashSound.Play();
    }

    public void wallSound()
    {
        wallclimb.Play();
    }

    void bubblesound()
    {
        inbubblesound.Play();
    }
}
