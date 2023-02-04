using System.Collections;
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

        // TODO: project mouse pos on plane, move basket rb there

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

            Vector3 basketPos = ray.GetPoint(enter);
            // Plane.TransformPoint(
            // Vector3.ProjectOnPlane(
            // Plane.InverseTransformPoint(

            // ),
            // Vector3.forward
            // )
            // )
            ;
            Rb.MovePosition(basketPos);
            Debug.Log($"New basket pos {basketPos.ToString("0.00")}");
        }

        // transform.position = basketPos;
        // Vector3 force = (Rb.position - basketPos) * ForceMul;
        // Debug.Log($"Adding basket force {force.ToString("0.00")}");
        // Rb.AddForce(force, ForceMode.Force);
    }
}
