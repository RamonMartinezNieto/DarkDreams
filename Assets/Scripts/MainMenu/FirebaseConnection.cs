using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class FirebaseConnection : MonoBehaviour
{
    public static FirebaseConnection Instance = null;

    //Use this bool to know when the list is already
    public static bool finish;

    private DatabaseReference dataReference;
    private FirebaseDatabase instance;
   
    private List<UserScore> usersList = new List<UserScore>();

    
    void Awake()
    {
        //Singleton instance
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public List<UserScore> GetListUsers()
    {
        //New sorted list 
        List<UserScore> sortedList = usersList.OrderByDescending(o => o.score).Take(10).ToList<UserScore>();
        return sortedList;
    }

    public void WriteNewScore(string user, int score) => PostUser(new UserScore(user,score));

    public void UpdateListLaderBoardBackground() => StartCoroutine(GetFirstTenUsersDesktop()); //StartCoroutine(GetFirstTenUsersMobile());

    void Start()
    {
        //Config Firabase Instance
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://pruebasapirest-13c28.firebaseio.com/");
        //Catch reference
        dataReference = FirebaseDatabase.DefaultInstance.RootReference;

//        StartCoroutine(GetFirstTenUsers());
    }


    //Only use this when export to Mobile 
    // in other way use GetFirstTenUsersDesktop(); 
    private IEnumerator GetFirstTenUsersMobile()
    {
        usersList.Clear(); 

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

    private IEnumerator GetFirstTenUsersDesktop() 
    {
        string uri = $"https://pruebasapirest-13c28.firebaseio.com/users.json?orderBy=\"score\"";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                //Split all string to individual strings to get user and score. 
                string[] spliJson = webRequest.downloadHandler.text.Split('{', '}');

                foreach (string s in spliJson)
                {
                    if (s.StartsWith("\"name\""))
                    {
                        string[] userName = s.Split(',');
                        string name = "";
                        string score = "";

                        foreach (string u in userName)
                        {
                            if (u.StartsWith("\"name\""))
                            {
                                char[] user = new char[16];
                                u.CopyTo(8, user, 0, u.Length - 9);
                                name = new string(user);
                            }
                            else if (u.StartsWith("\"score\""))
                            {
                                char[] score2 = new char[16];
                                u.CopyTo(8, score2, 0, u.Length - 8);
                                score = new string(score2);
                            }
                        }

                        usersList.Add(new UserScore(name, Convert.ToInt32(score)));
                    }
                }
            }
            finish = true;
        }
    }

    //Only use this when export to Mobile 
    // in other way use GetFirstTenUsersDesktop(); 
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

    public void PostUser(UserScore user)
    {
        RestClient.Post<UserScore>($"https://pruebasapirest-13c28.firebaseio.com/users.json", user);
    }
}