using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasketMover : MonoBehaviour
{
    public Transform GaemPlane;
    public Camera Cam;
    public Rigidbody Rb;

    [Space]
    public float ForceMul = 1f;
    public float MaxForce = 1f;
    public float MinForce = .1f;
    public float RotSpeed = 1f;
    public float DirThreshold = .1f;

    float rotBuffer;
    float rotTarget;

    Utils.FixedVectorQueue posCache;
    public int PosCacheSize = 5;

    private void Start()
    {
        if (!Rb) Rb = GetComponent<Rigidbody>();
        if (!Rb)
        {
            enabled = false;
            Debug.LogWarning($"basket {gameObject.name} is missing its rb");
            return;
        }

        if (!GaemPlane) GaemPlane = transform.parent;

        posCache = new(PosCacheSize);
    }

    private void FixedUpdate()
    {


        posCache.Enqueue(Rb.position);
        Vector3 dir = posCache.First - posCache.Last;
        if (dir.magnitude > DirThreshold)
        {
            rotTarget = Vector3.SignedAngle(GaemPlane.up, dir, GaemPlane.forward);
            rotBuffer = Mathf.MoveTowardsAngle(rotBuffer, rotTarget, RotSpeed * Time.deltaTime);

            Rb.rotation = Quaternion.AngleAxis(rotBuffer, GaemPlane.forward);
        }

        Plane plane = new Plane(
            GaemPlane.position + GaemPlane.up,
            GaemPlane.position + GaemPlane.right,
            GaemPlane.position
        );
        Ray ray = Cam.ScreenPointToRay(
                        // Pointer.current.position.ReadValue()
                        Mouse.current.position.ReadValue()
                    );

        if (plane.Raycast(
            ray, out float enter))
        {

            Vector3 basketWorldPos = ray.GetPoint(enter);

            Vector3 force = (basketWorldPos - Rb.position) * ForceMul;
            float magnitude = force.magnitude;

            if (magnitude < float.Epsilon)
                return;

            force = force.normalized * Mathf.Clamp(magnitude, MinForce, MaxForce);
            // Debug.Log($"Adding basket force {force.ToString("0.00")}");
            Rb.AddForceAtPosition(force, basketWorldPos, ForceMode.Force);
        }

    }


}
