using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PickingUpItems : MonoBehaviour
{
    int score;
    float timer;
    bool startDeleteMessage;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        startDeleteMessage = false;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (startDeleteMessage)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                GameObject.Find("General Info").GetComponent<Text>().text = "";
                timer = 0.0f;
                startDeleteMessage = false;
            }
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        string nameOfObject = coll.collider.gameObject.name;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.tag == "Ammo")
        {
            string nameOfObject = hit.collider.gameObject.name;
            Destroy(hit.collider.gameObject);
            score++;
            GameObject.Find("General Info").GetComponent<Text>().text = "Picked up "+ score +" ammo boxes";
            startDeleteMessage = true;
            if (score > 5) SceneManager.LoadScene("Level2_Compound");
        }
    }
}
