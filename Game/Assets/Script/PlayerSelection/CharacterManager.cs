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

    private void UpdateCharacter(int SelectedOption)
    {
        Character character= CharacterDB.GetCharacter(SelectedOption);
        artworkSprite.sprite=character.CharacterSprite;
        nameText.text=character.CharacterName;
    }

    private void Load()
    {
        SelectedOption = PlayerPrefs.GetInt("SelectedOption");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("SelectedOption", SelectedOption);
    }

    public void ChangeScene (int SceneID)
    {
        SceneManager.LoadScene(SceneID);
    }
}
