/**
 * Department: Game Developer
 * File: ListController.cs
 * Objective: Create a new List component to add in the laderboard
 * Employee: Ramón Martínez Nieto
 */
using System.Collections.Generic;
using UnityEngine;

/**
 * Class to create a new component to put in the lader board
 * 
 * @author Ramón Martínez Nieto
 * @see ListItemController
 */
public class ListController : MonoBehaviour
{
    /**
     * Script FireBaseConnection whit all functionality of the comunication with the database.
     * @see FirebaseConnection
     */
    [Tooltip("Put FireBaseConnection script")]
    public FirebaseConnection firebaseConn;
    
    /**
     * Prefab with the ListItemUser 
     */
    [Tooltip("Put a prefab with the ListItemUser")]
    public GameObject PreffabListItemUser;
    
    /**
     * ContentPanel where adding all ListItemUser
     */
    [Tooltip("Put contentpanel laderboard")]
    public GameObject ContentPanel;

    List<UserScore> listUsers = new List<UserScore>();
    List<GameObject> listItemController = new List<GameObject>();

    private void Update()
    {
        if (FirebaseConnection.finish)
        {
            listUsers = firebaseConn.GetListUsers();
            ChargeHighScores();
            FirebaseConnection.finish = false;
        }
    }

    /**
     * Method to search and add higher scores. 
     */
    public void ChargeHighScores() 
    {
        for (int i = 0; i < listUsers.Count; i++)
        {
            UserScore u = listUsers[i];
            GameObject newUser = Instantiate(PreffabListItemUser, ContentPanel.transform) as GameObject;
            ListItemController lit = newUser.GetComponent<ListItemController>();
            lit.name.text = u.name;
            lit.score.text = u.score.ToString();
            lit.time.text = u.time;

            listItemController.Add(newUser);
        }
    }

    /**
     * Method for cleaning the laderboard
     */
    public void clearList() 
    {
        foreach (GameObject go in listItemController) 
        {
            Destroy(go);
        }
    }
}
