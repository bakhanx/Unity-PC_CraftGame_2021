using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    public static bool isActivate = false; // activate


    [SerializeField] private Gun currentGun; // equipped gun
    private float currentFireRate; // fire rate 

    //state var
    private bool isReload = false;
    [HideInInspector] public bool isFineSightMode = false;

    private Vector3 originPos; // origin position
    private AudioSource audioSource; // sound effect

    private RaycastHit hitInfo; // ray hit info

    //component
    [SerializeField] private Camera theCam;
    private Crosshair theCrosshair;

    [SerializeField] private GameObject hit_effect_prefab; // hit effect

    void Start()
    {
        originPos = Vector3.zero; // initailize
        audioSource = GetComponent<AudioSource>();
        theCrosshair = FindObjectOfType<Crosshair>();


    }

    // Update is called once per frame

    void Update()
    {
        if (isActivate & !Inventory.inventoryActivated)
        {
            GunFireRateCalc();
            TryFire();
            TryReload();
            TryFineSight();
        }

    }
    // fire rate calculation
    private void GunFireRateCalc()
    {
        if (currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }
    }
    //fire try
    private void TryFire()
    {
        if (Input.GetButton("Fire1") && currentFireRate <= 0 && !isReload)
        {
            Fire();
        }
    }
    // before fire
    private void Fire()
    {
        if (!isReload)
        {
            if (currentGun.currentBulletCount > 0)
                Shoot();
            else
            {
                CancelFineSight();
                StartCoroutine(ReloadCoroutine());
            }

        }

    }
    // after fire
    private void Shoot()
    {
        theCrosshair.FireAnimation();
        currentGun.currentBulletCount--;
        currentFireRate = currentGun.fireRate; // fire speed recalculate
        PlaySE(currentGun.fire_Sound);
        currentGun.muzzleFlash.Play();
        Hit();
        StopAllCoroutines();
        StartCoroutine(RetroActionCoroutine());
    }

    private void Hit()
    {
        if (Physics.Raycast(theCam.transform.position, theCam.transform.forward +
            new Vector3(Random.Range(-theCrosshair.GetAccuracy() - currentGun.accuracy, theCrosshair.GetAccuracy() + currentGun.accuracy), // random : min-max by accuracy
                        Random.Range(-theCrosshair.GetAccuracy() - currentGun.accuracy, theCrosshair.GetAccuracy() + currentGun.accuracy),
                        0)
                        , out hitInfo, currentGun.range))
        {
            //point : coordinates ,normal : surface the ray hit
            GameObject clone = Instantiate(hit_effect_prefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));

            Destroy(clone, 2.0f);
        }
    }

    // reload try
    private void TryReload()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReload && currentGun.currentBulletCount < currentGun.reloadBulletCount)
        {
            CancelFineSight();
            StartCoroutine(ReloadCoroutine());
        }
    }

    public void CancelReload()
    {
        if (isReload)
        {
            StopAllCoroutines();
            isReload = false;
        }
    }
    //reload
    IEnumerator ReloadCoroutine()
    {

        if (currentGun.carryBulletCount > 0)
        {
            isReload = true;

            currentGun.anim.SetTrigger("Reload");

            currentGun.carryBulletCount += currentGun.currentBulletCount;
            currentGun.currentBulletCount = 0;

            yield return new WaitForSeconds(currentGun.reloadTime);

            if (currentGun.carryBulletCount >= currentGun.reloadBulletCount)
            {
                currentGun.currentBulletCount = currentGun.reloadBulletCount;
                currentGun.carryBulletCount -= currentGun.reloadBulletCount;
            }
            else
            {
                currentGun.currentBulletCount = currentGun.carryBulletCount;
                currentGun.carryBulletCount = 0;
            }
            isReload = false;
        }
        else
        {
            Debug.Log("No bullet");
        }
    }
    // finesight try
    private void TryFineSight()
    {
        if (Input.GetButtonDown("Fire2") && !isReload)
        {
            FineSight();
        }
    }
    // finesight cancel
    public void CancelFineSight()
    {
        if (isFineSightMode)
            FineSight();
    }
    // finesight logic
    private void FineSight()
    {
        isFineSightMode = !isFineSightMode;
        currentGun.anim.SetBool("FineSightMode", isFineSightMode);
        theCrosshair.FineSightAnimation(isFineSightMode);

        if (isFineSightMode)
        {
            StopAllCoroutines();
            StartCoroutine(FineSightActivateCoroutine());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(FineSightDeActivateCoroutine());
        }
    }
    // finesight activate
    IEnumerator FineSightActivateCoroutine()
    {
        while (currentGun.transform.localPosition != currentGun.fineSightOriginPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.2f);
            yield return null;
        }
    }
    // finesight deactivate
    IEnumerator FineSightDeActivateCoroutine()
    {
        while (currentGun.transform.localPosition != originPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.2f);
            yield return null;
        }
    }
    // retro action
    IEnumerator RetroActionCoroutine()
    {
        Vector3 recoilBack = new Vector3(currentGun.retroActionForce, originPos.y, originPos.z); // x: child position x is forward/back
        Vector3 retroActionRecoilBack = new Vector3(currentGun.retroActionFineSightForce, currentGun.fineSightOriginPos.y, currentGun.fineSightOriginPos.z);

        if (!isFineSightMode)
        {
            currentGun.transform.localPosition = originPos;

            //retroaction start
            while (currentGun.transform.localPosition.x <= currentGun.retroActionForce - 0.02f)  // 0.02f : because of lerp
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, recoilBack, 0.4f);
                yield return null;
            }

            //origin location
            while (currentGun.transform.localPosition != originPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.1f);
                yield return null;
            }
        }
        else
        {
            currentGun.transform.localPosition = currentGun.fineSightOriginPos;

            //retroaction start
            while (currentGun.transform.localPosition.x <= currentGun.retroActionFineSightForce - 0.02f)  // 0.02f : because of lerp
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, retroActionRecoilBack, 0.4f);
                yield return null;
            }

            //origin location
            while (currentGun.transform.localPosition != currentGun.fineSightOriginPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.1f);
                yield return null;
            }

        }
    }
    // sound play
    private void PlaySE(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }

    public Gun GetGun()
    {
        return currentGun;
    }

    public bool GetFineSightMode()
    {
        return isFineSightMode;
    }

    public void GunChange(Gun _gun)
    {
        if (WeaponManager.currentWeapon != null)
            WeaponManager.currentWeapon.gameObject.SetActive(false);

        currentGun = _gun;
        WeaponManager.currentWeapon = currentGun.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentGun.anim;

        currentGun.transform.localPosition = Vector3.zero;
        currentGun.gameObject.SetActive(true);
        isActivate = true;
    }

}
