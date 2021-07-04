using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using SimpleJSON;

/// <summary>
/// This class is used to read the json data and then generate the level based on the data from the json
/// </summary>
public class LevelController : MonoBehaviour
{
    public List<GameObject> grids;
    public GameObject circle;
    private List<int> positions = new List<int>();
    private List<string> colors = new List<string>();

    private void Start()
    {
        CreateLevel(LevelManager.pInstance.levelNumber - 1);
    }

    public void CreateLevel(int level)
    {
        var textFile = Resources.Load<TextAsset>("leveldata");  //Read the json file from Resources folder
        JSONNode jsondata = JSON.Parse(textFile.text);          //Conver the read json file into an json object so that we can traverse through the data

        //Gather the required data from the json into local variables so that they can be accessed easier
        for (int i = 0; i < 10; i++)
        {
            positions.Add(int.Parse(jsondata["leveldata"][level]["grid"][i]["row"].Value + jsondata["leveldata"][level]["grid"][i]["column"].Value));
            colors.Add(jsondata["leveldata"][level]["grid"][i]["color"]);
        }
        //Instantiate objects in the grid
        foreach(GameObject g in grids)
        {
            GameObject go = Instantiate(circle, g.transform.position, Quaternion.identity, g.transform);
            go.name = g.name;
            go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, -0.01f);
            for(int i = 0; i < positions.Count; i++)
            {
                //If the position of dots from the json matches the grid position, mark them as dots and add the respective color received from the json
                if(g.name == positions[i].ToString())
                {
                    go.GetComponent<DotController>().isEnd = true;
                    go.GetComponent<SpriteRenderer>().enabled = true;
                    if (ColorUtility.TryParseHtmlString(colors[i], out Color color))
                    {
                        go.GetComponent<DotController>().col = color;
                        go.GetComponent<SpriteRenderer>().color = color;
                    }
                }
            }
        }
    }
}

