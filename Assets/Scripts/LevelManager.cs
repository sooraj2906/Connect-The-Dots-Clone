using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is used to carry the level number to the game scene so that the level generator can use this to generate the scene from the json file
/// </summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager pInstance;

    public int levelNumber;

    private void Start()
    {
        DontDestroyOnLoad(this);
        if (pInstance == null)
        {
            pInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
