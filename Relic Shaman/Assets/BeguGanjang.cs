using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeguGanjang : Enemy
{
    // Start is called before the first frame update
    [SerializeField] GameObject rockEffect;
    [SerializeField] public Transform SideAttackTransform; //the middle of the side attack area
    [SerializeField] public Vector2 SideAttackArea; //how large the area of side attack is
    [SerializeField] public Transform DownAttackTransform; //the middle of the down attack area
    [SerializeField] public Vector2 DownAttackArea; //how large the area of down attack is

    public float attackRange;
    public float attackTimer;

    [HideInInspector] public bool facingRight;
    [Header("Ground Check Settings:")]
    [SerializeField] private Transform groundCheckPoint; //point at which ground check happens
    [SerializeField] private float groundCheckY = 0.2f; //how far down from ground chekc point is Grounded() checked
    [SerializeField] private float groundCheckX = 0.5f; //how far horizontally from ground chekc point to the edge of the player is
    [SerializeField] private LayerMask whatIsGround; //sets the ground layer

    int hitCounter;
    bool stunned, canStun;
    bool alive;

    [HideInInspector] public float runSpeed;

    public static BeguGanjang Instance;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    protected override void Start()
    {
        base.Start();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        ChangeState(EnemyStates.Begu_Stage1);
        alive = true;
    }
    public bool Grounded()
    {
        if (Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckY, whatIsGround)
            || Physics2D.Raycast(groundCheckPoint.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround)
            || Physics2D.Raycast(groundCheckPoint.position + new Vector3(-groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(SideAttackTransform.position, SideAttackArea);
        Gizmos.DrawWireCube(DownAttackTransform.position, DownAttackArea);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(health <= 0 && alive)
        {
            Death(0);
        }
        if(!attacking)
        {
            attackCountdown -= Time.deltaTime;
        }
    }
    public void Flip()
    {
        if(PlayerController.Instance.transform.position.x < transform.position.x && transform.localScale.x > 0)
        {
            transform.eulerAngles = new Vector2(transform.eulerAngles.x, 0);
            facingRight = true;
        }
        else
        {
            
            transform.eulerAngles = new Vector2(transform.eulerAngles.x, 180);
            facingRight = false;
        }
    }

    protected override void UpdateEnemyStates()
    {
        if(PlayerController.Instance != null)
        {
            switch (GetCurrentEnemyState)
            {
                case EnemyStates.Begu_Stage1:
                    attackTimer = 5;
                    break;

                case EnemyStates.Begu_Stage2:
                    attackTimer = 3;
                    break;

                case EnemyStates.Begu_Stage3:
                    attackTimer = 7;
                    break;
            }
        }
    }
    protected override void OnCollisionStay2D(Collision2D _other)
    {
        
    }
    #region attacking
    #region variables
    [HideInInspector] public bool attacking;
    [HideInInspector] public float attackCountdown;

    [HideInInspector] public Vector2 moveToPosition;
    [HideInInspector] public bool diveAttack;
    public GameObject divingCollider;
    public GameObject pillar;
    #endregion
    
    #region Control

    public void AttackHandler()
    {
        if(currentEnemyState == EnemyStates.Begu_Stage1)
        {
            if(Vector2.Distance(PlayerController.Instance.transform.position, rb.position) <= attackRange)
            {
                StartCoroutine(DoubleStomp());
            }
            else
            {
            }
        }
        if (currentEnemyState == EnemyStates.Begu_Stage2)
        {
            if (Vector2.Distance(PlayerController.Instance.transform.position, rb.position) <= attackRange)
            {
                StartCoroutine(DoubleStomp());
            }
            else
            {
            }
        }
        if (currentEnemyState == EnemyStates.Begu_Stage3)
        {
            if (Vector2.Distance(PlayerController.Instance.transform.position, rb.position) <= attackRange)
            {
                DiveAttackJump();
            }
            else
            {
                DiveAttackJump();
            }
        }
    }
    public void ResetAllAttacks()
    {
        attacking = false;
        StopCoroutine(DoubleStomp());

        diveAttack = false;
    }

    #endregion
    public override void EnemyHit(float _damageDone, Vector2 _hitDirection, float _hitforce)
    {
        base.EnemyHit(_damageDone, _hitDirection, _hitforce);
        #region health to state
        if (health > 20)
        {
            ChangeState(EnemyStates.Begu_Stage1);
        }
        else if (health >= 10 && health <= 20)
        {
            ChangeState(EnemyStates.Begu_Stage2);
        }
        else if (health < 10)
        {
            ChangeState(EnemyStates.Begu_Stage3);
        }
        else if (health <= 0 && alive)
        {
            Death(0);
        }
        #endregion
    }
    #region Stage 1

    IEnumerator DoubleStomp()
    {
        attacking = true;
        rb.velocity = Vector2.zero;

        anim.SetTrigger("Stomp");
        yield return new WaitForSeconds(0.4f);
        RockAngle();
        yield return new WaitForSeconds(0.5f);
        anim.ResetTrigger("Stomp");
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("Stomp");
        yield return new WaitForSeconds(0.4f);
        RockAngle();
        yield return new WaitForSeconds(0.5f);
        anim.ResetTrigger("Stomp");

        ResetAllAttacks();
    }
    void RockAngle()
    {
        if (PlayerController.Instance.transform.position.x > transform.position.x || PlayerController.Instance.transform.position.x < transform.position.x)
        {
            Instantiate(rockEffect, SideAttackTransform);
        }
    }
    void RockEffectAtAngle(GameObject _rockAngle, int _effectAngle, Transform _attackTransform)
    {
        _rockAngle = Instantiate(_rockAngle, _attackTransform);
        _rockAngle.transform.eulerAngles = new Vector3(0, 0, _effectAngle);
        _rockAngle.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
    }
    #endregion

    #region Stage 2

    void DiveAttackJump()
    {
        attacking = true;
        moveToPosition = new Vector2(PlayerController.Instance.transform.position.x, rb.position.y + 30);
        diveAttack = true;
        anim.SetBool("Jump", true);
    }
    public void Dive()
    {
        anim.SetBool("Dive", true);
        anim.SetBool("Jump", false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerController>() != null && diveAttack)
        {
            other.GetComponent<PlayerController>().TakeDamage(damage * 2);
            PlayerController.Instance.pState.recoilingX = true;
        }
    }
    public void DivingPillars()
    {
        Vector2 _impactPoint = groundCheckPoint.position;
        float _spawnDistance = 35;

        for(int i = 0; i < 10; i++)
        {
            Vector2 _pillarSpawnPointRight = _impactPoint + new Vector2(_spawnDistance, 0);
            Vector2 _pillarSpawnPointLeft = _impactPoint - new Vector2(_spawnDistance, 0);
            Instantiate(pillar, _pillarSpawnPointRight, Quaternion.Euler(0, 0, 0));
            Instantiate(pillar, _pillarSpawnPointLeft, Quaternion.Euler(0, 0, 0));
            _spawnDistance += 35;
        }
        ResetAllAttacks();
    }

    #endregion
    #endregion

    protected override void Death(float _destroyTime)
    {
        ResetAllAttacks();
        alive = false;
        rb.velocity = new Vector2(rb.velocity.x, -25);
        anim.SetTrigger("Death");
    }
    public void DestroyAfterDeath()
    {
        SpawnBoss.Instance.isNotTrigger();
        GameManager.Instance.BeguGajangDefeated = true;
        SaveData.Instance.SaveBossData();
        SaveData.Instance.SavePlayerData();
        Destroy(gameObject);
    }
}
