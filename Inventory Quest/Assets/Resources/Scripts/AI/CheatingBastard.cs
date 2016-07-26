using UnityEngine;
using System.Collections;

public class CheatingBastard : MonoBehaviour {

    public NPC character;
    Equipment gear;
    Stats stats;

	// Use this for initialization
	void Start () {

	}

    public void Awake()
    {
        gear = character.gear;
        stats = character.skills;
        var fakeItem = Item.Vesta(0);
        gear.EquipItem(fakeItem);
        fakeItem = Item.Ciapka(0);
        gear.EquipItem(fakeItem);
        fakeItem = Item.Topanka(0);
        gear.EquipItem(fakeItem);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void EquipItem(Item what)
    {
        int dmax;
        object argmax;
        foreach (DictionaryEntry de in what.stats.contents)
        {
            dmax = 0;
            argmax = null;
            foreach(DictionaryEntry j in what.compatibleSlots)
            {
                if (dmax < ((Skill)de.Value).level - ((Item)gear.ItemAt(j.Key)).stats.LevelOf(de.Key))
                {
                    dmax = ((Skill)de.Value).level - ((Item)gear.ItemAt(j.Key)).stats.LevelOf(de.Key);
                    argmax = j.Key;
                }
            }
            if(argmax != null)
            {
                ((Item)gear.ItemAt(argmax)).stats.Add(de.Key, dmax);
                character.skills.Add(de.Key, dmax);
            }
        }
    }


}
