using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGauge : MonoBehaviour{

	public int player;
	public int maxFood = 10;
	public RectTransform guage;

	private int currentFood = 0;

	void OnEnable()
	{
		Events.AddListener<FoodEaten>(OnFoodEaten);
	}

	void OnDisable()
	{
		Events.RemoveListener<FoodEaten>(OnFoodEaten);
	}

	private void OnFoodEaten(FoodEaten _event)
	{
		if (player == _event.player)
		{
			++currentFood;
			guage.localScale = new Vector2((float)currentFood / (float)maxFood,1);
			// TODO: DO something if you win!
		}
	}
}
