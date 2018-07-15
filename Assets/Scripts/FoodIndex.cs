using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodIndex : MonoBehaviour {

    public FoodTrigger[] foods;
    

    public void GenerateRandomFoodAtLocation(Vector3 pos, float mod)
    {
        float rand = Mathf.Pow(Random.value, 1 / mod);
        if (rand > 0.5)
        {
            FoodTrigger toCreate = foods[0];
            if (rand > 0.75)
            {
                if (rand < .9)
                {
                    toCreate = foods[1];
                }
                else if (rand < .98)
                    toCreate = foods[2];
                else
                    toCreate = foods[3];
            }
            Instantiate(toCreate, pos, Quaternion.Euler(0, 0, 0));
        }
    }
}
