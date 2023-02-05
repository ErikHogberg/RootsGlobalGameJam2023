using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookScript : MonoBehaviour
{
    public string Cookedness = "deepfry";

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<Beet>(out var beet))
        {
            beet.Cook(Cookedness);
        }

    }
}
