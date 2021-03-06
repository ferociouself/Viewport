﻿using UnityEngine;
using System.Collections.Generic;

public class ApplyMenuAttributes : MonoBehaviour {

	// set number of players and player skins as determined by menu input
	void Start () {
        // Access player preferences values set by menu
        int toPlay1 = PlayerPrefs.GetInt("p1join");
        int toPlay2 = PlayerPrefs.GetInt("p2join");
        int toPlay3 = PlayerPrefs.GetInt("p3join");
        int toPlay4 = PlayerPrefs.GetInt("p4join");

        // Get players 
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        /*
		int length = players.Length;
        if (length < 2) {
						return;
		}
        */

        // Make players that were not selected inactive
        for (int i = 0; i < players.Length; i++)
        {
            PlayerControls controls = players[i].GetComponent<PlayerControls>();
            int playerNum = controls.playerNum;

            if (toPlay1 == 0 && toPlay2 == 0 && toPlay3 == 0 && toPlay4 == 0)
            {
                toPlay1 = 1;
                toPlay2 = 1;
                toPlay3 = 1;
                toPlay4 = 1;
            }

            if (playerNum == 1 && toPlay1 == 0)
                players[i].SetActive(false);
            if (playerNum == 2 && toPlay2 == 0)
                players[i].SetActive(false);
            if (playerNum == 3 && toPlay3 == 0)
                players[i].SetActive(false);
            if (playerNum == 4 && toPlay4 == 0)
                players[i].SetActive(false);
        }
    }
}
