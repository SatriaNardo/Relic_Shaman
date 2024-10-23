using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class GhostShardCollect : MonoBehaviour
{
    ParticleSystem gs;
    [SerializeField]
    GhostShard ghostScript;
    Vector2 ShardDropping;
    List<ParticleSystem.Particle> ghostShard = new List<ParticleSystem.Particle>();

    public static GhostShardCollect Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);

    }
    private void Start()
    {
        gs = GetComponent<ParticleSystem>();

    }

    public void SummonShard(float monsterX, float monsterY)
    {
        transform.localPosition = new Vector2(monsterX, monsterY + 8);
    }
    private void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("Player"))
        {
            ghostScript.AddShard();
        }
    }
    private void OnParticleTrigger()
    {
       
        int triggeredParticles = gs.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, ghostShard);
        for (int i = 0; i < triggeredParticles; i++)
        {
            ParticleSystem.Particle p = ghostShard[i];
            p.remainingLifetime = 0;
            ghostScript.AddShard();
            ghostShard[i] = p;
        }
        gs.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, ghostShard);

    }
}
