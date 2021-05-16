using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private int hp; // rock's hp
    [SerializeField]
    private float destroyTime; // fragment destroy time

    [SerializeField]
    private SphereCollider col; // sphere collider

    //Game Object
    [SerializeField]
    private GameObject go_rock; // normal rock
    [SerializeField]
    private GameObject go_debris; // breaked rock
    [SerializeField]
    private GameObject go_effect_prefabs; // mining effect
    [SerializeField]
    private GameObject go_rock_item_prefab; // rock item


    [SerializeField]
    private int count; // rock item

    // Sound Name
    [SerializeField]
    private string strike_Sound;
    [SerializeField]
    private string destroy_Sound;


    public void Mining()
    {
        SoundManager.instance.PlaySE(strike_Sound);


        var clone = Instantiate(go_effect_prefabs, col.bounds.center, Quaternion.identity);
        Destroy(clone, destroyTime);

        hp--;
        if (hp <= 0)
            Destruction();
    }

    private void Destruction()
    {

        SoundManager.instance.PlaySE(destroy_Sound);
        col.enabled = false;
        for (int i = 0; i < count; i++)
        {
            Instantiate(go_rock_item_prefab, go_rock.transform.position, Quaternion.identity);
        }


        Destroy(go_rock);

        go_debris.SetActive(true);

        Destroy(go_debris, destroyTime);


    }
}
