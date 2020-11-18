﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;

    public GameObject nextScreen;

    void Start()
    {

    }

    public void LoginUser()
    {
        StartCoroutine(LoginUserCo(usernameField.text, passwordField.text));
    }

    IEnumerator GetUsers()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://boostworks.online/GetStudents.php"))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                byte[] results = www.downloadHandler.data;
            }
        }
    }
    IEnumerator LoginUserCo(string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginEmail", email);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://boostworks.online/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }

            else
            {
                try
                {

                    LeerlingJSONConvert leerling = LeerlingJSONConvert.CreateFromJSON(www.downloadHandler.text);

                    //Setup Logged in Leerling
                    LeerlingObject.Id = leerling.id;
                    LeerlingObject.Firstname = leerling.firstname;
                    LeerlingObject.Lastname = leerling.lastname;
                    LeerlingObject.Email = leerling.email;
                    LeerlingObject.Nickname = leerling.nickname;

                    //Continue to next screen
                    FindObjectOfType<ScreenManager>().OpenScreen(nextScreen);
                }
                catch
                {

                    Debug.LogWarning("Credentials not correct");
                }


            }
        }
    }
}