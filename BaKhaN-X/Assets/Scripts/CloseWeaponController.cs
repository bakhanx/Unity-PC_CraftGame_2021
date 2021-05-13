using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract class : imcomplete class
public abstract class CloseWeaponController : MonoBehaviour
{
    // current equipped hand type weapon
    [SerializeField]
    protected CloseWeapon currentCloseWeapon;

    // attacking

    protected bool isAttack = false;
    protected bool isSwing = false;

    protected RaycastHit hitInfo;

    // Update is called once per frame


    protected void TryAttack()
    {
        if (Input.GetButton("Fire1"))
        {
            if (!isAttack)
            {
                StartCoroutine(AttackCoroutine());
            }
        }
    }
    protected IEnumerator AttackCoroutine()
    {
        isAttack = true;
        currentCloseWeapon.anim.SetTrigger("Attack");

        yield return new WaitForSeconds(currentCloseWeapon.attackDelayA);
        isSwing = true;
        StartCoroutine(HitCoroutine());

        //attack activity point;

        yield return new WaitForSeconds(currentCloseWeapon.attackDelayB);
        isSwing = false;

        yield return new WaitForSeconds(currentCloseWeapon.attackDelay - currentCloseWeapon.attackDelayA - currentCloseWeapon.attackDelayB);
        isAttack = false;
    }

    //abstract coroutine : imcomplete
    protected abstract IEnumerator HitCoroutine();


    protected private bool CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, currentCloseWeapon.range))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    //virtual function : complete function but can change function
    public virtual void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        if (WeaponManager.currentWeapon != null)
            WeaponManager.currentWeapon.gameObject.SetActive(false);

        currentCloseWeapon = _closeWeapon;
        WeaponManager.currentWeapon = currentCloseWeapon.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentCloseWeapon.anim;

        currentCloseWeapon.transform.localPosition = Vector3.zero;
        currentCloseWeapon.gameObject.SetActive(true);


    }
}

