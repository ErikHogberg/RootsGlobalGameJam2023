using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalRbLocker : MonoBehaviour
{

    public Transform ReferenceObject;
    public Rigidbody Rb;

    [Space]
    public float Limit = 1f;
    public float AdjustForceMul = 1f;

    private void Start()
    {
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


    }

    public void AddLocalForce(Vector3 localForce, ForceMode mode = ForceMode.Force)
    {
        Rb.AddForce(ReferenceObject.TransformDirection(localForce), mode);
    }

}
