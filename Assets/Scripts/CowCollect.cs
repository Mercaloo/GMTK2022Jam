using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowCollect : MonoBehaviour
{
    [SerializeField] GameObject scoreManager;
    [SerializeField] GameObject abduction;
    [SerializeField] GameObject abductionSound;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hi");
        if (other.gameObject.CompareTag("Player"))
        {
            scoreManager.GetComponent<ScoreManager>().AddPoints(1);
            Instantiate(abduction, transform.position, Quaternion.identity);
            GameObject cowSound = Instantiate(abductionSound, transform.position, Quaternion.identity);
            cowSound.GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
            cowSound.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}
