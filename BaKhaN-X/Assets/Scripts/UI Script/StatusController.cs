using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    //Hp
    [SerializeField]
    private int hp;
    private int currentHp;

    //Sp
    [SerializeField]
    private int sp;
    private int currentSp;

    //Sp Increase amount
    [SerializeField]
    private int spIncreaseSpeed;

    //Sp Recharge delay
    [SerializeField]
    private int spRechargeTime;
    private int currentSpRechargeTime;

    //Sp decrease ?
    private bool spUsed;

    //Dp
    [SerializeField]
    private int dp;
    private int currentDp;

    //Hungry
    [SerializeField]
    private int hungry;
    private int currentHungry;

    //Hungry decrease speed;
    [SerializeField]
    private int hungryDecreaseTime;
    private int currentHungryDecreaseTime;

    //Thirsty
    [SerializeField]
    private int thirsty;
    private int currentThirsty;

    //Thirsty decrease speed;
    [SerializeField]
    private int thirstyDecreaseTime;
    private int currentThirstyDecreaseTime;

    //Satisfy
    [SerializeField]
    private int satisfy;
    private int currentSatisfy;

    //Image
    [SerializeField]
    private Image[] images_Gague;


    private const int HP = 0, DP = 1, SP = 2, HUNGRY = 3, THIRSTY = 4, SATISFY = 5;


    // Start is called before the first frame update
    void Start()
    {
        currentHp = hp;
        currentSp = sp;
        currentDp = dp;
        currentHungry = hungry;
        currentThirsty = thirsty;
        currentSatisfy = satisfy;
    }

    // Update is called once per frame
    void Update()
    {
        Hungry();
        Thirsty();
        SPRechargeTime();
        SPRecover();
        GagueUpdate();
    }

    private void SPRechargeTime()
    {
        if (spUsed)
        {
            if (currentSpRechargeTime < spRechargeTime)
                currentSpRechargeTime++;
            else
                spUsed = false;
        }
    }

    private void SPRecover()
    {
        if (!spUsed && currentSp < sp)
        {
            currentSp += spIncreaseSpeed;
        }
    }

    private void Hungry()
    {
        if (currentHungry > 0)
        {
            if (currentHungryDecreaseTime <= hungryDecreaseTime)
                currentHungryDecreaseTime++;
            else
            {
                currentHungry--;
                currentHungryDecreaseTime = 0;
            }
        }
        else
            Debug.Log("Hungry : 0");
    }

    private void Thirsty()
    {
        if (currentThirsty > 0)
        {
            if (currentThirstyDecreaseTime <= thirstyDecreaseTime)
                currentThirstyDecreaseTime++;
            else
            {
                currentThirsty--;
                currentThirstyDecreaseTime = 0;
            }
        }
        else
            Debug.Log("Thirsty : 0");

    }

    private void GagueUpdate()
    {
        images_Gague[HP].fillAmount = (float)currentHp / hp;
        images_Gague[SP].fillAmount = (float)currentSp / sp;
        images_Gague[DP].fillAmount = (float)currentDp / dp;
        images_Gague[HUNGRY].fillAmount = (float)currentHungry / hungry;
        images_Gague[THIRSTY].fillAmount = (float)currentThirsty / thirsty;
        images_Gague[SATISFY].fillAmount = (float)currentSatisfy / satisfy;
    }

    public void IncreaseHP(int _count)
    {
        if (currentHp + _count < hp)
            currentHp += _count;
        else
            currentHp = hp;
    }

    public void DecreaseHp(int _count)
    {
        if (currentDp > 0)
        {
            DecreaseDp(_count);
            return;
        }
        currentHp -= _count;

        if (currentHp <= 0)
            Debug.Log("HP : 0");
    }

    public void IncreaseDp(int _count)
    {
        if (currentDp + _count < dp)
            currentDp += _count;
        else
            currentDp = dp;
    }

    public void DecreaseDp(int _count)
    {
        currentDp -= _count;

        if (currentDp <= 0)
            Debug.Log("DP : 0");
    }

    public void IncreaseHungry(int _count)
    {
        if (currentHungry + _count < hungry)
            currentHungry += _count;
        else
            currentHungry = hungry;
    }

    public void DecreaseHungry(int _count)
    {
        if (currentHungry - _count < 0)
            currentHungry = 0;

        else
        {
            currentHungry -= _count;
            Debug.Log("Hungry : 0");
        }
    }

    public void IncreaseThirsty(int _count)
    {
        if (currentThirsty + _count < thirsty)
            currentThirsty += _count;
        else
            currentThirsty = thirsty;
    }

    public void DecreaseThirsty(int _count)
    {
        if (currentThirsty - _count < 0)
            currentThirsty = 0;

        else
        {
            currentThirsty -= _count;
            Debug.Log("Thirsty : 0");
        }
    }

    public void DecreaseStamina(int _count)
    {
        spUsed = true;
        currentSpRechargeTime = 0;

        if (currentSp - _count > 0)
            currentSp -= _count;
        else
            currentSp = 0;
    }

    public int GetCurrentSp()
    {
        return currentSp;
    }

}