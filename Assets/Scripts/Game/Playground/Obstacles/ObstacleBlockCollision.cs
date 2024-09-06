using System.Collections;
using UnityEngine;

public class ObstacleBlockCollision : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameSoundsController.Instance.PlayOneShot("obstacle_block");

            _animator.SetTrigger("Bounce");
        }
    }
}