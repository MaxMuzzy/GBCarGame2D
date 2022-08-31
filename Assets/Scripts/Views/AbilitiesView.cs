using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AbilitiesView : MonoBehaviour
{
    [SerializeField] private Button _bombAbilityButton;

    public void Init(UnityAction bombAbility)
    {
        _bombAbilityButton.onClick.AddListener(bombAbility);
    }

    protected void OnDestroy()
    {
        _bombAbilityButton.onClick.RemoveAllListeners();
    }
}
