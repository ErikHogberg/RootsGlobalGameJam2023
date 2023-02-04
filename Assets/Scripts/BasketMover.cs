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

    float rotBuffer;
    float rotTarget;

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
    }

    private void FixedUpdate()
    {
        // if(!Pointer.current.IsPressed()) return;

        // TODO: set target rotation using cached previous positions
        // TODO: rotate towards target rotation
        // Rb.rotation =

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
