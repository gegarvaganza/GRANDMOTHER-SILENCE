using UnityEngine;
using UHFPS.Runtime;

public class StartIntroCutscene : MonoBehaviour
{
    public CutsceneTrigger cutsceneTrigger;

    void Start()
    {
        cutsceneTrigger.TriggerCutscene();
    }
}