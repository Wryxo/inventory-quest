﻿using UnityEngine;
using System.Collections;

public class CategoricDistribution {

    ArrayList categories;
    Hashtable helpIndex;

    int sorted = 0;

    float area; //the area under the curve

    public CategoricDistribution()
    {
        categories = new ArrayList();
        helpIndex = new Hashtable();
    }

    public object Random()
    {
        if (categories.Count == 0) throw new System.Exception("Error: Can't select a random category out of 0 options");
        float v = UnityEngine.Random.value;
        if (sorted < categories.Count) Normalize();
        v *= area;
        bool ok = false;
        int i = 1;
        while (!ok)
        {
            if (i < categories.Count) i <<= 1;
            else ok = true;
        }
        i >>= 1;
        ok = false;
        int j = 0;
        while (i > 0)
        {
            if((i|j) < categories.Count && v > ((RandomItem)categories[i | j]).quad)
            {
                j |= i;
            }
            i >>= 1;
        }
        return ((RandomItem)categories[j]).value;
    }

    public void AddCategory(object what, float weight = 1)
    {
        if (helpIndex.Contains(what))
        {
            int i = (int)helpIndex[what];
            RandomItem f = (RandomItem)categories[i];
            area += weight;
            f.weight += weight;
            if (i < sorted) sorted = i+1;
            
        } else
        {
            RandomItem f = new RandomItem() { value = what, weight = weight, quad = area };
            area += weight;
            if (categories.Count == sorted) sorted += 1;
            categories.Add(f);
            helpIndex.Add(what, categories.Count-1);
        }

    }

    public RandomItem find(object key)
    {
        return (RandomItem)helpIndex[key];
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
        float surface = 0;
        for(int i = 1; i < categories.Count; i++)
        {
            RandomItem f = (RandomItem)categories[i];
            f.quad = surface;
            surface = f.quad + f.weight;
        }
        for (int i = 1; i < categories.Count; i++)
        {
            RandomItem f = (RandomItem)categories[i];
            f.quad /= surface;
            f.weight /= surface;
        }
        sorted = categories.Count;
        area = surface;
    }

    public override string ToString()
    {
        var ret = "";
        for(int i = 0; i < categories.Count; i++)
        {
            ret += categories[i] + "\n";
        }
        return ret;
    }
}
