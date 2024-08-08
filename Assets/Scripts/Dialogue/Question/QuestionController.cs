using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour {

    public Question question;
    public Text questionText;
    public Button choiceButton;
    //LAGT TILL FÖLJANDE SjÄLV//
    public GameObject firstSpeakerUI;
    public GameObject secondSpeakerUI;
    //LAGT TILL OVAN SJÄLV

    private List<ChoiceController> choiceControllers = new List<ChoiceController>();

    public void Change(Question _question)
    {
        RemoveChoices();
        question = _question;
        gameObject.SetActive(true);
        //LAGT TILL UNDER SJÄLV
        firstSpeakerUI.SetActive(false);
        secondSpeakerUI.SetActive(false);
        //LAGT TILL OVAN SJÄLV
        Initialize();
    }

    public void Hide(Conversation conversation)
    {
        RemoveChoices();
        gameObject.SetActive(false);
    }

    public void RemoveChoices()
    {
        foreach (ChoiceController c in choiceControllers)
            Destroy(c.gameObject);

        choiceControllers.Clear();
    }

    private void Initialize()
    {
        questionText.text = question.text;

        for(int index = 0; index < question.choices.Length; index++)
        {
            ChoiceController c = ChoiceController.AddChoiceButton(choiceButton, question.choices[index], index);
            choiceControllers.Add(c);
        }

        choiceButton.gameObject.SetActive(false);
    }
        
        // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
