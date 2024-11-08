using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 maxSpeed;
    [SerializeField] private Vector2 timeToFullSpeed;
    [SerializeField] private Vector2 timeToStop;
    [SerializeField] private Vector2 stopClamp;
    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Vector2 stopFriction;
    private Rigidbody2D rb;
    private bool isMoving;
    private Camera mainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;

        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;

        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);
    }
    
    // Update is called once per frame
    public void Move()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        Vector2 freaktion = GetFriction();
        Vector2 velociraptor = moveDirection * maxSpeed;

        rb.velocity = velociraptor;

        velociraptor.x = Mathf.Clamp(-freaktion.x * Time.deltaTime, -maxSpeed.x, maxSpeed.x);
        velociraptor.y = Mathf.Clamp(-freaktion.y * Time.deltaTime, -maxSpeed.y, maxSpeed.y);
        isMoving = true;

        isMoving = Mathf.Abs(rb.velocity.x) > stopClamp.x && Mathf.Abs(rb.velocity.y) > stopClamp.y;
        if (!isMoving) {
            moveVelocity = Vector2.zero;
        }

        MoveBound();
    }

    public Vector2 GetFriction()
    {
        return moveDirection != Vector2.zero ? moveFriction : stopFriction;
    }

    public void MoveBound() {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }
    
    public bool IsMoving()
    {
        return rb.velocity != Vector2.zero;
    }
}
