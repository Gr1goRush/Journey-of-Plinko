using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SuperGameCard : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Animator _animator;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject addBalanceFrameObject;

    public void AddClickListener(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }

    public void ShowDefaultAnimation()
    {
        _animator.SetBool("Opened", false);
        _animator.Play("Hidden");
    }

    public void ShowOpenAnimation(AnimatorOverrideController overrideController)
    {
        _animator.runtimeAnimatorController = overrideController;
        _animator.SetBool("Opened", true);
    }

    public void SetInteractable(bool v)
    {
        _button.interactable = v;
    }

    public void SetAddBalanceFrameActive(bool v)
    {
        addBalanceFrameObject.SetActive(v);
    }
}