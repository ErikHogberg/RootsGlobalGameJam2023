using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

public class BeetShooter : MonoBehaviour
{
    public GameObject BeetPrefab;
    public Transform MinDir;
    public Transform MaxDir;

    Random random;

    private void Start() {
        random = new((uint)System.DateTime.Now.Millisecond);
    }

    public void Shoot(){
        float lerp = random.NextFloat(0, 1);
    }

}
