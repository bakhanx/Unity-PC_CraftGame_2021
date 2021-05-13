using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    //component
    [SerializeField]
    private GunController theGunController;
    private Gun currentGun;

    //HUD on/off
    [SerializeField]
    private GameObject go_BulletHUD;

    //bullet count text
    [SerializeField]
    private Text[] text_bullet;

    // Update is called once per frame
    void Update()
    {
        CheckBullet();
    }

    private void CheckBullet()
    {
        currentGun = theGunController.GetGun();
        text_bullet[0].text = currentGun.carryBulletCount.ToString();
        text_bullet[1].text = currentGun.reloadBulletCount.ToString();
        text_bullet[2].text = currentGun.currentBulletCount.ToString();
    }
}
