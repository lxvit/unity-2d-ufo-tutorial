using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour {

    void Start () {
        Hide();
    }
	
	public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
