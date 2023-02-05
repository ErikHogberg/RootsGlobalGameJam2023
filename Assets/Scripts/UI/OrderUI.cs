using UnityEngine;
using UnityEngine.Events;

public class OrderUI : MonoBehaviour
{
    public UrderUIHandler ParentHandler;
    public IngredientUI[] Ingredients;

    public float Timer = 60;
    float timer = 1;
    public UnityEvent<float> OnTick;

    [Space]
    public RectTransform Banner;
    public float BannerSpacing = 1f;
    public float BannerOffset = 1f;

    public StakeScript stakeRef;


    private void Update()
    {
        timer -= Time.deltaTime;
        OnTick.Invoke(timer/Timer);
        if (timer < 0)
        {
            stakeRef.Finish();
            enabled = false;
        }
    }

    public void Finish()
    {
        // ParentHandler.RemoveOrder(this);
        Destroy(this.gameObject);
    }

    public void SetOrder(StakeScript stake)
    {
        stakeRef = stake;
        gameObject.SetActive(true);
        timer = Timer;

        int enableCount = 0;
        for (int i = 0; i < Ingredients.Length; i++)
        {
            var ingredient = Ingredients[i];

            if (i >= stake.Order.Count)
            {
                ingredient.Hide();
                continue;
            }

            enableCount++;

            StakeScript.OrderEntry order = stake.Order[i];
            ingredient.Set(order.Type, order.OneCookedness);
        }

        var bannerSize = Banner.sizeDelta;
        bannerSize.y = BannerOffset + BannerSpacing * enableCount;
        Banner.sizeDelta = bannerSize;
    }
}
