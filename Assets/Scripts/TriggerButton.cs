using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerButton : MonoBehaviour
{

    public float NextPressDelay = 1f;

    public UnityEvent OnPress;

    float timer = -1;

    private void Update() {
        if(timer < 0) return;

        timer -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        if(timer > 0) return;

        OnPress.Invoke();
        Debug.Log($"Press {gameObject.name}");
        timer = NextPressDelay;
    }
}
