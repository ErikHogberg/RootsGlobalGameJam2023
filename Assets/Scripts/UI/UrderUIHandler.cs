using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrderUIHandler : MonoBehaviour
{
    public OrderUI OrderPrefab;

    public Transform OrderParent;

    public void AddOrder(StakeScript stake)
    {
        var order = Instantiate(OrderPrefab, OrderParent);
        order.SetOrder(stake);
        stake.OrderRef = order;
    }

    public void RemoveOrder(OrderUI order){
        
    }

    public void RemoveOrder(StakeScript stake){
        
    }

}
