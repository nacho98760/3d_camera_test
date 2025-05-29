using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 7f;
    public float jumpForce = 5f;

    public float playerHeight;
    public LayerMask floorLayer;
    bool grounded;

    public float groundDrag;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 movementDirection;

    Rigidbody playerRigidBody;

    public GameObject mainCube;
    public Material cubeColor;

    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerRigidBody.freezeRotation = true;
    }

    // Update is called once per frame
    private void Update()
    {
        movePlayer();
        speedControl();
        changeCubeColor();

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, floorLayer);

        if (grounded)
        {
            playerRigidBody.linearDamping = groundDrag;
        }
        else
        {
            playerRigidBody.linearDamping = 0;
        }
    }

    private void FixedUpdate()
    {
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        playerRigidBody.AddForce(movementDirection.normalized * movementSpeed * 10f, ForceMode.Force);
    }


    private void movePlayer()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
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

    private void changeCubeColor()
    {   
        Vector3 distanceBetweenPlayerAndCube = mainCube.transform.position - playerRigidBody.transform.position;
        
        if (distanceBetweenPlayerAndCube.magnitude < 5f)
        {
            cubeColor.color = Color.red;
        }
        else
        {
            cubeColor.color = Color.green;
        }
    }
}
