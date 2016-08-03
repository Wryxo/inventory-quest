using UnityEngine;
using System.Collections;

public class MarkovChain{

    public Hashtable states;

    public object currentstate;

    public CategoricDistribution pagerank; //Doesn't actually compute anything, maybe fix later

    public object Random()
    {
        if (currentstate != null && states.Contains(currentstate))
        {
            currentstate = ((CategoricDistribution)states[currentstate]).Random();
        }
        else
        {
            currentstate = pagerank.Random();
        }
        return currentstate;
    }

    public static MarkovChain Clique(ICollection vertices)
    {
        var ret = new MarkovChain();
        foreach(var i in vertices)
        {
            var col = new CategoricDistribution();
            foreach(var j in vertices)
            {
                if (i != j) col.AddCategory(j);
            }
            ret.pagerank.AddCategory(i);
            ret.states.Add(i, col);
        }
        return ret;
    }

    public MarkovChain()
    {
        pagerank = new CategoricDistribution();
        states = new Hashtable();
    }
}
