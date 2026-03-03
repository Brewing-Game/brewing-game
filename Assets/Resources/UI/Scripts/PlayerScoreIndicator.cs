using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScoreIndicator : MonoBehaviour
{
    [SerializeField]
    public GameObject counter;
    private TextMeshProUGUI counterTextMesh;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        if (!counter) Debug.Log("PlayerScoreIndicator does not have counter. Assign it in the editor.");

        counterTextMesh = counter.GetComponent<TextMeshProUGUI>();
        if (!counterTextMesh) Debug.Log("PlayerScoreIndicator.counter does not have TextMeshProUGUI.");
    }

    // Update is called once per frame
    void Update()
    {
        if (counterTextMesh.text != score.ToString())
        {
            counterTextMesh.text = score.ToString();
        }
    }
}
