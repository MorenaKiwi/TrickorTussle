using System.Collections;
using UnityEngine;
using TMPro;

public class CheckpointManager : MonoBehaviour
{
    public Transform buildingsParent;
    public GameObject checkpointLight;
    public float timerDuration = 45.0f;
    public GameObject timerParent; // Reference to the parent object that holds the timer elements
    public TextMeshProUGUI timerText; // Reference to the UI Text element

    private bool isCheckpointActive = false;
void Start()
{
    StartCoroutine(StartCheckpointTimer(3.0f));
    GameObject buildingsParentObject = GameObject.Find("SceneAsset");
    buildingsParent = buildingsParentObject.transform;
    

}

IEnumerator StartCheckpointTimer(float displayTime)
{
    timerParent.SetActive(false);
    checkpointLight.SetActive(false);
    yield return new WaitForSeconds(displayTime); 
    timerParent.SetActive(true);

    // Activate the checkpoint light
    checkpointLight.SetActive(true);

    StartCoroutine(CheckpointTimer());
    //ActivateRandomBuilding();
}

IEnumerator CheckpointTimer()
{
    float remainingTime = timerDuration;

    while (remainingTime > 0)
    {
        // Update the UI Text with the remaining time
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.CeilToInt(remainingTime).ToString();
        }

        yield return new WaitForSeconds(1.0f); // Wait for 1 second
        remainingTime -= 1.0f; // Decrement the remaining time
    }
}



}