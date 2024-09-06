using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : PullObject<Ball>
{
    [SerializeField] private float accelerationVelocity = 5f, bounceForce = 1f, minBounceVelocity = 3f, maxBounceVelocity = 5f, obstacleBounceForce = 1f; //accelerationToForce = 1f, 

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _animator;

    private float lastAccelerationX = 0f;

    private void OnEnable()
    {
        _animator.Play("Default");
    }

    void Update()
    {
        float xOffset, x;
#if UNITY_EDITOR
        xOffset = Input.GetAxisRaw("Horizontal") * 0.001f;
        x = xOffset;
#else
        x = Input.acceleration.x;
        xOffset = x - lastAccelerationX;
#endif

        lastAccelerationX = x;

        _rigidbody.velocity += new Vector2(accelerationVelocity * xOffset, 0f);
        if (transform.position.y < -10f)
        {
            UnpullThis();
        }
    }

    public void OnTargetMarkReached()
    {
        UnpullThis();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ball"))
        {
            GameSoundsController.Instance.PlayOneShot("bounce");

            Vector2 bounceVelocity = collision.relativeVelocity * bounceForce;
            if(bounceVelocity.x > 0 && bounceVelocity.x < minBounceVelocity)
            {
                bounceVelocity.x = minBounceVelocity;
            }
            if(bounceVelocity.x > maxBounceVelocity)
            {
                bounceVelocity.x = maxBounceVelocity;
            }

            if (bounceVelocity.y > 0 && bounceVelocity.y < minBounceVelocity)
            {
                bounceVelocity.y = minBounceVelocity;
            }
            if (bounceVelocity.y > maxBounceVelocity)
            {
                bounceVelocity.y = maxBounceVelocity;
            }

            _rigidbody.AddForce(bounceVelocity, ForceMode2D.Impulse);

            _animator.SetTrigger("Bounce");
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            float d = Random.Range(0, 2) == 1 ? 1 : -1;
            _rigidbody.AddForce(new Vector2(d, 0) * obstacleBounceForce, ForceMode2D.Impulse);

            GameSoundsController.Instance.PlayOneShot("bounce");

            _animator.SetTrigger("Bounce");
        }
    }
}
