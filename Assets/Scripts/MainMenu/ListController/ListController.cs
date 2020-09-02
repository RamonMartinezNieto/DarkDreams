using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListController : MonoBehaviour
{
    public FirebaseConnection firebaseConn; 
    public GameObject PreffabListItemUser;
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

    public void clearList() 
    {
        foreach (GameObject go in listItemController) 
        {
            Destroy(go);
        }
    }
}
