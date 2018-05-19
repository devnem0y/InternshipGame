using System.Collections;
using UnityEngine;

public class Car : MonoBehaviour {

    private GameManager gm;
    private AudioManager am;

    private CarController carController;

    [Range(0f, 2500f)]
    public float forceMotor = 0f;
    [Range(0f, 1000f)]
    public float rotationSpeed = 0f;

    public ParticleSystem particleWF, particleWB;
    private Rigidbody2D body;
    public WheelJoint2D jointWF, jointWB;
    private GameObject wFront, wBack;

    private bool grounded;
    private int curValFlip;
    private bool _backFlip;
    private bool _block;
    private float distance = 0f;
    private float startPosX;

    private float timerFly = 0f;
    private float timerFlying = 0f;

    private bool isCrash;
    private Carpet carpet;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        wFront = transform.GetChild(0).gameObject;
        wBack = transform.GetChild(1).gameObject;

        gm = FindObjectOfType<GameManager>();
        am = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        carController = new CarController(body, wFront, wBack, transform, forceMotor, rotationSpeed);
        carpet = transform.GetChild(3).transform.GetComponent<Carpet>();

        startPosX = transform.position.x;
    }

    private void Update()
    {
        if (!isCrash)
        {
            carController.Controller(IsGrounded());
            OnEffects();

            AddScoreOnDistance(20f);
            AddScoreInFliying(2f, 1f);
            BackFlip();

            if (transform.position.y <= -20f || carpet.IsCarpet())
            {
                isCrash = true;
                am.PlayDestruction();
                am.PlayWheelImpact();
            }
        } else
        {
            jointWF.enabled = false;
            jointWB.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (!isCrash) carController.Move(IsGrounded());
        if (body.velocity.x >= 28f) body.velocity = new Vector2(28f, body.velocity.y);
    }

    public float GetSpeed()
    {
        return body.velocity.x;
    }

    public float GetVerticalSpeed()
    {
        return body.velocity.y;
    }

    public void InitSkin()
    {
        Sprite skin = null;
        Sprite wheelFront = null;
        Sprite wheelBack = null;
        Gradient trail = null;

        for (int i = 0; i < Data.items.Length; i++)
        {
            if (Data.items[i] == "ACTUAL")
            {
                // skin Sprite
                skin = gm.storeContent.transform.GetChild(i).GetComponent<Item>().Car;
                // trail
                trail = gm.storeContent.transform.GetChild(i).GetComponent<Item>().Trail;
                // wheels sprites
                wheelFront = gm.storeContent.transform.GetChild(i).GetComponent<Item>().WheelF;
                wheelBack = gm.storeContent.transform.GetChild(i).GetComponent<Item>().WheelB;
            }
        }

        GetComponent<SpriteRenderer>().sprite = skin;
        wFront.GetComponent<SpriteRenderer>().sprite = wheelFront;
        wBack.GetComponent<SpriteRenderer>().sprite = wheelBack;
        transform.GetChild(2).GetComponent<TrailRenderer>().colorGradient = trail;

        particleWF.Stop();
        particleWB.Stop();
    }

    private void AddScoreInFliying(float time1, float time2)
    {
        if (!IsGrounded())
        {
            if (timerFly >= time1)
            {
                if (timerFlying >= time2)
                {
                    GameParams.AddScore(1);
                    GameParams.AddLastScore(1);
                    timerFlying = 0f;
                }
                timerFlying += Time.deltaTime;
            }
            timerFly += Time.deltaTime;
        }
        else if (IsGrounded())
        {
            timerFly = 0f;
            timerFlying = 0f;
        }
    }

    private void AddScoreOnDistance(float offsetD)
    {
        if (distance >= offsetD)
        {
            startPosX = transform.position.x;
            distance = 0f;
            GameParams.AddScore(1);
            GameParams.AddLastScore(1);
        }
        distance = transform.position.x - startPosX;
    }

    public bool IsCrash()
    {
        return isCrash;
    }

    private void BackFlip()
    {
        if (_backFlip)
        {
            if (wFront.GetComponent<CollisionWheel>().IsGrounded() && wBack.GetComponent<CollisionWheel>().IsGrounded())
            {
                // Landing both wheels
                Landing("Cool Flip!", 2);
            }
            else if (wFront.GetComponent<CollisionWheel>().IsGrounded() || wBack.GetComponent<CollisionWheel>().IsGrounded())
            {
                // Landing one wheel
                Landing("Bravo!", 1);
            }

        }
    }

    private void Landing(string infoText, int score)
    {
        gm.GetGUI().infoDrop.SetActive(true);
        gm.GetGUI().drop.text = infoText;
        gm.GetGUI().valDrop.text = "+" + score.ToString();
        Debug.Log(infoText + " : " + score.ToString());
        GameParams.AddScore(score);
        GameParams.AddLastScore(score);
        GameParams.SetScore(GameParams.GetScore() * curValFlip);
        GameParams.SetLastScore(GameParams.GetScore());
        am.PlayLanding();
        curValFlip = 0;
        StartCoroutine(HideInfoDrop(1f));
        _backFlip = false;
    }

    private IEnumerator HideInfoDrop(float time)
    {
        yield return new WaitForSeconds(time);
        gm.GetGUI().infoDrop.SetActive(false);
    }

    public bool IsBackFlip()
    {
        return _backFlip;
    }
    public void SetBackFlip(bool backFlip)
    {
        _backFlip = backFlip;
    }

    public bool IsBlock()
    {
        return _block;
    }
    public void SetBlock (bool block)
    {
        _block = block;
    }

    public int GetCurrValFlip()
    {
        return curValFlip;
    }
    public void SetCurrValFlip(int val)
    {
        curValFlip = val;
    }

    private bool IsGrounded()
    {
        if ((wFront.GetComponent<CollisionWheel>().IsGrounded() || !wFront.GetComponent<CollisionWheel>().IsGrounded()) && wBack.GetComponent<CollisionWheel>().IsGrounded()) grounded = true;
        else if (!wFront.GetComponent<CollisionWheel>().IsGrounded() && !wBack.GetComponent<CollisionWheel>().IsGrounded()) grounded = false;

        return grounded;
    }

    private void OnEffects()
    {
        if (carController.GetMovement() != 0f)
        {
            if (wFront.GetComponent<CollisionWheel>().IsGrounded() && wBack.GetComponent<CollisionWheel>().IsGrounded())
            {
                particleWF.Play();
                particleWB.Play();
            }
            else if (!wFront.GetComponent<CollisionWheel>().IsGrounded() && wBack.GetComponent<CollisionWheel>().IsGrounded())
            {
                particleWF.Stop();
                particleWB.Play();
            }
            else if (!wFront.GetComponent<CollisionWheel>().IsGrounded() && !wBack.GetComponent<CollisionWheel>().IsGrounded())
            {
                particleWF.Stop();
                particleWB.Stop();
            }
        }
        else
        {
            particleWF.Stop();
            particleWB.Stop();
        }
    }
}
