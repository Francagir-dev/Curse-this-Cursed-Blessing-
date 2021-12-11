using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

public class LangResolver : SingletonPattern<LangResolver>
{
    private Dictionary<string, string> _localizationDictionary;

    public SystemLanguage sysLang;
    private bool _isInit;
    private CSVLoader _csvLoader;

    private void OnEnable()
    {
        _csvLoader = CSVLoader.Instance;
       
    }

    private void Init()
    {
        _csvLoader.LoadCsv();
        //_sysLang = Application.systemLanguage;
        GetDictionary();
    }

    public void ChangeGameLanguage(string language)
    {
        switch (language)
        {
            case "English":
            case "Inglés":
                sysLang = SystemLanguage.English;
                break;
            case "Spanish":
            case "Español":
                sysLang = SystemLanguage.Spanish;
                break;
            default:
                sysLang = Application.systemLanguage;
                break;
        }
    }

    private void GetDictionary()
    {
        switch (sysLang)
        {
            case SystemLanguage.English:
                _localizationDictionary = _csvLoader.GetDictionaryValues("en");
                break;
            case SystemLanguage.Spanish:
                _localizationDictionary = _csvLoader.GetDictionaryValues("es");
                break;
            //   case SystemLanguage.Catalan:
            //       localizationDictionary = _csvLoader.GetDictionaryValues("cat");
            //       break;
            default:
                _localizationDictionary = _csvLoader.GetDictionaryValues("en");
                break;
        }

        _isInit = true;
    }

    public string GetLocalizedValue(string key)
    {
        if (!_isInit) Init();
        string value;
        _localizationDictionary.TryGetValue(key, out value);
        Debug.Log(key + " :" + value);
        return value;
    }
}