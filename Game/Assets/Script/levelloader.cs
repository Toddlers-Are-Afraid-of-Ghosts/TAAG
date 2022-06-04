using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelloader : MonoBehaviour
{

    public GameObject LoadingScreen;
    public Slider slider;
    public UnityEngine.UI.Text ProgressText;

   //load l'ecran de chargement
    public void Loadlevel (int SceneIndex)
    {
        StartCoroutine(LoadAsync(SceneIndex));
    }
    public void Loadlevel (string Scene)
    {
        StartCoroutine(LoadAsync(Scene));
    }
     IEnumerator LoadAsync (int SceneIndex)
    {
        AsyncOperation operation= SceneManager.LoadSceneAsync(SceneIndex);

        LoadingScreen.SetActive(true);


        while (!operation.isDone)
        {
            float progress= Mathf.Clamp01(operation.progress / 0.9f);

            //permet de visualiser le pourcentage
            ProgressText.text=progress * 100 + "%";

            //permet de visualiser l'avancement de la bande
            slider.value=progress;
            yield return null;
        }
    }
     IEnumerator LoadAsync (string Scene)
     {
         AsyncOperation operation= SceneManager.LoadSceneAsync(Scene);

         LoadingScreen.SetActive(true);


         while (!operation.isDone)
         {
             float progress= Mathf.Clamp01(operation.progress / 0.9f);

             //permet de visualiser le pourcentage
             ProgressText.text=progress * 100 + "%";

             //permet de visualiser l'avancement de la bande
             slider.value=progress;
             yield return null;
         }
     }

    

}

