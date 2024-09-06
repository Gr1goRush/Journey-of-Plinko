using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBlock : MonoBehaviour
{
    [SerializeField] private GameObject block;

    private bool dynamic = false;

    public void StartBlinking()
    {
        StartCoroutine(Blinking());
    }

    IEnumerator Blinking()
    {
        dynamic = true;
        while (dynamic)
        {
            block.SetActive(true);
            yield return new WaitForSeconds(Random.Range(1f, 4f));

            block.SetActive(false);
            yield return new WaitForSeconds(Random.Range(1f, 4f));
        }
    }
}
