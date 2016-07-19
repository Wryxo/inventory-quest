using UnityEngine;
using System.Collections;

public class StatMatrix {

    Hashtable members;
    float diag;

    float GetFieldAt(int x,int y)
    {
        if (x == y) return diag;
        return 0;
    }
    
}
