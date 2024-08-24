using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreditMover : MonoBehaviour
{
    [SerializeField][Min(0)] private float secondsToScroll;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float secondsToNextScene;
    private bool isScrolling = false;

    private void Start()
    {
        Invoke("StartScrolling", secondsToScroll);
        Invoke("GoToNextScene", secondsToNextScene);
    }

    private void Update()
    {
        if (isScrolling)
        {
            Scroll();
        }
    }

    private void StartScrolling()
    {
        isScrolling = true;
    }

    private void Scroll()
    {
        Vector3 position = transform.position;
        gameObject.transform.position = new Vector3(position.x, position.y + scrollSpeed * Time.deltaTime, position.z);
    }

    private void GoToNextScene()
    {
        FindObjectOfType<SceneChangeManager>().LoadNextScene();
    }
}
