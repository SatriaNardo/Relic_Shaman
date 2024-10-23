using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GhostShard : MonoBehaviour
{
    TextMeshProUGUI shardText;
    public static GhostShard Instance;
    public int shard;
    ParticleSystem gs;

    // Start is called before the first frame update
    void Start()
    {
        shardText = GetComponent<TextMeshProUGUI>();
        gs = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
    }

    public void AddShard()
    {
        shard += 1;
        shardText.text = "" + shard;
    }
    public void UpdateCounter()
    {
        shardText.text = "" + shard;
    }

}
