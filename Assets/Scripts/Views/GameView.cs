using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField]
    private Button _startFightButton;

    public void Init(UnityAction fight)
    {
        _startFightButton.onClick.AddListener(fight);
    }
}
