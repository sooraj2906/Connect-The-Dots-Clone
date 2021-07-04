using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    //This method is used to Load a level from the Level selector screen
    public void LevelLoad(int level)
    {
        LevelManager.pInstance.levelNumber = level;
        SceneManager.LoadScene("GameScene");
    }
}
