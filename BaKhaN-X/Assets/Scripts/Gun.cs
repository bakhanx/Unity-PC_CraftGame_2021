using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string gunName; // gun name
    public float range; // gun range
    public float accuracy; // gun accuracy
    public float fireRate; // rate of fire
    public float reloadTime; // reload time

    public int damage; // damge of gun

    public int reloadBulletCount; // reload bullet count
    public int currentBulletCount; // current remaining bullet count
    public int maxBulletCount; // maximum bullet count
    public int carryBulletCount; // current own bullet count

    public float retroActionForce; // recoil force
    public float retroActionFineSightForce; // FineSight recoil force

    public Vector3 fineSightOriginPos; // FineSight position
    public Animator anim; // grap gun animation
    public ParticleSystem muzzleFlash; // fire effect of muzzle

    public AudioClip fire_Sound; // fire sound

}
