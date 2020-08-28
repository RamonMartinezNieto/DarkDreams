using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListController : MonoBehaviour
{

    public GameObject PreffabListItemUser;
    public GameObject ContentPanel;

    List<UserScore> listUsers = new List<UserScore>();

    private void Start()
    {
        listUsers.Add(new UserScore("gggggggggggggggg", 80000000));
        listUsers.Add(new UserScore("ramon", 555));
        listUsers.Add(new UserScore("ramon", 555));
        listUsers.Add(new UserScore("ramon", 555));
        listUsers.Add(new UserScore("gggggggggggggggg", 80000000));
        listUsers.Add(new UserScore("ramon", 555));
        listUsers.Add(new UserScore("ramon", 555));
        listUsers.Add(new UserScore("ramon", 555));
        listUsers.Add(new UserScore("ram", 555));
        listUsers.Add(new UserScore("gggggggggggggggg", 80000000));


        showHighScores();
    }


    private void showHighScores() 
    {
        foreach (UserScore u in listUsers)
        {
            Debug.Log(u.name);
            GameObject newUser = Instantiate(PreffabListItemUser, ContentPanel.transform) as GameObject;
            ListItemController lit = newUser.GetComponent<ListItemController>();
            lit.name.text = u.name;
            lit.score.text = u.score.ToString();
        }
    }
}
