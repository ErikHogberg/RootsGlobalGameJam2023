using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderUI : MonoBehaviour
{
    public IngredientUI[] Ingredients;

    public void SetOrder(StakeScript stake)
    {
        for (int i = 0; i < Ingredients.Length; i++)
        {
            var ingredient = Ingredients[i];

            if (i >= stake.Order.Count)
            {
                ingredient.Hide();
                continue;
            }

            StakeScript.OrderEntry order = stake.Order[i];
            ingredient.Set(order.Type, order.OneCookedness);
        }
    }
}
