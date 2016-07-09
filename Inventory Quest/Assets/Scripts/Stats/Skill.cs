using UnityEngine;
using System.Collections;

public class Skill {

    public int level;
    public int baselevel;

    int rc;

    public Skill()
    {
        rc = 1;
    }

    public Skill(int level)
    {
        this.level = level;
        baselevel = level;
    }

    public override string ToString()
    {
        return (string.Format("{0} ({1} + {2})", level, baselevel, level-baselevel));
    }

}
