using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeguEvents : MonoBehaviour
{
    void RockDamagePlayer()
    {
        if(PlayerController.Instance.transform.position.x > transform.position.x || PlayerController.Instance.transform.position.x < transform.position.x)
        {
            Hit(BeguGanjang.Instance.SideAttackTransform, BeguGanjang.Instance.SideAttackArea);
        }
    }
    void Hit(Transform _attackTransform, Vector2 _attackArea)
    {
        Collider2D[] _objectsToHit = Physics2D.OverlapBoxAll(_attackTransform.position, _attackArea, 0);
        for(int i = 0; i < _objectsToHit.Length; i++)
        {
            if (_objectsToHit[i].GetComponent<PlayerController>() != null && !PlayerController.Instance.pState.invincible)
            {
                _objectsToHit[i].GetComponent<PlayerController>().TakeDamage(BeguGanjang.Instance.damage);
                if (PlayerController.Instance.pState.alive)
                {
                    PlayerController.Instance.HitStopTime(0, 5, 0.5f);
                }
            }
        }
    }
    
}
