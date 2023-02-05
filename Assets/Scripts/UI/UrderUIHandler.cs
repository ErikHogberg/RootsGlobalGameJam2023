using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrderUIHandler : MonoBehaviour
{

    private static UrderUIHandler mainInstance = null;
    void Awake()
    {
        mainInstance = this;
    }
    void OnDestroy()
    {
        mainInstance = null;
    }


    public OrderUI OrderPrefab;

    public Transform OrderParent;

    public static void AddOrder(StakeScript stake)
    {
        if(mainInstance) mainInstance.AddOrderInternal(stake);
    }
    private void AddOrderInternal(StakeScript stake)
    {
        var order = Instantiate(OrderPrefab, OrderParent);
        order.SetOrder(stake);
        stake.OrderRef = order;
    }


}
