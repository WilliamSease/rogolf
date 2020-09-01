using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Item> items;
    public PlayerAttributes playerAttributes;
    public PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        items = new List<Item>();
        playerAttributes = new PlayerAttributes();
        playerStats = new PlayerStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
