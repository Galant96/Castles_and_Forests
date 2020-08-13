using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used to provide any text information such as tutorials or dialogues into the game.
[CreateAssetMenu(menuName = "TextInfo")]
public class TextInfo : ScriptableObject
{
	[TextArea(14, 10)][SerializeField]
	string textToDisplay = null;

	public string GetText()
	{
		return textToDisplay;
	}
}
