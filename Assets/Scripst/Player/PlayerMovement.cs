using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;


    public Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private float wallJumpCooldown;
    private float horizontalInput;

    public Gamepad playerControl;
    private InputAction jump;
    
   
 

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // referenciák
        boxCollider = GetComponent<BoxCollider2D>();
        playerControl = new Gamepad();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
    
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);


        // ha jobbra megy jobbra fordul , ha balra akkor balra

        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(-4, 4, 4);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(4, 4, 4);



        //Set animator parameter
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());


        // Walljump logikaja
        if (wallJumpCooldown > 0.2f)
        {

            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else body.gravityScale = 7;
            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else
            wallJumpCooldown += Time.deltaTime;
            
    }

    // ugrás --- kell a groundede hogy ne repüljön -- ez annyit tesz hogy csak akkor ugorhat ha földn van 
    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }

        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 6);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            wallJumpCooldown = 0;
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && !onWall();
    }

}
