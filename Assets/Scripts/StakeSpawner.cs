using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = Unity.Mathematics.Random;

public class StakeSpawner : MonoBehaviour
{

    public static StakeSpawner MainInstance = null;
    void Awake()
    {
        MainInstance = this;
    }
    void OnDestroy()
    {
        MainInstance = null;
    }


    public Transform[] Spots;
    public StakeRef[] Prefabs;
    private List<StakeRef> Spawned = new();

    public int MaxStakes = 3;

    public float Timer = 3;
    float timer = -1f;

    Random random;

    private void Start()
    {
        random = new Random((uint)System.DateTime.Now.Millisecond);
    }

    private void Update()
    {
        if (Spawned.Count >= MaxStakes)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            Debug.Log("try spawn stake");
            int spot = random.NextInt(0, Spots.Length);
            if (Spawned.Any(a => a.stake.Spot == spot))
            {
                return;
            }

            timer = Timer;
            var stake = Instantiate(Prefabs[random.NextInt(0, Prefabs.Length)], Spots[spot].position, Spots[spot].rotation);
            stake.stake.Spot = spot;
            Spawned.Add(stake);
            Debug.Log("try spawn success");

        }
    }

    public void Remove(StakeRef stake){
        Spawned.Remove(stake);
    }

}
