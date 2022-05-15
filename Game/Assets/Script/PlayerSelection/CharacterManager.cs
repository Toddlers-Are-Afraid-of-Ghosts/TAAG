using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase CharacterDB;

    public Text nameText;
    public SpriteRenderer artworkSprite;

    private int SelectedOption = 0;


    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("SelectedOption"))
        {
            SelectedOption=0;
        }
        else
        {
            Load();
        }

        UpdateCharacter(SelectedOption);
    }

    //permet d'afficher le character suivant en cliquant sur le bouton suivant
    public void NextOption()
    {
        SelectedOption++;

        if(SelectedOption>=CharacterDB.CharacterCount)
        {
            SelectedOption=0;
        }

        UpdateCharacter(SelectedOption);
        Save();
    }


    //permet d'afficher le character précédent en cliquant sur le bouton retour
    public void BackOption()
    {
        SelectedOption--;

        if(SelectedOption < 0)
        {
            SelectedOption=CharacterDB.CharacterCount-1;
        }

        UpdateCharacter(SelectedOption);
        Save();
    }

    //change les caractéristiques du character affiché
    private void UpdateCharacter(int SelectedOption)
    {
        Character character= CharacterDB.GetCharacter(SelectedOption);
        artworkSprite.sprite=character.CharacterSprite;
        nameText.text=character.CharacterName;
    }

    //permet de commencer avec le joueur choisi
    private void Load()
    {
        SelectedOption = PlayerPrefs.GetInt("SelectedOption");
    }


    // sauvegarde le dernier character sur lequel était le joueur
    // pour qu'il puisse revenir dessus
    private void Save()
    {
        PlayerPrefs.SetInt("SelectedOption", SelectedOption);
    }

    //commencer la partie
    public void ChangeScene (int SceneID)
    {
        SceneManager.LoadScene(SceneID);
    }
}
