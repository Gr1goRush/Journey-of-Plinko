using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwistButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    void Start()
    {
        _button.onClick.AddListener(ClickButton);    
    }

    void ClickButton()
    {
        GameController.Instance.Twist();
    }
}
