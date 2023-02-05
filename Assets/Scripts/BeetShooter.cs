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

    public float MinLaunchForce = 1;
    public float MaxLaunchForce = 2;

    Random random;

    private void Start() {
        random = new((uint)System.DateTime.Now.Millisecond);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.grey;
        Gizmos.DrawLine(MinDir.position, MinDir.position + MinDir.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(MaxDir.position, MaxDir.position + MaxDir.forward);
    }

    public void Shoot(){
        float lerp = random.NextFloat(0, 1);


        Vector3 pos = Vector3.Lerp(MinDir.position, MaxDir.position, lerp);
        Quaternion rot = Quaternion.Lerp(MinDir.rotation, MaxDir.rotation, lerp);

        var newBeet = Instantiate(BeetPrefab, pos, rot);

        lerp = random.NextFloat(0, 1);
        newBeet.GetComponent<Rigidbody>().AddForce(rot * Vector3.forward * Mathf.Lerp(MinLaunchForce, MaxLaunchForce, lerp), ForceMode.VelocityChange);

    }

}
