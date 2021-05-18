using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    [SerializeField]
    private string animalName;
    [SerializeField]
    private int hp;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;

    private float applySpeed;

    private Vector3 direction;


    //State Var
    private bool isAction;
    private bool isWalking;
    private bool isRunning;
    private bool isDead;

    [SerializeField]
    private float walkTime;
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private float runTime;

    private float currentTime;

    //Component
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Rigidbody rigid;
    [SerializeField]
    private BoxCollider boxCol;

    private AudioSource theAudio;
    [SerializeField]
    private AudioClip[] sound_pig_Normal;
    [SerializeField]
    private AudioClip sound_pig_Hurt;
    [SerializeField]
    private AudioClip sound_pig_Dead;

    // Start is called before the first frame update
    void Start()
    {
        theAudio = GetComponent<AudioSource>();
        currentTime = waitTime;
        isAction = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Move();
            Rotation();
            ElapseTime();
        }
    }

    private void Move()
    {
        if (isWalking || isRunning)
            rigid.MovePosition(transform.position + (transform.forward * applySpeed * Time.deltaTime));
    }

    private void Rotation()
    {
        if (isWalking || isRunning)
        {
            Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, direction.y, 0f), 0.01f);
            rigid.MoveRotation(Quaternion.Euler(_rotation));
        }
    }

    private void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
                ReSet();
        }
    }

    private void ReSet()
    {
        isWalking = false;
        isRunning = false;
        isAction = true;

        applySpeed = walkSpeed;
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);

        direction.Set(0f, Random.Range(0f, 360f), 0f);
        RandomAction();
    }

    private void RandomAction()
    {
        RandomSound();
        int _random = Random.Range(0, 4); // wait, eat, peek, walk // range :  0~3 

        if (_random == 0)
            Wait();

        else if (_random == 1)
            Eat();

        else if (_random == 2)
            Peek();

        else if (_random == 3)
            TryWalk();

    }


    private void Wait()
    {
        currentTime = waitTime;
    }
    private void Eat()
    {
        currentTime = waitTime;
        anim.SetTrigger("Eat");
    }
    private void Peek()
    {
        currentTime = waitTime;
        anim.SetTrigger("Peek");
    }
    private void TryWalk()
    {
        isWalking = true;
        currentTime = walkTime;
        anim.SetBool("Walking", isWalking);
        applySpeed = walkSpeed;
    }


    public void Run(Vector3 _targetPos)
    {
        direction = Quaternion.LookRotation(transform.position - _targetPos).eulerAngles;

        currentTime = runTime;
        isWalking = false;
        isRunning = true;
        applySpeed = runSpeed;
        anim.SetBool("Running", isRunning);

    }

    public void Damage(int _dmg, Vector3 _targetPos)
    {
        if (!isDead)
        {
            hp -= _dmg;
            if (hp <= 0)
            {
                Dead();
                return;
            }
            PlaySE(sound_pig_Hurt);
            anim.SetTrigger("Hurt");
            Run(_targetPos);
        }
    }

    private void Dead()
    {
        PlaySE(sound_pig_Dead);
        isWalking = false;
        isRunning = false;
        isDead = true;
        anim.SetTrigger("Dead");
        Destroy(this.gameObject, 3);
    }

    private void RandomSound()
    {
        int _random = Random.Range(0, 3); // normal Sound
        PlaySE(sound_pig_Normal[_random]);
    }

    private void PlaySE(AudioClip _clip)
    {
        theAudio.clip = _clip;
        theAudio.Play();
    }
}
