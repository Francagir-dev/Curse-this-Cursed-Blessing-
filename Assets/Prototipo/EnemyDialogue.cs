using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyDialogue", menuName = "Enemy Dialogue")]
public class EnemyDialogue : ScriptableObject
{
    public List<string> secure;
    public List<string> tauntSecure;
    public List<string> tauntScared;
    public List<string> halfScared;
    public List<string> scared;
    public List<string> flee;
    public List<string> laugh;

    public float timeToAnswer;

    public List<string> effective;
    public List<string> ineffective;

    #region Getters
    public string Secure { get => secure[Random.Range(0, secure.Count)]; }
    public string HalfScared { get => halfScared[Random.Range(0, halfScared.Count)]; }
    public string Scared { get => scared[Random.Range(0, scared.Count)]; }
    public string Flee { get => flee[Random.Range(0, flee.Count)]; }
    public string Laugh { get => laugh[Random.Range(0, laugh.Count)]; }
    public string TauntScared { get => tauntScared[Random.Range(0, tauntScared.Count)]; }
    public string TauntSecure { get => tauntSecure[Random.Range(0, tauntSecure.Count)]; }
    #endregion

    public string[] GetAnswers()
    {
        string[] answers = new string[4];

        List<string> ineffectiveAnsw = new List<string>(ineffective);
       
        answers[Random.Range(0, answers.Length)] = effective[Random.Range(0, effective.Count)];

        for (int i = 0; i < answers.Length; i++)
        {
            if (answers[i] != null) continue;

            int rand = Random.Range(0, ineffectiveAnsw.Count);
            answers[i] = ineffectiveAnsw[rand];
            ineffectiveAnsw.RemoveAt(rand);
        }

        return answers;
    }
}
