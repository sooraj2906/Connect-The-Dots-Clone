using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class manages the line renderers
/// </summary>
public class LineController : MonoBehaviour
{
    private bool isDrag = false;
    private int i = 0;
    private GameObject prevGrid;
    private LineRenderer lr;
    private List<GameObject> currentLine = new List<GameObject>();
    public List<LineRenderer> lines = new List<LineRenderer>();


    private void Update()
    {
        //Draw a ray from camera to screen on position of the mosue to get the object that we are clikcing
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit2D = Physics2D.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction, 100);

        if (Input.GetMouseButtonDown(0))
        {
            isDrag = true;
        }

        if (isDrag)
        {
            //Checks if the mouse is over a grid when clicking
            if (hit2D.collider)
            {
                DotController dot = hit2D.collider.GetComponent<DotController>();
                //Checks if no line is currently being drawn, and if the grid is not an empty grid
                if (lr == null && hit2D.collider.GetComponent<DotController>().col != Color.white)
                {
                    if (prevGrid == null)
                        prevGrid = hit2D.collider.gameObject;
                    //The below section of code instantiates a new gameobject and add a new line renderer component to it. And then the 
                    //properties of the line renderer are defined
                    GameObject go = new GameObject();
                    go.AddComponent<LineRenderer>();
                    go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, 0.01f);
                    lr = go.GetComponent<LineRenderer>();
                    lr.startWidth = 0.4f;
                    lr.endWidth = 0.4f;
                    lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
                    lr.useWorldSpace = true;
                    lr.sortingLayerName = "Foreground";
                    lr.sortingOrder = 1;
                    lr.positionCount = i + 1;
                    lr.startColor = dot.col;
                    lr.endColor = dot.col;
                }
                else if (!dot.isActivated)
                //Checks if the grid is not activated
                {
                    //Checks if the grid color is the same color as the line renderer or white, so that we can draw over it
                    if (dot.col == lr.startColor || dot.col == Color.white)
                    {
                        //Checks whether a new line has to be drawn or a point is to be added to a previous line
                        if (prevGrid != null)
                        {
                            //Checks if the grid that we are on is either horizontal or vertical to the previous grid and not diagonal
                            if (hit2D.collider.transform.position.x == prevGrid.transform.position.x || hit2D.collider.transform.position.y == prevGrid.transform.position.y)
                            {
                                if (lr.GetPosition(lr.positionCount - 1) == hit2D.collider.transform.position)
                                {
                                    lr.positionCount -= 1;
                                }
                                else
                                {
                                    //Adds the current grid position to the line renderer and set the current grid as previous grid
                                    dot.isActivated = true;
                                    lr.positionCount = i + 1;
                                    lr.SetPosition(i, hit2D.collider.transform.position);
                                    hit2D.collider.enabled = false;
                                    currentLine.Add(hit2D.collider.gameObject);
                                    i++;
                                    prevGrid = hit2D.collider.gameObject;
                                }
                            }
                        }
                        else
                        {
                            if (lr.GetPosition(lr.positionCount) == hit2D.collider.transform.position)
                            {
                                lr.positionCount -= 1;
                            }
                            else
                            {
                                //Adds the current grid position to the line renderer and set the current grid as previous grid
                                dot.isActivated = true;
                                lr.SetPosition(i, hit2D.collider.transform.position);
                                hit2D.collider.enabled = false;
                                currentLine.Add(hit2D.collider.gameObject);
                                i++;
                                prevGrid = hit2D.collider.gameObject;
                            }
                        }
                    }
                }
                else
                {
                    //Check if the current grid is not the previous grid and if the current grid is not the ending grid
                    if (hit2D.collider.gameObject != prevGrid && !hit2D.collider.GetComponent<DotController>().isEnd)
                        GameManager.pInstance.game.GameOver();

                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            ResetLines();
        }
    }

    //This method checks whether the gird is a end grid. If so finishes the line or deletes the current drawn line
    void ResetLines()
    {
        if (prevGrid != null)
        {
            if (!prevGrid.GetComponent<DotController>().isEnd)// || prevGrid == hit2D.collider.gameObject)
            {
                Destroy(lr.gameObject);
                foreach (GameObject g in currentLine)
                {
                    g.GetComponent<CircleCollider2D>().enabled = true;
                    g.GetComponent<DotController>().isActivated = false;
                }
            }
            else
            {
                lines.Add(lr);
            }

            isDrag = false;
            lr = null;
            prevGrid = null;
            i = 0;
        }
    }

    //Resets the current level so that the user can start over
    public void ResetLevel()
    {
        foreach (LineRenderer g in lines.ToArray())
        {
            Destroy(g);
            lines.Remove(g);
        }
        currentLine.Clear();
        foreach (GameObject g in GameManager.pInstance.level.grids)
        {
            g.GetComponentInChildren<DotController>().isActivated = false;
            g.GetComponentInChildren<CircleCollider2D>().enabled = true;
        }
        isDrag = false;
        lr = null;
        prevGrid = null;
        i = 0;
    }

    //Used to get the count of drawn lines
    public int GetLinesCount()
    {
        return lines.Count;
    }

}
