using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private PlayerMoveController controller;

    [SerializeField] private int bulletNumber;
    [SerializeField] private float reloadTimes = 1.5f;
    private float _reloadTime;
    private void Update()
    {
        _reloadTime += Time.fixedDeltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && bulletNumber > 0)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        
        if (_reloadTime < reloadTimes) return;
        _reloadTime = 0;
        BulletCheck(-1);
        controller.RecoilFire();
    }

    void BulletCheck(int changeBullet)
    {
        bulletNumber += changeBullet;
    }
}
