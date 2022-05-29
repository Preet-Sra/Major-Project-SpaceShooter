using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootAnim : MonoBehaviour
{
    private Boss boss;

    private void Start()
    {
        boss = transform.parent.GetComponent<Boss>();
    }
    public void Shoot()
    {
        boss.Shoot();
    }
}
