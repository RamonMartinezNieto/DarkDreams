using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FirebaseConnection : MonoBehaviour
{
    //Use this bool to know when the list is already
    public static bool finish;

    private DatabaseReference dataReference;
    private FirebaseDatabase instance;
   
    private List<UserScore> usersList = new List<UserScore>();

    public List<UserScore> GetListUsers()
    {
        //New sorted list 
        List<UserScore> sortedList = usersList.OrderByDescending(o => o.score).ToList<UserScore>();
        return sortedList;
    }

    public void WriteNewScore(string user, int score) => StartCoroutine(writeNewScore(user,score));

    public void UpdateListLaderBoardBackground() => StartCoroutine(GetFirstTenUsers());

    void Start()
    {
        //Config Firabase Instance
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://pruebasapirest-13c28.firebaseio.com/");
        //Catch reference
        dataReference = FirebaseDatabase.DefaultInstance.RootReference;

        StartCoroutine(GetFirstTenUsers());
    }

    private IEnumerator GetFirstTenUsers()
    {
        //Instance to get users
        instance = FirebaseDatabase.DefaultInstance;

        //Get References to add users
        dataReference.Child("users");

        //Query 10 people whit the better score
        instance.GetReference("users").OrderByChild("score").LimitToLast(10).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted) Debug.LogError("Error to get users");
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot user in snapshot.Children)
                {
                    usersList.Add(new UserScore(user.Child("name").Value.ToString(), Convert.ToInt32(user.Child("score").Value)));
                }
                finish = true;
            }
        });
        yield return null;
    }

    private IEnumerator writeNewScore(string name, int score)
    {
        string key = dataReference.Child("users").Push().Key;
        UserScore user = new UserScore(name, score);
        Dictionary<string, object> entryValues = user.ToDictionary();

        Dictionary<string, object> childUpdates = new Dictionary<string, object>();
        childUpdates["/users/" + key] = entryValues;

        dataReference.UpdateChildrenAsync(childUpdates);

        yield return null;
    }
}