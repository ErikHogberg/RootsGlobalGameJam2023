using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerButton : MonoBehaviour
{

    public float NextPressDelay = 1f;
    public bool UpdateTimer = false;
    
    [Space]
    public UnityEvent OnPress;

    static float sharedTimer = -1;

    private void Update() {
        if(!UpdateTimer || sharedTimer < 0) return;

        sharedTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        if(sharedTimer > 0) return;

        OnPress.Invoke();
        Debug.Log($"Press {gameObject.name}");
        sharedTimer = NextPressDelay;
    }
}
