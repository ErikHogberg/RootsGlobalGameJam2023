using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StakeScript : MonoBehaviour
{
    [System.Serializable]
    public class OrderEntry
    {
        public string Type;
        public List<string> Cookednesses;
    }

    public float VeggieSpacing = .1f;
    public List<OrderEntry> Order;
    private List<Beet> Veggies;
    public Transform VeggiePlacer;

    [Space]
    public float OffTime = 1f;
    float timer = 1;

    [Space]
    public UnityEvent<StakeScript> OnOn;

    private void Start()
    {
        if (!VeggiePlacer) VeggiePlacer = transform;
        timer = OffTime;
        Veggies = new(Order.Count);
    }

    private void Update()
    {
        if (OffTime >= 0)
        {
            OffTime -= Time.deltaTime;
            if (OffTime < 0)
            {
                Debug.Log("stake on");
                OnOn.Invoke(this);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (OffTime > 0 || Veggies.Count >= Order.Count)
        {
            return;
        }

        if (other.gameObject.TryGetComponent<Beet>(out var beet))
        {
            beet.Stake();
            Veggies.Add(beet);
            Debug.Log("stake got veggie");
            RepositionVeggies();
            if(Veggies.Count >= Order.Count){
                // TODO: finish order
            }
        }
    }

    void RepositionVeggies()
    {
        for (int i = 0; i < Veggies.Count; i++)
        {
            Veggies[i].transform.position = VeggiePlacer.position + VeggiePlacer.forward * VeggieSpacing * i;
        }
    }
}
