using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatPointModifier : MonoBehaviour
{
    public ModifiableInt statsModifier;
    public TextMeshProUGUI strValueText;
    public TextMeshProUGUI strAllocatedValueText;
    public TextMeshProUGUI defValueText;
    public TextMeshProUGUI defAllocatedValueText;
    public TextMeshProUGUI intValueText;
    public TextMeshProUGUI intAllocatedValueText;
    //Archer
    public TextMeshProUGUI dexValueText;
    public TextMeshProUGUI dexAllocatedValueText;
    private int tempstrholder;
    private int tempdefholder;
    private int tempintholder;
    private float tempdexholder;

    private Player thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        strValueText.text = "" + thePlayer.attributes[0].modifiableValue.BaseValue;
        strAllocatedValueText.text = "" + tempstrholder;
        defValueText.text = "" + thePlayer.attributes[1].modifiableValue.BaseValue;
        defAllocatedValueText.text = "" + tempdefholder;
        if (thePlayer.name == "Mage")
        {
            intValueText.text = "" + thePlayer.attributes[2].modifiableValue.BaseValue;
            intAllocatedValueText.text = "" + tempintholder;
        }
		else if (thePlayer.name == "Archer")
		{
			dexValueText.text = "" + thePlayer.archerRateOfFire;
			dexAllocatedValueText.text = "" + tempdexholder;
		}
	}
    public void AddStrStat()
	{
        if(thePlayer.statPoints > 0)
		{
            tempstrholder++;
            thePlayer.statPoints--;
		}
        else if (thePlayer.statPoints < 1)
		{
            print("You dont have enough stat points");
		}
	}
    public void RemoveStrStat()
	{
        if(tempstrholder > 0)
		{
            tempstrholder--;
            thePlayer.statPoints++;
		}
        else if(tempstrholder < 1)
		{
            print("You cant remove any more points");
		}
	}

    public void AddDefStat()
    {
        if (thePlayer.statPoints > 0)
        {
            tempdefholder++;
            thePlayer.statPoints--;
        }
        else if (thePlayer.statPoints < 1)
        {
            print("You dont have enough stat points");
        }
    }
    public void RemoveDefStat()
    {
        if (tempdefholder > 0)
        {
            tempdefholder--;
            thePlayer.statPoints++;
        }
        else if (tempdefholder < 1)
        {
            print("You cant remove any more points");
        }
    }

    public void AddIntStat()
    {
        if (thePlayer.statPoints > 0)
        {
            tempintholder++;
            thePlayer.statPoints--;
        }
        else if (thePlayer.statPoints < 1)
        {
            print("You dont have enough stat points");
        }
    }
    public void RemoveIntStat()
    {
        if (tempintholder > 0)
        {
            tempintholder--;
            thePlayer.statPoints++;
        }
        else if (tempintholder < 1)
        {
            print("You cant remove any more points");
        }
    }

	//for archer
	public void AddDexStat()
	{
		if (tempdexholder > 0f)
		{
			tempdexholder = +.1f;
			thePlayer.statPoints--;
		}
		else if (tempdexholder < 1f)
		{
			print("You dont have enough stat points");
		}
	}
	public void RemoveDexStat()
	{
		if (tempdexholder > 0f)
		{
			tempdexholder -= .1f;
			thePlayer.statPoints++;
		}
		else if (tempdexholder < 1f)
		{
			print("You cant remove any more points");
		}
	}
	public void ConfirmAllocation()
	{
        thePlayer.attributes[0].modifiableValue.BaseValue += tempstrholder;
        thePlayer.statPointsAllocated += tempstrholder;
        tempstrholder = 0;

        thePlayer.attributes[1].modifiableValue.BaseValue += tempdefholder;
        thePlayer.statPointsAllocated += tempdefholder;
        tempdefholder = 0;

        if (thePlayer.name == "Mage")
        {
            thePlayer.attributes[2].modifiableValue.BaseValue += tempintholder;
            thePlayer.statPointsAllocated += tempintholder;
            tempintholder = 0;
        }
		else if (thePlayer.name == "Archer")
		{
			thePlayer.archerRateOfFire += tempdexholder;
			thePlayer.statPointsAllocated += tempdexholder;
			tempdexholder = 0f;
		}
	}





    


}
