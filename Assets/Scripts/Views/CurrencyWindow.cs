using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyWindow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _diamondText;
    [SerializeField]
    private TextMeshProUGUI _woodText;

    public void RefreshText(int wood, int diamond)
    {
        if (_diamondText != null)
            _diamondText.text = diamond.ToString();
        if (_woodText != null)
            _woodText.text = wood.ToString();
    }
}
