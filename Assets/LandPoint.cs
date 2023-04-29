using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LandPoint : MonoBehaviour
{
    GameObject astronaut;
    GameObject starship;
    bool ready = false;
    float upForce = 0f;
    float upForceTarget = 0f;
    float forwardForce = 0f;
    float forwardForceTarget = 0f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        astronaut = GameObject.Find("Astronaut");
        starship = GameObject.Find("Starship");
        rb = starship.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        upForce = Mathf.Lerp(upForce, upForceTarget, 10);
        forwardForce = Mathf.Lerp(forwardForce, forwardForceTarget, 1f);
        rb.AddRelativeForce(Vector3.up * upForce, ForceMode.Acceleration);
        rb.AddRelativeForce(Vector3.forward * forwardForce, ForceMode.Acceleration);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.JoystickButton6))
        {
            Application.Quit();
        }

        if (ready && (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.JoystickButton7)))
        {
            Debug.Log("Go!");
            astronaut.SetActive(false);
            starship.GetComponent<AudioSource>().Play();
            upForceTarget = 25;
            forwardForceTarget = 50;
            
            StartCoroutine(ExampleCoroutine());


        }
    }

    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(7);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    void OnTriggerEnter(Collider target)
    {
        if (target.transform.name == "Astronaut")
        {
            Debug.Log(target.transform.name + " enter");
            ready = true;
        }
    }

    void OnTriggerExit(Collider target)
    {
        if (target.transform.name == "Astronaut")
        {
            Debug.Log(target.transform.name + " exit");
            ready = false;
        }
    }
}
