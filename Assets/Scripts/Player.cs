using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        kickButton = FindObjectOfType<Button>();

        rigidbody2d = GetComponent<Rigidbody2D>();
        //ballRB2d = FindObjectOfType<Ball>().GetComponent<Rigidbody2D>();

        angle = 0;
        oldAngle = 0;
        rotateDir = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2d.velocity = new Vector3(joystick.Horizontal * 500 * Time.deltaTime,
                                         joystick.Vertical * 500 * Time.deltaTime,
                                         0);
        if (ball)
        {
            ballRB2d.velocity = rigidbody2d.velocity;

            if (joystick.Direction != new Vector2(0, 0))
                RotateBody();

            if (kickButton.isPressed)
            {
                Kick();
            }
        }
    }

    private void Kick()
    {
        var X = ball.transform.position.x - transform.position.x;
        var Y = ball.transform.position.y - transform.position.y;
        DetachBall();
        ballRB2d.AddForce(new Vector2(X, Y) * 10000);
    }

    private void RotateBody()
    {
        Vector2 dir = joystick.Direction.normalized;
        Vector2 ballDir = (ballRB2d.position - rigidbody2d.position).normalized;

        if (V2Equal(dir,ballDir))
            return;

        angle = Vector2.Angle(dir, ballDir);
        
        rotateDir = angle > oldAngle ? -rotateDir : rotateDir;
        transform.Rotate(0, 0, rotateDir);
        oldAngle = angle;
    }

    public bool V2Equal(Vector2 a, Vector2 b)
    {
        return Vector2.SqrMagnitude(a - b) < 0.01;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ball")
        {
            ball = collision.collider.GetComponent<Ball>();
            collision.collider.transform.parent = transform; //temp
            Debug.Log("On your feet!");
        }

        else
        {
            if (ball)
                DetachBall();
        }
    }

    private void DetachBall()
    {
        ball.transform.parent = null;
        ball = null;
    }

    protected Joystick joystick;
    protected Button kickButton;

    private Rigidbody2D rigidbody2d;

    private Ball ball;
    private Rigidbody2D ballRB2d;

    private float rotateDir;

    private float angle;
    private float oldAngle;
}