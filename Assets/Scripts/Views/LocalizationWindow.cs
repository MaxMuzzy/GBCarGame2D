using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

public class LocalizationWindow : MonoBehaviour
{
    private Transform _parentUi;
    [SerializeField]
    private Button _buttonPrefab;

    public void Init(Transform parentUi)
    {
        _parentUi = parentUi;
        StartCoroutine(ChangeLocale());
    }

    private IEnumerator ChangeLocale()
    {
        yield return LocalizationSettings.InitializationOperation;
        var locales = LocalizationSettings.AvailableLocales.Locales;
        foreach (var locale in locales)
        {
            CreateButton(locale);
        }
    }

    private void CreateButton(Locale locale)
    {
        var gameObject = GameObject.Instantiate(_buttonPrefab, this.gameObject.transform);
        var text = gameObject.GetComponentInChildren<Text>();
        if (text != null)
            text.text = locale.Identifier.Code;
        gameObject.onClick.AddListener(() => LocalizationSettings.SelectedLocale = locale);
    }
}
