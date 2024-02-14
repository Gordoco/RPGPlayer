using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using AOT;

public static class GM
{
    public static string promptToUse = "A Generic RPG Character from DND";
    public static string campaignRegion = "A cold mountainous region";
    public static CharacterData data;
    public static bool bReturnToCharacterSelect = false;
}

public class CharacterData
{
    public string Race;
    public string Class;
    public int[] AbilityScores;
    public string Gender;
    public string HeadShape;
    public string NoseShape;
    public string HairColor;
    public string EyeColor;
    public string FacialHairDescription;
    public string ChinDescription;
    public string NeckDescription;
    public string BodyDescription;
    public string Height;
    public string ClothesDescription;
    public string WeaponDescription;
    public string ExtraDescription;
    public string Lifestyle;
}

public class ButtonManager : MonoBehaviour
{

    private void Start()
    {
        if (GM.bReturnToCharacterSelect)
        {
            GM.bReturnToCharacterSelect = false;
            RestoreFields();
        }
    }

    [DllImport("__Internal")]
    public static extern void ClipboardWriter(string newClipText);

    public void CopyToClipboard(string str)
    {
        GUIUtility.systemCopyBuffer = str;
    }

    public void CharacterCreation_Clicked()
    {
        SceneManager.LoadScene("CharacterCreation");
    }

    public void MyCharacters_Clicked()
    {
        SceneManager.LoadScene("MyCharacters");
    }

    public void BackButtonMeta_Clicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void BackButtonFinalization_Clicked()
    {
        GM.bReturnToCharacterSelect = true;
        SceneManager.LoadScene("CharacterCreation");
    }

    public void SaveCharacter_Clicked()
    {

    }

    public void CharacterCreation_SubmitGameplay_Clicked()
    {
        GameObject gameplay = GameObject.FindGameObjectWithTag("Gameplay");
        GameObject lore = GameObject.FindGameObjectWithTag("Lore");
        gameplay.GetComponent<Canvas>().sortingOrder = -1;
        lore.GetComponent<Canvas>().sortingOrder = 0;
    }

    public void CopyURL_Clicked()
    {
        GameObject text = GameObject.FindGameObjectWithTag("URLText");
        TMPro.TMP_InputField IF = text.GetComponent<TMPro.TMP_InputField>();
        ClipboardWriter(IF.text);
    }

    public void CharacterCreation_BackLore_Clicked()
    {
        GameObject gameplay = GameObject.FindGameObjectWithTag("Gameplay");
        GameObject lore = GameObject.FindGameObjectWithTag("Lore");
        gameplay.GetComponent<Canvas>().sortingOrder = 0;
        lore.GetComponent<Canvas>().sortingOrder = -1;
    }

    public void CharacterCreation_SubmitFinal_Clicked()
    {
        CreatePrompt();
        //Debug.Log(GM.promptToUse);
        SceneManager.LoadScene("CharacterFinalization");
    }

    private void CreatePrompt()
    {
        CharacterData info = LoadData();
        GM.data = info;
        GM.promptToUse = "A " + info.Race + " " + info.Class + " from a popular fantasy game like DND or World of Warcraft preparing to embark on a new chapter of their life. The picture should be a dramatic portrait of the character with nothing else obstructing the artwork including all of its pets and items amid its original environment: " + info.FacialHairDescription + ". The physical description of this character is as follows: the lifestyle of the character is " + info.Lifestyle + ", the gender of the character is " + info.Gender + ", The head of the character is " + info.HeadShape + ", the nose of the character is " + info.NoseShape + ", the hair color of the character is " + info.HairColor + ", the eye color of the character is " + info.EyeColor + ", the chin of the character is " + info.ChinDescription + ", the neck of the character is " + info.NeckDescription + ", the body of the character is " + info.BodyDescription + ", the character is " + info.Height + ", the character is wearing " + info.ClothesDescription + ", the chracter is weilding " + info.WeaponDescription + ", the character has " + info.ExtraDescription + " which is absolutely essential to the image. Prioritize accuracy of the image over quality of the image.";
    }

    private void RestoreFields()
    {
        CharacterData characterData = GM.data;
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("PlayerData");
        characterData.AbilityScores = new int[6];
        for (int i = 0; i < gameObjects.Length; i++)
        {
            int val;
            try
            {
                val = int.Parse("" + gameObjects[i].name[0] + gameObjects[i].name[1]);
            }
            catch (System.Exception)
            {
                val = int.Parse("" + gameObjects[i].name[0]);
            }

            switch (val)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    //if (characterData.AbilityScores.Length >= 6 || characterData.AbilityScores.Length <= 0) break;
                    gameObjects[i].GetComponent<TMPro.TMP_Text>().text = characterData.AbilityScores[0] + "";
                    break;
                case 4:
                    //if (characterData.AbilityScores.Length >= 6 || characterData.AbilityScores.Length <= 0) break;
                    gameObjects[i].GetComponent<TMPro.TMP_Text>().text = characterData.AbilityScores[1] + "";
                    break;
                case 5:
                    //if (characterData.AbilityScores.Length >= 6 || characterData.AbilityScores.Length <= 0) break;
                    gameObjects[i].GetComponent<TMPro.TMP_Text>().text = characterData.AbilityScores[2] + "";
                    break;
                case 6:
                    //if (characterData.AbilityScores.Length >= 6 || characterData.AbilityScores.Length <= 0) break;
                    gameObjects[i].GetComponent<TMPro.TMP_Text>().text = characterData.AbilityScores[3] + "";
                    break;
                case 7:
                    //if (characterData.AbilityScores.Length >= 6 || characterData.AbilityScores.Length <= 0) break;
                    gameObjects[i].GetComponent<TMPro.TMP_Text>().text = characterData.AbilityScores[4] + "";
                    break;
                case 8:
                    //if (characterData.AbilityScores.Length >= 6 || characterData.AbilityScores.Length <= 0) break;
                    gameObjects[i].GetComponent<TMPro.TMP_Text>().text = characterData.AbilityScores[5] + "";
                    break;
                case 9:
                    gameObjects[i].GetComponent<TMPro.TMP_InputField>().text = characterData.HeadShape;
                    break;
                case 10:
                    gameObjects[i].GetComponent<TMPro.TMP_InputField>().text = characterData.NoseShape;
                    break;
                case 11:
                    gameObjects[i].GetComponent<TMPro.TMP_InputField>().text = characterData.HairColor;
                    break;
                case 12:
                    gameObjects[i].GetComponent<TMPro.TMP_InputField>().text = characterData.EyeColor;
                    break;
                case 13:
                    gameObjects[i].GetComponent<TMPro.TMP_InputField>().text = characterData.FacialHairDescription;
                    break;
                case 14:
                    break;
                case 15:
                    break;
                case 16:
                    gameObjects[i].GetComponent<TMPro.TMP_InputField>().text = characterData.ChinDescription;
                    break;
                case 17:
                    gameObjects[i].GetComponent<TMPro.TMP_InputField>().text = characterData.NeckDescription;
                    break;
                case 18:
                    gameObjects[i].GetComponent<TMPro.TMP_InputField>().text = characterData.BodyDescription;
                    break;
                case 19:
                    gameObjects[i].GetComponent<TMPro.TMP_InputField>().text = characterData.Height;
                    break;
                case 20:
                    gameObjects[i].GetComponent<TMPro.TMP_InputField>().text = characterData.ClothesDescription;
                    break;
                case 21:
                    gameObjects[i].GetComponent<TMPro.TMP_InputField>().text = characterData.WeaponDescription;
                    break;
                case 22:
                    gameObjects[i].GetComponent<TMPro.TMP_InputField>().text = characterData.ExtraDescription;
                    break;
            }
        }
    }

    private CharacterData LoadData()
    {
        CharacterData characterData = new CharacterData();
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("PlayerData");
        characterData.AbilityScores = new int[6];
        for (int i = 0; i < gameObjects.Length; i++)
        {
            int val;
            try
            {
                val = int.Parse("" + gameObjects[i].name[0] + gameObjects[i].name[1]);
            }
            catch (System.Exception)
            {
                val = int.Parse("" + gameObjects[i].name[0]);
            }

            switch (val)
            {
                case 1:
                    TMPro.TMP_Dropdown dd1 = gameObjects[i].GetComponent<TMPro.TMP_Dropdown>();
                    //Debug.Log("1: " + gameObjects[i]);
                    characterData.Race = dd1.options[dd1.value].text;
                    break;
                case 2:
                    TMPro.TMP_Dropdown dd2 = gameObjects[i].GetComponent<TMPro.TMP_Dropdown>();
                    //Debug.Log("2: " + gameObjects[i]);
                    characterData.Class = dd2.options[dd2.value].text;
                    break;
                case 3:
                    //if (characterData.AbilityScores.Length >= 6 || characterData.AbilityScores.Length <= 0) break;
                    characterData.AbilityScores[0] = int.Parse(gameObjects[i].GetComponent<TMPro.TMP_Text>().text);
                    break;
                case 4:
                    //if (characterData.AbilityScores.Length >= 6 || characterData.AbilityScores.Length <= 0) break;
                    characterData.AbilityScores[1] = int.Parse(gameObjects[i].GetComponent<TMPro.TMP_Text>().text);
                    break;
                case 5:
                    //if (characterData.AbilityScores.Length >= 6 || characterData.AbilityScores.Length <= 0) break;
                    characterData.AbilityScores[2] = int.Parse(gameObjects[i].GetComponent<TMPro.TMP_Text>().text);
                    break;
                case 6:
                    //if (characterData.AbilityScores.Length >= 6 || characterData.AbilityScores.Length <= 0) break;
                    characterData.AbilityScores[3] = int.Parse(gameObjects[i].GetComponent<TMPro.TMP_Text>().text);
                    break;
                case 7:
                    //if (characterData.AbilityScores.Length >= 6 || characterData.AbilityScores.Length <= 0) break;
                    characterData.AbilityScores[4] = int.Parse(gameObjects[i].GetComponent<TMPro.TMP_Text>().text);
                    break;
                case 8:
                    //if (characterData.AbilityScores.Length >= 6 || characterData.AbilityScores.Length <= 0) break;
                    characterData.AbilityScores[5] = int.Parse(gameObjects[i].GetComponent<TMPro.TMP_Text>().text);
                    break;
                case 9:
                    characterData.HeadShape = gameObjects[i].GetComponent<TMPro.TMP_InputField>().text;
                    break;
                case 10:
                    characterData.NoseShape = gameObjects[i].GetComponent<TMPro.TMP_InputField>().text;
                    break;
                case 11:
                    characterData.HairColor = gameObjects[i].GetComponent<TMPro.TMP_InputField>().text;
                    break;
                case 12:
                    characterData.EyeColor = gameObjects[i].GetComponent<TMPro.TMP_InputField>().text;
                    break;
                case 13:
                    characterData.FacialHairDescription = gameObjects[i].GetComponent<TMPro.TMP_InputField>().text;
                    break;
                case 14:
                    TMPro.TMP_Dropdown dd3 = gameObjects[i].GetComponent<TMPro.TMP_Dropdown>();
                    //Debug.Log("3: " + gameObjects[i]);
                    characterData.Gender = dd3.options[dd3.value].text;
                    break;
                case 15:
                    TMPro.TMP_Dropdown dd4 = gameObjects[i].GetComponent<TMPro.TMP_Dropdown>();
                    //Debug.Log(gameObjects[i]);
                    characterData.Lifestyle = dd4.options[dd4.value].text;
                    break;
                case 16:
                    characterData.ChinDescription = gameObjects[i].GetComponent<TMPro.TMP_InputField>().text;
                    break;
                case 17:
                    characterData.NeckDescription = gameObjects[i].GetComponent<TMPro.TMP_InputField>().text;
                    break;
                case 18:
                    characterData.BodyDescription = gameObjects[i].GetComponent<TMPro.TMP_InputField>().text;
                    break;
                case 19:
                    characterData.Height = gameObjects[i].GetComponent<TMPro.TMP_InputField>().text;
                    break;
                case 20:
                    characterData.ClothesDescription = gameObjects[i].GetComponent<TMPro.TMP_InputField>().text;
                    break;
                case 21:
                    characterData.WeaponDescription = gameObjects[i].GetComponent<TMPro.TMP_InputField>().text;
                    break;
                case 22:
                    characterData.ExtraDescription = gameObjects[i].GetComponent<TMPro.TMP_InputField>().text;
                    break;
            }
        }
        return characterData;
    }
}
