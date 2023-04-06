using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    [SerializeField] float dialogDisplayTime = 4f;
    [SerializeField] GameObject dialogBox;

    private float displayTimer;

    void Start()
    {
        dialogBox.SetActive(false);
        displayTimer = -1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (displayTimer >= 0)
        {
            displayTimer -= Time.deltaTime;

            if (displayTimer < 0) dialogBox.SetActive(false);
        }
    }

    public void DisplayDialog()
    {
        displayTimer = dialogDisplayTime;
        dialogBox.SetActive(true);
    }
}
