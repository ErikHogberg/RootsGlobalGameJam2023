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
    }

}
