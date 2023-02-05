using UnityEngine;

public class OrderUI : MonoBehaviour
{
    public IngredientUI[] Ingredients;

    [Space]
    public RectTransform Banner;
    public float BannerSpacing = 1f;
    public float BannerOffset = 1f;

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

        var bannerSize = Banner.sizeDelta;
        bannerSize.y = BannerOffset + BannerSpacing * Ingredients.Length;
        Banner.sizeDelta = bannerSize;
    }
}
