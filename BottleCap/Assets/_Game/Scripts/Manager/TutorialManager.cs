using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{ 
    [System.Serializable]
    public class TutorialMoment
    {
        public TutorialSection section;
        public TutorialSection nextSection;
        public TutorialManager manager;
        [System.NonSerialized] public bool completed = false;
        [System.NonSerialized] public bool started = false;
        public UnityEvent OnStarted;
        public UnityEvent OnCompleted;

        public bool shouldDoDialogue = false;
        [TextArea]
        public string dialogueText = "";
        public bool shouldDoObjective = false;
        [TextArea]
        public string objectiveText = "";

        public void TryComplete()
        {
            if(!completed)
            {
                OnCompleted.Invoke();
                completed = true;
                manager.SetSection(nextSection);
                ObjectiveUI.SetObjective("");
            }
        }

        public void TryStart()
        {
            if (!started)
            {
                OnStarted.Invoke();
                started = true;
                if(shouldDoDialogue)
                {
                    DialogueManager.Instance.ShowText(dialogueText);
                } 
            }
        }

        public void ShowObjectiveText()
        {
            if (shouldDoObjective)
            {
                ObjectiveUI.SetObjective(objectiveText);
            }
        }
    }

    public void ShowObjectText()
    {
        var moment = momentMap[currentSection];
        moment.ShowObjectiveText();
    }

    private void SetSection(TutorialSection nextSection)
    {
        this.currentSection = nextSection;
        momentMap[nextSection].TryStart();
    }

    [SerializeField] private TutorialSection firstSection = TutorialSection.House;

    public enum TutorialSection
    {
        House,
        HouseTwo,
        GetToBeach,
        ThrowFirstBottle,
        FindFirstBottle,
        FinishTutorial
    }

    public TutorialMoment[] moments;

    private Dictionary<TutorialSection, TutorialMoment> momentMap 
        = new Dictionary<TutorialSection, TutorialMoment>();
    private TutorialSection currentSection;

    public static TutorialManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError($"Instance of {nameof(TutorialManager)} already exists");
        }

        foreach (var moment in moments)
        {
            momentMap.Add(moment.section, moment);
        }
    }

    private void Start()
    {
        SetSection(firstSection);
    }

    public void TriggerEventCompleted(TutorialSection section)
    {
        if(currentSection == section)
        {
            momentMap[section].TryComplete();
        }
    }

    public void TriggerMomentStart(TutorialSection section)
    {
        momentMap[section].TryStart();
    }
}
