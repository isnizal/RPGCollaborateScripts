using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HelmetLoad : MonoBehaviour
{
    private Object[] helmetAbandon,helmetBasic,helmetChristmas,helmetEpic,helmetHorns,helmetKnights,helmetLegendary,helmetMillitary, helmetSamurai,helmetSandLords,helmetStyle,helmetSwamp,helmetThrone,helmetUndead, helmetVikings;
    private TMP_Dropdown helmetDropdown;
    private List<string> h_abandonName = new List<string>();
    private List<Sprite> h_abandonSprite = new List<Sprite>();
    
    // Start is called before the first frame update
    void Start()
    {
        helmetBasic = Resources.LoadAll("Helmet/Basic", typeof(Sprite));
        Debug.Log(helmetBasic.GetType());
        //helmetDropdown = GetComponent<TMP_Dropdown>();
        //for (int currentHelmet = 0; currentHelmet < helmetAbandon.Length; currentHelmet++)
        //{
        //    h_abandonName.Add(helmetBasic[currentHelmet].name);
        //    h_abandonSprite.Add(helmetBasic[currentHelmet].game);
        //}
        //helmetDropdown.AddOptions(h_abandonName);
        //helmetDropdown.AddOptions()
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
