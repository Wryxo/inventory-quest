using UnityEngine;
using System.Collections;

public class CategoricDistribution {

    ArrayList categories;
    Hashtable helpIndex;

    int sorted = 0;

    float area; //the area under the curve

    public object Random()
    {
        float v = UnityEngine.Random.value;
        if (sorted < categories.Count) Normalize();
        //TODO: Implement binary search
        return null;
    }

    public void AddCategory(object what, float weight = 1)
    {
        if (helpIndex.Contains(what))
        {
            int i = (int)helpIndex[what];
            RandomItem f = (RandomItem)categories[i];
            area += weight;
            f.weight += weight;
            f.quad += weight;
            if (i < sorted) sorted = i;
            
        } else
        {
            RandomItem f = new RandomItem() { value = what, weight = weight, quad = area + weight };
            area += weight;
            categories.Add(f);
            helpIndex.Add(what, categories.Count-1);
        }

    }

    public void Add(CategoricDistribution rhs, float weight = 1)
    {

    }

    public void Multiply(CategoricDistribution rhs)
    {

    }

    void SetWeight(int index, float weight)
    {

    }

    public void SetWeight(object what,float weight)
    {

    }

    public void Normalize()
    {

    }
}
