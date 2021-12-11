using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text.RegularExpressions;
using Managers;
using UnityEngine;

public class CSVLoader : SingletonPattern<CSVLoader>
{
    private TextAsset _localizationCsvFile;
    private char _lineSeparator = '\n';
    private char _surround = '"';
    private string[] _fieldSeparator = { "\",\"" };

    public void LoadCsv()
    {
        _localizationCsvFile = Resources.Load<TextAsset>("Localization");
    }

    public Dictionary<string, string> GetDictionaryValues(string attributeID)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        string[] lines = _localizationCsvFile.text.Split(_lineSeparator);

        int attributeIndex = -1;

        string[] headers = lines[0].Split(_fieldSeparator, System.StringSplitOptions.None);

        for (int i = 0; i < headers.Length; i++)
        {
            if (headers[i].Contains(attributeID))
            {
                attributeIndex = i;
                break;
            }
        }

        Regex csvParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];

            string[] fields = csvParser.Split(line);

            for (int f = 0; f < fields.Length; f++)
            {
                fields[f] = fields[f].TrimStart(' ', _surround);
                fields[f] = fields[f].Replace("\"", "");
               // Debug.Log(fields[f]);
            }

            if (fields.Length > attributeIndex)
            {
                var key = fields[0];

                if (dictionary.ContainsKey(key))
                {
                    continue;
                }

                var value = fields[attributeIndex];

                dictionary.Add(key, value);
            }
        }

        return dictionary;
    }
}