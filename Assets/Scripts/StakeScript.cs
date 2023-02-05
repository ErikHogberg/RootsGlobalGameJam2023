 using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class StakeScript : MonoBehaviour
{
    [System.Serializable]
    public class OrderEntry
    {
        public string Type;
        public List<string> Cookednesses;
        public string OneCookedness => Cookednesses == null || Cookednesses.Count < 1 ? string.Empty : Cookednesses[0];
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
    public UnityEvent<Beet> OnVeggie;

    public UrderUIHandler OrderHandler;
    public OrderUI OrderRef;

    private void Start()
    {
        if (!VeggiePlacer) VeggiePlacer = transform;
        timer = OffTime;
        Veggies = new(Order.Count);

        // OrderHandler.AddOrder(this);

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
            AudioManager.Sfx("stake");
            beet.Stake();
            Veggies.Add(beet);
            OnVeggie.Invoke(beet);
            Debug.Log("stake got veggie");
            RepositionVeggies();
            if (Veggies.Count >= Order.Count)
            {
                Finish();
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

    public void Finish(){
        // TODO: award score
        int scoreAcc = 0;

        foreach (var item in Order)
        {
            if(Veggies.Count < 1) break;

            int best = 0;
            int bestScore = 0;
            for (int i = 0; i < Veggies.Count; i++)
            {
                int score = 0;
                if(Veggies[i].Type == item.Type){
                    score++;
                }
                if(Veggies[i].OneCookedness == item.OneCookedness){
                    score++;
                }
                if(score > bestScore){
                    bestScore = score;
                    best = i;
                }
            }
            Destroy(Veggies[best].gameObject);
            Veggies.RemoveAt(best);
            scoreAcc += bestScore;
        }

        ScoreManager.Score += scoreAcc;

        foreach (var item in Veggies)
        {
            Destroy(item.gameObject);
        }
        Veggies.Clear();
        OrderRef.Finish();
        Destroy(gameObject);
    }
}
