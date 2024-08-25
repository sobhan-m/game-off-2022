using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class CreditMover : MonoBehaviour
{
    [SerializeField] private RectTransform endGoal;
    [SerializeField][Min(0)] private float waitSecondsBeforeScrolling;
    [SerializeField] private float secondsToScroll;
    [SerializeField] private float waitSecondsBeforeLoadingNextScene;
    private bool isScrolling = false;
    private RectTransform currentTransform;
    private float speed;

    private void Awake() {
        currentTransform = GetComponent<RectTransform>();
        speed = (endGoal.localPosition.y - currentTransform.localPosition.y) / secondsToScroll; // velocity = displacement / change in time
    }

    private void Start()
    {
        Invoke("StartScrolling", waitSecondsBeforeScrolling);
    }

    private void Update()
    {
        if (isScrolling)
        {
            Scroll();
        }
        if (IsAtGoal() && isScrolling)
        {
            Invoke("GoToNextScene", waitSecondsBeforeLoadingNextScene);
        }
    }

    private bool IsAtGoal()
    {
        return currentTransform.localPosition.y >= endGoal.localPosition.y;
    }

    private void StartScrolling()
    {
        isScrolling = true;
    }

    private void Scroll()
    {
        Vector3 position = currentTransform.localPosition;
        currentTransform.localPosition = new Vector3(position.x, position.y + speed * Time.deltaTime, position.z);
    }

    private void GoToNextScene()
    {
        FindObjectOfType<SceneChangeManager>().LoadNextScene();
    }
}
