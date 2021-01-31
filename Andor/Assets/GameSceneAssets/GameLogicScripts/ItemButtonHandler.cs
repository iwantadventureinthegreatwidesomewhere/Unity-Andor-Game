using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemButtonHandler : MonoBehaviour
{

	public Button item1;
	public Button item2;
	public Button item3;
	public Button item4;
	public Button item5;


	public Image selected1;
	public Image selected2;
	public Image selected3;
	public Image selected4;
	public Image selected5;


	// Start is called before the first frame update
	public void item1ButtonHandler()
	{
		if (selected1.enabled == true)
		{
			selected1.enabled = false;
		}
		else
		{
			selected1.enabled = true;
		}
	}
	public void item2ButtonHandler()
	{
		if (selected2.enabled == true)
		{
			selected2.enabled = false;
		}
		else
		{
			selected2.enabled = true;
		}
	}

	public void item3ButtonHandler()
	{
		if (selected3.enabled == true)
		{
			selected3.enabled = false;
		}
		else
		{
			selected3.enabled = true;
		}
	}

	public void item4ButtonHandler()
	{
		if (selected4.enabled == true)
		{
			selected4.enabled = false;
		}
		else
		{
			selected4.enabled = true;
		}
	}

	public void item5ButtonHandler()
	{
		if (selected5.enabled == true)
		{
			selected5.enabled = false;
		}
		else
		{
			selected5.enabled = true;
		}
	}

}
