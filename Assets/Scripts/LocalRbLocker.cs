using UnityEngine;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

public class LocalRbLocker : MonoBehaviour
{

    static bool randomInit = false;
    static Random random;

    public Transform ReferenceObject;
    public Rigidbody Rb;

    [Space]
    public float Limit = 1f;
    public float AdjustForceMul = 1f;
    public float MinVelocity = .01f;
    public float JostleForceMin = .2f;
    public float JostleForceMax = .2f;

    private void Start()
    {

        if(!randomInit){
            random = new Random();
            randomInit = true;
        }

        if (!Rb) Rb = GetComponent<Rigidbody>();
        if (!Rb)
        {
            enabled = false;
            Debug.LogWarning($"rb locker {gameObject.name} is missing its rb");
            return;
        }

        if (!ReferenceObject) ReferenceObject = transform.parent;
    }

    private void FixedUpdate()
    {
        Vector3 localPos = ReferenceObject.InverseTransformPoint(Rb.position);
        localPos.z = Mathf.Clamp(localPos.z, -Limit, Limit);
        Rb.position = ReferenceObject.TransformPoint(localPos);

        Vector3 localVelocity = ReferenceObject.InverseTransformDirection(Rb.velocity);
        localVelocity.z = -localPos.z * AdjustForceMul;
        Rb.velocity = ReferenceObject.TransformDirection(localVelocity);

        if(Rb.velocity.sqrMagnitude <MinVelocity){
            Vector3 localForce = random.NextFloat3(JostleForceMin, JostleForceMax);
            Vector3 worldForce = ReferenceObject.TransformDirection(localForce);
            Rb.AddForce(worldForce, ForceMode.Impulse);
        }
    }

    public void AddLocalForce(Vector3 localForce, ForceMode mode = ForceMode.Force)
    {
        Rb.AddForce(ReferenceObject.TransformDirection(localForce), mode);
    }

}
