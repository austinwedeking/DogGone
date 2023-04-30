using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] Animation fadeAnimation;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fade());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewLevel()
    {
        this.gameObject.GetComponent<Text>().color = new Color(255, 255, 255, 1f);
        
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            Debug.Log("the statement was reached");
            this.gameObject.GetComponent<Text>().text = "Nearly out of the forest...";
            StartCoroutine(Fade());
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            this.gameObject.GetComponent<Text>().text = "Back in the city...";
            StartCoroutine(Fade());
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            this.gameObject.GetComponent<Text>().text = "Almost home, seems like danger's ahead...";
            StartCoroutine(Fade());
        }
    }

    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(3f);
        fadeAnimation.Play();
        
    }
}
