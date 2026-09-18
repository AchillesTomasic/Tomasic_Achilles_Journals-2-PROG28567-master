using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;
    // variables for task 1 A //
    public Vector2 bombOffset;// vector for offset
    // Variables for task 1 B //
    public float bombTrailSpacing; // used to space the bombs along the trail
    public int numberOfTrailBombs; // used to determine the number of bombs in the trail

    // used for the coroutine assignment provided in class //
    public float bombSpawnWaitTime = 3f; // time for the bomb to wait before spawning
    private IEnumerator bombWaitCoroutine; // coroutine for the bomb
    void Start()
    {
        Debug.Log(dotProduct(transform.up,enemyTransform.up));
       
    }
    // Update is called once per frame
    void Update()
    {
        // checks if the b key is pressed
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            bombOffset = transform.position + transform.up; // offet of the bomb
            SpawnBombAtOffset(bombOffset); // spawns bomb
        }
        // checks if the t key is pressed
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            SpawnBombTrail(bombTrailSpacing, numberOfTrailBombs); // spawns a trail of bombs behind the player
        }
        
        }
    
    // used to spawn a trail of bombs behind the player
    public void SpawnBombTrail(float inBombSpacing, int inNumberOfBombs)
    {
        Vector3 bombOffsetAmount = transform.position; // used locally to keep track of the distance between bombs
        for(int i = 0; i < inNumberOfBombs; i++)
        {
            bombOffsetAmount.y -= inBombSpacing;
            Instantiate(bombPrefab,bombOffsetAmount , Quaternion.identity);// spawns bomb

        }
    }
    // function that will be used to spawn bomb at an offset
    public void SpawnBombAtOffset(Vector3 inOffset) {
            bombWaitCoroutine = waitToSpawnBomb(bombSpawnWaitTime, inOffset); // sets the bomb coroutine at the start of the game
            StartCoroutine(bombWaitCoroutine); // only starts this isntance of the coroutine                                                              
    }

    // classwork methods //
    //coroutine that is used before spawning bombs when b is pressed
    private IEnumerator waitToSpawnBomb(float waitTimer,Vector3 inOffset)
    {

        yield return new WaitForSeconds(waitTimer); // waits set time
        Instantiate(bombPrefab, inOffset, Quaternion.identity);// spawns bomb
        
    }
    // calculation used to normalize a vector ///classwork////
    public Vector2 NormalizeCustom(Vector2 inVector)
    {
        float mag = inVector.magnitude;
        Vector2 normalize = new Vector2(inVector.x / Mathf.Abs(mag), inVector.y / Mathf.Abs(mag));
        return normalize;
    }
    // calculation used to find the dot product of a vector set. used to find angle between two vectors //// claswork/////
    public float dotProduct(Vector2 inVector, Vector2 enemyVector)
    {
        float mag = inVector.magnitude;
     
        float enemyMag = enemyVector.magnitude;
        // dot product for finding the angle
        float dotprod = ((inVector.x * enemyVector.x) + (inVector.y * enemyVector.y)) / Mathf.Abs(mag) * Mathf.Abs(enemyMag);
        return Mathf.Cos(dotprod); 
    }
}
