using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using SimpleJSON;

/// <summary>
/// This class contains the game win logic
/// </summary>
public class GameController : MonoBehaviour
{
    public LineController line;
    public LevelController level;
    public GameObject winPanel, losePanel;
    public bool win;
    private void Start()
    {

    }

    private void Update()
    {
        //Check is all 5 lines are drawn
        if(GameManager.pInstance.line.GetLinesCount() >= 5)
        {
            //Check if all the grids are covered
            foreach(GameObject g in level.grids)
            {
                if(g.GetComponentInChildren<DotController>().isActivated)
                {
                    win = true;
                }
                else
                {
                    win = false;
                    break;
                }
            }
        }
        //If all 5 lines are drawn and all the grids are covered, display the win screen
        if(win)
        {
            if(!winPanel.activeSelf)
            {
                winPanel.SetActive(true);
            }
        }
    }

    //Method to display the game over screen
    public void GameOver()
    {
        if(!losePanel.activeSelf)
        {
            losePanel.SetActive(true);
        }
    }
}
