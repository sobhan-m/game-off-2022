using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
	public string speaker;
	[TextArea] public string text;
	public Dialogue nextDialogue;
}
