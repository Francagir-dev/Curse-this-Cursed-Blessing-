using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UIResolver : MonoBehaviour
{
    public string Identifier
    {
        get => identifier;
        set => identifier = value;
    }

    [SerializeField] private string identifier;
    private TextMeshProUGUI _textLocalizable;
    private LangResolver[] _resolvers;
    private LangResolver _langResolver;

    private void Awake()
    {
        _resolvers = FindObjectsOfType<LangResolver>();
    }

    private void Start()
    {
        SetTexts();
    }

    public void SetTexts()
    {
        _textLocalizable = GetComponent<TextMeshProUGUI>();
        foreach (LangResolver langresolver in _resolvers)
        {
            string value = langresolver.GetLocalizedValue(identifier);
            _textLocalizable.text = value;
        }
    }

    public void GetAllTexts()
    {
        _resolvers = FindObjectsOfType<LangResolver>();
        foreach (LangResolver resolver in _resolvers)
        {
            resolver.enabled = false;
            resolver.enabled = true;
        }

        SetTexts();
    }
}