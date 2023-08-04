using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    [SerializeField] private InputReader input;
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Vector3 _moveDirection;
    
    [SerializeField] private bool _isJumping;

    [SerializeField] Transform cameraObject;
    private Vector2 lookDir;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // Subscribing to public events from InputReader
        input.MoveEvent += HandleMove;
        input.LookEvent += HandleLook;
        input.AimEvent += HandleAim;
        input.ShootEvent += HandleShoot;

        input.JumpEvent += HandleJump;
        input.JumpCancelledEvent += HandleCancelledJump;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void HandleMove(Vector2 direction)
    {
        float x = direction.x;
        float y = direction.y;
        _moveDirection = ((cameraObject.forward * y) + (cameraObject.right * x));
        _moveDirection.Normalize();
        _moveDirection.y = 0;
    }

    private void HandleLook(Vector2 direction)
    {
        lookDir = direction;
    }

    private void HandleAim()
    {

    }

    private void HandleShoot()
    {

    }

    private void HandleJump()
    {
        _isJumping = true;
    }


    private void HandleCancelledJump()
    {
        _isJumping = false;
    }

    private void Move()
    {
        
        if (_moveDirection == Vector3.zero)
        {
            return;
        }

        // Movement logic here
        //rb.MovePosition(transform.position + _moveDirection * speed * Time.deltaTime);
        rb.velocity = _moveDirection * speed;
    }

    private void Jump()
    {
        if (_isJumping)
        {
            // Jump Logic here
            transform.position += new Vector3(0, 1, 0) * (jumpSpeed * Time.deltaTime);
        }
    }
}
