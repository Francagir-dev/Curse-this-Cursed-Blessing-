using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Shaker))]
public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyDialogue dialogue;
    [SerializeField] DialogueRunner runner;

    [SerializeField] float maxEnemyHp;
    float enemyHp;

    [SerializeField] float timerInWheel = 20;
    float timer;

    Shaker shaker;
    bool isScared = false;
    private void Start()
    {
        shaker = GetComponent<Shaker>();
        enemyHp = maxEnemyHp;
        Detect();
        timer = timerInWheel;
    }

    private void Update()
    {
        runner.Check();
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Attack();
            timer = timerInWheel;
        }
    }

    public void GetAnswer(string answer)
    {
        if (!dialogue.effective.Contains(answer))
        {
            runner.SetText(dialogue.Laugh);
            return;
        }

        float trueDmg = 1;
        //trueDmg /= Vector3.Distance(Player.instance.transform.position, transform.position)/5;
        //trueDmg *= timeLeft;
        AddDamage(trueDmg);
    }

    void Detect()
    {
        runner.SetText(dialogue.Secure);
    }

    void Attack()
    {
        if (isScared) { runner.SetText(dialogue.TauntScared); shaker.BeginShake(); }
        else runner.SetText(dialogue.TauntSecure);

        //TODO: Activate Wheel Player
        Player.instance.playerWheel.SetAnswers(dialogue.GetAnswers(), dialogue.timeToAnswer, this);
        Player.instance.playerChoice.expand = true;
    }

    void AddDamage(float dmg)
    {
        enemyHp -= dmg;
        shaker.BeginShake();

        if (enemyHp <= 0) { Flee(); return; }

        if (enemyHp / maxEnemyHp <= .5f)
        {
            isScared = true;
            runner.SetText(dialogue.Scared);
        }
        else runner.SetText(dialogue.HalfScared);
        

    }

    void Flee()
    {
        runner.SetText(dialogue.Flee);
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().Play("Flee");
        enabled = false;
        StartCoroutine(WaitASec());

        IEnumerator WaitASec()
        {
            float time = 1.5f;
            while (time > 0)
            {
                yield return null;
                time -= Time.deltaTime;
            }
            GameStartup.instance.EndGame(true);
        }
    }
}
