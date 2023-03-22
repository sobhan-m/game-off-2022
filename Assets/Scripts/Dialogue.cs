using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    [TextArea] public string dialogueText;
    public List<string> responseTexts;
    public List<Dialogue> responseDialogues;
}
