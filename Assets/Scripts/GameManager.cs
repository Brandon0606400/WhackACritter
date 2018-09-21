using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public List<GameObject> critterPrefabs;
    public float critterSpawnFreqency = 1.0f;
    public Score scoreDisplay;
    public Timer timer;
    public SpriteRenderer button;

    private float lastCritterSpawn = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // Check if it is time to spawn the next critter
        float nextSpawnTime = lastCritterSpawn + critterSpawnFreqency;
        if (Time.time >= nextSpawnTime  && timer.IsTimerRunning() == true)
        {
            // It is time

            //Choose a random criter to spawn
            int critterIndex = Random.Range(0, critterPrefabs.Count);
            GameObject PrefabToSpawn = critterPrefabs[critterIndex];

            // Spawn the critter
            GameObject spawnedCritter = Instantiate(PrefabToSpawn);

            // Get access to our Critter script
            Critter critterScript = spawnedCritter.GetComponent<Critter>();

            //Tell the critter script the score object and te timer object
            critterScript.ScoreDisplay = scoreDisplay;
            critterScript.timer = timer;

            // Update the most recent critter spaw time to now
            lastCritterSpawn = Time.time;
        }

        //Update button visibility
        if (timer.IsTimerRunning() == true)
        {
            button.enabled = false;
        }
        else // if game is not running
        {
            button.enabled = true;
        }
	}

    // Did they click the button?
    private void OnMouseDown()
    {
        // Only respond if game is not running
        if (timer.IsTimerRunning() == false)
        {
            // Start new game!
            timer.StartTimer();
            scoreDisplay.ResetScore();
        }
    }

}
