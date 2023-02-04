using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnerCollider : MonoBehaviour
{
    public string TagToDespawn = "Beet";

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(TagToDespawn))
        {
            Destroy(other.gameObject);
        }
    }

}
