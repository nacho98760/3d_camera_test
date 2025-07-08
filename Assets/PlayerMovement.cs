using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 7f;
    public float jumpForce = 5f;

    public float playerHeight;
    public LayerMask floorLayer;

    public float groundDrag;

    float horizontalInput;
    float verticalInput;

    Vector3 movementDirection;

    Rigidbody playerRigidBody;

    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerRigidBody.freezeRotation = true;
    }

    // Update is called once per frame
    private void Update()
    {

        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, floorLayer);

        if (isGrounded)
        {
            playerRigidBody.linearDamping = groundDrag;
        }
        else
        {
            playerRigidBody.linearDamping = 0;
        }

        movePlayer(isGrounded);
        speedControl();
    }

    private void FixedUpdate()
    {
        movementDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        playerRigidBody.AddForce(movementDirection.normalized * movementSpeed * 10f, ForceMode.Force);
    }


    private void movePlayer(bool isGrounded)
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        playerRigidBody.linearVelocity = new Vector3(playerRigidBody.linearVelocity.x, 0f, playerRigidBody.linearVelocity.z);

        playerRigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void speedControl()
    {
        Vector3 flatVelocity = new Vector3(playerRigidBody.linearVelocity.x, 0f, playerRigidBody.linearVelocity.z);

        if (flatVelocity.magnitude > movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * movementSpeed;
            playerRigidBody.linearVelocity = new Vector3(limitedVelocity.x, playerRigidBody.linearVelocity.y, limitedVelocity.z);
        }
    }
}
