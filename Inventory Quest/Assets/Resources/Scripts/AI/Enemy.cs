using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public NPC character;

    public Stats priorities;

    public int intelligence;

    int focus;

    Hashtable maxstats;
    Hashtable penultimate;

    string[] slots;
    int currentInventorySlot;
    int currentEquipmentSlot;

	// Use this for initialization
	void Start () {
	    if(character == null)
        {
            character = GetComponent<NPC>();
        }
	}

    void Awake()
    {
        slots = new string[3] { "head", "chest", "feet" };
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public int MaximizeStat(string statName) //Uses the greedy algorithm
    {
        bool ok = false;
        int startEquipmentSlot = currentEquipmentSlot;
        int startInventorySlot = currentInventorySlot;
        while(!ok && focus > 0)
        {
            focus--;
            var c = slots[currentEquipmentSlot] + "/" + statName;
            var item = character.inventory.ItemAt(currentInventorySlot % character.inventory.width, currentInventorySlot / character.inventory.width);
            if (item.compatibleSlots.Contains(statName) && (!maxstats.Contains(c) || item.stats.LevelOf(statName) > ((Item)maxstats[c]).stats.LevelOf(statName)))
            {
                penultimate[c] = maxstats[c];
                maxstats[c] = item;
            } else if(item.compatibleSlots.Contains(statName) && (!penultimate.Contains(c) || item.stats.LevelOf(statName) > ((Item)penultimate[c]).stats.LevelOf(statName)))
            {
                penultimate[c] = item;
            }
            currentInventorySlot++;
            if (currentInventorySlot == character.inventory.width * character.inventory.height) {
                currentInventorySlot = 0;
            }
            if (currentInventorySlot == startInventorySlot) {
                character.inventory.RemoveItem((Item)maxstats[slots[currentEquipmentSlot] + "/" + statName]);
                FitItem(character.EquipItem((Item)maxstats[slots[currentEquipmentSlot] + "/" + statName], slots[currentEquipmentSlot]));
                currentEquipmentSlot++;
                if (currentEquipmentSlot == slots.Length)
                {
                    currentEquipmentSlot = 0;
                }
                if (currentEquipmentSlot == startEquipmentSlot)
                {
                    ok = true;
                }
            }
        }
        return character.skills.LevelOf(statName);
    }

    public bool FitItem(Item what)
    {
        bool ok = false;
        int startEquipmentSlot = currentEquipmentSlot;
        int startInventorySlot = currentInventorySlot;
        int optimalslot = startInventorySlot;
        int swapvalue = 0;
        int swapcost = -1;
        int tswapcost;
        foreach (DictionaryEntry de in what.compatibleSlots) //If useless, discard it, else calculate the value of having it
        {
            focus--;
            foreach (DictionaryEntry j in what.stats.contents)
            {
                focus--;
                if (!maxstats.Contains(de.Key + "/" + j.Key) || (maxstats[de.Key + "/" + j.Key] == null))
                {
                    swapvalue += ((Skill)j.Value).level * priorities.LevelOf(j.Key);
                } else if(((Item)maxstats[de.Key + "/" + j.Key]).stats.LevelOf(j.Key) < ((Skill)j.Value).level)
                {
                    swapvalue += (((Skill)j.Value).level - ((Item)maxstats[de.Key + "/" + j.Key]).stats.LevelOf(j.Key)) * priorities.LevelOf(j.Key);
                }
            }
        }
        if (swapvalue == 0) return false;
        var maxdel = new Hashtable();
        var penultimatedel = new Hashtable();
        bool sameslot = false;
        while (!ok && focus >= what.width * what.height)
        {
            focus--;
            tswapcost = 0;
            maxdel.Clear();
            penultimatedel.Clear();
            for (int yi = 0; yi < what.height; yi++)
            {
                focus--;
                for(int xi = 0; xi < what.width; xi++)
                {
                    focus--;
                    var slot = character.inventory.ItemAt(xi + currentInventorySlot % character.inventory.width, yi + currentInventorySlot / character.inventory.width);
                    if(slot != null)
                    {
                        foreach (DictionaryEntry de in slot.compatibleSlots)
                        {
                            focus--;
                            foreach(DictionaryEntry j in slot.stats.contents)
                            {
                                focus--;
                                var c = de.Key + "/" + j.Key;
                                if(slot == maxstats[c] && !maxdel.Contains(c))
                                {
                                    maxdel.Add(slot, null);
                                    if(what.compatibleSlots.Contains(de))
                                    {
                                        sameslot = true;
                                        if (what.stats.LevelOf(j.Key) < ((Skill)j.Value).level)
                                        {
                                            if (penultimatedel.Contains(c))
                                            {
                                                tswapcost += (((Skill)j.Value).level - what.stats.LevelOf(j.Key)) * priorities.LevelOf(j.Key);
                                            }
                                            else
                                            {
                                                tswapcost += (((Skill)j.Value).level - ((Item)penultimate[c]).stats.LevelOf(j.Key)) * priorities.LevelOf(j.Key);
                                            }
                                        }
                                    }
                                    else 
                                    {
                                        if (penultimatedel.Contains(c))
                                        {
                                            tswapcost += (((Skill)j.Value).level) * priorities.LevelOf(j.Key);
                                        }
                                        else
                                        {
                                            tswapcost += (((Skill)j.Value).level - ((Item)penultimate[c]).stats.LevelOf(j.Key)) * priorities.LevelOf(j.Key);
                                        }
                                    }
                                }
                                else
                                {
                                    if (slot == penultimate[c] && !penultimatedel.Contains(c))
                                    {
                                        penultimatedel.Add(c, null);
                                        if (maxdel.Contains(c))
                                        {
                                            if (what.compatibleSlots.Contains(de))
                                            {
                                                tswapcost += (((Item)penultimate[c]).stats.LevelOf(j.Key)) * priorities.LevelOf(j.Key);
                                            }
                                            else
                                            {
                                                tswapcost += (((Item)penultimate[c]).stats.LevelOf(j.Key) - what.stats.LevelOf(j.Key)) * priorities.LevelOf(j.Key);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            currentInventorySlot++;
            if (currentInventorySlot == character.inventory.width * character.inventory.height)
            {
                currentInventorySlot = 0;
            }
            if (currentInventorySlot == startInventorySlot)
            {
                ok = true;
            }
        }
        return false;
        //TODO: Code this
    }

    public void Refresh()
    {
        focus = intelligence;
    }
}
