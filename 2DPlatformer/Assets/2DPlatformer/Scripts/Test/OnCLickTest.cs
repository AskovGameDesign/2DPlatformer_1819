using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnCLickTest : MonoBehaviour
{
    public GameObject button;
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject b = Instantiate(button, canvas.transform, false);
            b.transform.localPosition = new Vector3(0f, -100 + (i * 50f), 0f);
            int x = new int();
            x = i;
            b.GetComponent<Button>().onClick.AddListener(() => ClickMe(x));

        }
    }

    void ClickMe(int index)
    {
        Debug.Log("Hello " + index);



    }
}
