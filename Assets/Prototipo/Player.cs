using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player instance;

    public int life = 5;
    public float speed;
    public ImageFollower playerText;
    public ImageFollower playerChoice;
    public AnswerWheel playerWheel;

    [Header("Dash")]
    public float speedDash;
    public float dashDist;
    public float timeDashInv;
    public float timeDashCool;

    Rigidbody rig;
    Collider col;

    bool dashEnab;
    float xMove = 0;
    float zMove = 0;

    PlayerInput input;

    [SerializeField] DialogueRunner runner;
    [SerializeField] TextMeshProUGUI textLife;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
        rig = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        //input = new PlayerInput();
        //input.Enable();
        //input.Player.Movement.performed += OnMove;
        //input.Player.Dash.performed += _ => Dash();
    }

    private void FixedUpdate()
    {
        textLife.text = "Life: " + life;

        rig.velocity = new Vector3(xMove * speed, 0, zMove * speed);
        if (xMove != 0 || zMove != 0)transform.rotation = Quaternion.LookRotation(rig.velocity);
    }

    public void OnMove(InputAction.CallbackContext cont)
    {
        Vector2 move = cont.ReadValue<Vector2>();
        Debug.Log(move);
        xMove = move.x;
        zMove = move.y;
    }

    void Dash()
    {
        if (!dashEnab) return;

        Vector3 direction;

        if (xMove + zMove == 0) direction = transform.forward;
        else direction = new Vector3(xMove, 0, zMove);



        StartCoroutine(Invecibility());
        StartCoroutine(Cooldown());

        IEnumerator Invecibility()
        {
            col.enabled = false;
            yield return new WaitForSeconds(timeDashInv);
            col.enabled = true;
        }

        IEnumerator Cooldown()
        {
            dashEnab = false;
            yield return new WaitForSeconds(timeDashCool);
            dashEnab = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        runner.Check();
    }
}
