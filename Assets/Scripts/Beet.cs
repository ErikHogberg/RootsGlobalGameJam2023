using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Beet : MonoBehaviour
{
    // public Rigidbody Rb;
    public UnityEvent OnStake;

    public string Type;
    public List<string> Cookednesses;
    public string OneCookedness => Cookednesses == null || Cookednesses.Count < 1 ? string.Empty : Cookednesses[0];

    public void Stake()
    {
        OnStake.Invoke();
    }

}
