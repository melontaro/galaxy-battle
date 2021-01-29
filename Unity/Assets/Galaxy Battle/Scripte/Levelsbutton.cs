using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Levelsbutton : MonoBehaviour
{
    public GameObject Lock;
    public int Buttonnumber;
    private Manage Manage;

    // Start is called before the first frame update
    void Start()
    {
        Manage = GameObject.Find("Manage").GetComponent<Manage>();
        if (Manage.Lock >= Buttonnumber)
        {
            Destroy(Lock);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Click()
    {
        if (Lock == null)
        {
            Manage.Level = Buttonnumber;
            SceneManager.LoadScene("Play", LoadSceneMode.Single);
        }
    }



    public void Exitlevelsmenu()
    {
        Manage.Items[0].SetActive(true);
        Manage.Items[1].SetActive(true);
        Manage.Items[2].SetActive(true);
        Manage.Items[3].SetActive(true);
        Manage.Items[4].SetActive(false);
        Manage.Items[5].SetActive(true);
    }
}
