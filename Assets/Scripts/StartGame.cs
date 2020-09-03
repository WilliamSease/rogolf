using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame
{
    public static void Start()
    {
        // Earned items
        ItemBag itemBag = new ItemBag();

        // Visited holes
        HoleBag holeBag = new HoleBag();
        // Load hole from holeBag
        SceneManager.LoadScene(holeBag.GetHole());

        //Instantiate prefab
        // Add holeBag and itemBag to Game object
        // Go somewhere?
    }

}
