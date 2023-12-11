using System;
using System.Collections;
using UnityEngine;

namespace Codebase.Logic
{
  public class LoadingCurtain : MonoBehaviour
  {
    public CanvasGroup Curtain;

    public Action Hidenn;

    private void Awake()
    {
      DontDestroyOnLoad(this);
    }

    public void Show()
    {
      gameObject.SetActive(true);
      Curtain.alpha = 1;
    }
    
    public void Hide() => 
      StartCoroutine(DoFadeIn());
    
    private IEnumerator DoFadeIn()
    {
      while (Curtain.alpha > 0)
      {
        Curtain.alpha -= 0.03f;
        yield return new WaitForSeconds(0.03f);
      }
      
      Hidenn?.Invoke();
      gameObject.SetActive(false);
    }
  }
}