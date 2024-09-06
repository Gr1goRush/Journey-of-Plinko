using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

    public delegate void GameAction();

public static class Utility
{
    public static Quaternion GetLookRotation(Vector3 position, Vector3 targetPosition)
    {
        Vector3 dir = targetPosition - position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public static IEnumerable<T> GetRandomEnumerable<T>(IEnumerable<T> arr)
    {
        System.Random random = new System.Random();
        return arr.OrderBy(x => random.Next());
    }

    public static void Invoke(this MonoBehaviour monoBehaviour, GameAction action, float time)
    {
        monoBehaviour.StartCoroutine(Invoking(action, time));
    }

    private static IEnumerator Invoking(GameAction action, float time)
    {
        yield return new WaitForSecondsRealtime(time);

        action?.Invoke();
    }


    public static void OnAnimation(this MonoBehaviour monoBehaviour, Animator animator, string name, GameAction call, float targetTime)
    {
        monoBehaviour.StartCoroutine(AnimationWatch(animator, name, call, targetTime));
    }

    static IEnumerator AnimationWatch(Animator animator, string name, GameAction call, float targetTime)
    {
        bool isAnimation = false;
        while (!isAnimation)
        {
            isAnimation = animator.GetCurrentAnimatorStateInfo(0).IsName(name);
            yield return null;
        }

        isAnimation = true;
        while (isAnimation)
        {
            AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            isAnimation = animatorStateInfo.IsName(name);
            if (animatorStateInfo.normalizedTime >= targetTime)
            {
                break;
            }
            yield return null;
        }

        if (call != null)
        {
            call.Invoke();
        }
    }
}