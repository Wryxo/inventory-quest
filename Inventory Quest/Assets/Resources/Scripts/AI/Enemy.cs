using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public NPC character;

    public int intelligence;

	// Use this for initialization
	void Start () {
	    if(character == null)
        {
            character = GetComponent<NPC>();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    int MaximizeStat(string statName) //Uses the greedy algorithm
    {
        foreach(DictionaryEntry de in character.gear.validSlots)
        {
            //TODO: Code this
        }
        return character.skills.LevelOf(statName);
    }
}
