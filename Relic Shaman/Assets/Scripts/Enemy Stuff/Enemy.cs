using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float recoilLength;
    [SerializeField] protected float recoilFactor;
    [SerializeField] protected bool isRecoiling = false;

    [SerializeField] public float speed;

    [Header("Sound")]
    [SerializeField] AudioClip[] SFX;
    AudioSource audioSource;

    [SerializeField] public float damage;
    [SerializeField] protected GameObject orangeBlood;

    public GameObject shard;

    protected float recoilTimer;
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected Animator anim;


    
    protected enum EnemyStates
    {
        // Crawler
        Crawler_Idle,
        Crawler_Flip,

        // Bat
        Bat_Idle,
        Bat_Chase,
        Bat_Stuned,
        Bat_Death,

        // Charger
        Charger_Idle,
        Charger_Surprised,
        Charger_Charge,

        //Begu
        Begu_Stage1,
        Begu_Stage2,
        Begu_Stage3,
    }
    protected EnemyStates currentEnemyState;

    protected virtual EnemyStates GetCurrentEnemyState
    {
        get { return currentEnemyState; }
        set
        {
            if(currentEnemyState != value)
            {
                currentEnemyState = value;

                ChangeCurrentAnimation();
            }
        }
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        shard = GhostShardCollect.Instance.gameObject;
        shard.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

}
    // Update is called once per frame
    protected virtual void Update()
    {
        if (GameManager.Instance.gameisPaused) return;
        if(isRecoiling)
        {
            if(recoilTimer < recoilLength)
            {
                recoilTimer += Time.deltaTime;
            }
            else
            {
                isRecoiling = false;
                recoilTimer = 0;
            }
        }
        else
        {
            UpdateEnemyStates();
        }
    }

    public virtual void EnemyHit(float _damageDone, Vector2 _hitDirection, float _hitForce)
    {
        health -= _damageDone;
        if(!isRecoiling)
        {
            audioSource.clip = SFX[1];
            audioSource.Play();
            GameObject _orangeBlood = Instantiate(orangeBlood, transform.position, Quaternion.identity);
            Destroy(_orangeBlood, 5.5f);
            rb.velocity =_hitForce * recoilFactor * _hitDirection;
            isRecoiling = true;
        }
    }
    protected virtual void OnCollisionStay2D(Collision2D _other)
    {
        if(_other.gameObject.CompareTag("Player") && !PlayerController.Instance.pState.invincible && !PlayerController.Instance.pState.invincible && health > 0)
        {
            Attack();
            if(PlayerController.Instance.pState.alive)
            {
                PlayerController.Instance.HitStopTime(0, 5, 0.5f);
            }
            
        }
    }

    protected virtual void Death (float _destroyTime)
    {
        shard.SetActive(true);
        rb.gravityScale = 12;
        float MonsterX = transform.localPosition.x;
        float MonsterY = transform.localPosition.y;    
        GhostShardCollect.Instance.SummonShard(MonsterX, MonsterY);
        StartCoroutine(WaitForSeconds());
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        
        Destroy(gameObject, _destroyTime);
    }
    protected virtual void UpdateEnemyStates(){ }
    protected virtual void ChangeCurrentAnimation(){ }
    protected void ChangeState(EnemyStates _newState)
    {
        GetCurrentEnemyState = _newState;
    }
    protected virtual void Attack()
    {
        PlayerController.Instance.TakeDamage(damage);
    }
    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(5f);
    }
}
