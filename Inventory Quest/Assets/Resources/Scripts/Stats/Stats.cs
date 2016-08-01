using UnityEngine;
using System.Collections;

public class Stats{

    public Hashtable contents; //This is supposed to be a Hashtable that allows vector maths

    public Stats()
    {
        contents = new Hashtable();
    }

    public void Add(Stats rhs)
    {
        if(rhs != null && rhs.contents != null)
            foreach(DictionaryEntry de in rhs.contents)
            {
                if (contents.Contains(de.Key))
                {
                    ((Skill)contents[de.Key]).level += ((Skill)de.Value).level;
                }
                else
                {
                    contents[de.Key] = new Skill() { level = ((Skill)de.Value).level, baselevel = 0 };
                }
            }
    }

    public void Add(object key,Skill value)
    {
        contents.Add(key, value);
    }

    public void Add(object key, int level)
    {
        if (contents.Contains(key))
        {
            ((Skill)contents[key]).level += level;
        }
        else
        {
            var value = new Skill(level);
            contents.Add(key, value);
        }
    }

    public void Subtract(Stats rhs)
    {
        if (rhs != null && rhs.contents != null)
            foreach (DictionaryEntry de in rhs.contents)
            {
                if (contents.Contains(de.Key))
                {
                    ((Skill)contents[de.Key]).level -= ((Skill)de.Value).level;
                }
                else
                {
                    contents[de.Key] = new Skill() { level = -((Skill)de.Value).level, baselevel = 0 };
                }
            }
    }

    public int LevelOf(object rhs)
    {
        if (contents.Contains(rhs))
        {
            return ((Skill)contents[rhs]).level;
        }
        return 0;
    }

    public int Dot(Stats rhs)
    {
        int ret = 0;
        foreach(DictionaryEntry de in contents)
        {
            if (rhs.contents.Contains(de.Key)) ret += ((Skill)de.Value).level * rhs.LevelOf(de.Key);
        }
        return ret;
    }

}
