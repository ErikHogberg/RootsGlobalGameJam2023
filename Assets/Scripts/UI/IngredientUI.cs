using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientUI : MonoBehaviour
{
    [System.Serializable]
    public class VeggieEntry
    {
        public string Key;
        public GameObject Value;
    }

    public VeggieEntry[] Veggies;
    public VeggieEntry[] Reqs;

    public void Set(string veggie, string req)
    {
        gameObject.SetActive(true);
        foreach (var item in Veggies)
        {
            item.Value.SetActive(item.Key == veggie);
        }
        foreach (var item in Reqs)
        {
            item.Value.SetActive(item.Key == req);
        }
    }

    public void Hide(){
        gameObject.SetActive(false);
    }
}
