using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWeapon : MonoBehaviour
{
    public string closeWeaponName; // eqipped closeweapon

    //Weapon Type
    public bool isHand;
    public bool isAxe;
    public bool isPickAxe;

    public float range; // attack range
    public int damage; // attack damage
    public float workSpeed; // work speed
    public float attackDelay; // attack delay
    public float attackDelayA; // attack activity point
    public float attackDelayB; // attack unactivity point

    public Animator anim; // animation

}
