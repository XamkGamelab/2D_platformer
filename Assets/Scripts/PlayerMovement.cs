using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Ongelma maan reunaan juuttumisen kanssa johtuu siitä, että peli luulee pelaajan olevan maassa, vaikka pelaaja koskeekin vaan maan reunaa


        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
        bool isTouchingWall = onWall();

        if (!isTouchingWall || isGrounded())
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        }
        else if (isTouchingWall && !isGrounded())
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }

            //jump logic
            if (Input.GetKeyDown(KeyCode.Space))
            Jump();

    }
    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
            Debug.Log("Moi");
        }
        else if (onWall() && !isGrounded())
        {
             float wallJumpPowerY = jumpPower * 1.2f; 
             float wallJumpPowerX = -Mathf.Sign(transform.localScale.x) * 50; 

            body.velocity = new Vector2(wallJumpPowerX, wallJumpPowerY);
            anim.SetTrigger("jump");

        }
    }

    private bool isGrounded()
    {
        float offset = 0.05f;
        Vector2 boxCenter = new Vector2(boxCollider.bounds.center.x, boxCollider.bounds.min.y - offset);
        Vector2 boxSize = new Vector2(boxCollider.bounds.size.x * 0.1f, 0.1f);
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCenter, boxSize, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded();

    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}
