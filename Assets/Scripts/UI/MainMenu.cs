using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //public Animator animator;
   // public Animator animator2;
    //这两个动画一个控制场景渐黑（大canvas，黑色幕布alpha从0到1. 一个是BGM渐小）
    public void PlayGame()
    {
        
        StartCoroutine(StartGame(3.0f));
    }
    IEnumerator StartGame(float timer)
    {
        //animator.SetTrigger("Start");
        //animator2.SetTrigger("fade");
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene("CyberCity");
    }
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
   
    void Start()
    {
        
    }
}
