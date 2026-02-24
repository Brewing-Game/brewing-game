using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public TMP_Text narrationTextBox;
    public GameObject narrationPanel;
    public TextData textData;
    public int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        TextAsset jsonText = Resources.Load<TextAsset>("Text/Narration");
        textData = JsonUtility.FromJson<TextData>(jsonText.text);

        DisplayText(textData.intro, currentIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            DisplayNextText(textData.intro);
        }
    }

    public void DisplayText(string[] lines, int index)
    {
        narrationTextBox.text = lines[index];
    }

    public void DisplayNextText(string[] lines)
    {
        currentIndex++;
        if(currentIndex >= lines.Length)
        {
            narrationPanel.gameObject.SetActive(false);
        }
        else
        {
            DisplayText(lines, currentIndex);
        }        
    }
}
