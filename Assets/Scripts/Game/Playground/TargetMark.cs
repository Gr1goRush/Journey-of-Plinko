using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum TargetMarkType
{
    Multiplier, Bonus
}

public class TargetMark : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TextMeshPro typeText;

    private TargetMarkType type;
    private float multiplier;

    public void Set(TargetMarkConfiguration configuration)
    {
        type = configuration.type;
        typeText.text = type == TargetMarkType.Bonus ? "Bonus" : "X" + configuration.multiplier;
        multiplier = configuration.multiplier;
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && collision.gameObject.activeSelf)
        {
            GameSoundsController.Instance.PlayOneShot("target_mark");

            Ball ball = collision.gameObject.GetComponent<Ball>();
            ball.OnTargetMarkReached();

            if(type == TargetMarkType.Multiplier)
            {
                GameController.Instance.MultipleBalance(multiplier);
            }
            else
            {
                GameController.Instance.StartSuperGame();
            }

            VibrationManager.Instance.Vibrate();
        }
    }
}
