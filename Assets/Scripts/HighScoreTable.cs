using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private HighScoreManager highScoreManager;

    private void Awake()
    {
        highScoreManager = GetComponent<HighScoreManager>();
        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        float templateHeight = 90;

        for (int i = 0; i < highScoreManager.scores.Count; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);

            int rank = i + 1;

            entryTransform.Find("Rank").GetComponent<TextMeshProUGUI>().text = rank.ToString();
            entryTransform.Find("Score").GetComponent<TextMeshProUGUI>().text = highScoreManager.scores[i].score.ToString();
            entryTransform.Find("Name").GetComponent<TextMeshProUGUI>().text = highScoreManager.scores[i].playerName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
