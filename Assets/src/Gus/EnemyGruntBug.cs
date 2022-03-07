using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGruntBug : MonoBehaviour
{
    public float EnemyHealth = 100.0f;
    public float EnemyDamage = 10.0f;

    [SerializeField]
    private float moveSpeed = 1.0f;

    private PlayerClass player = null;
    private bool alert = false;
    private bool waitForEvent = false;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerClass.Instance;
        if(player == null)
        {
            Debug.LogError("The ENEMY can't find an active player");
            this.gameObject.SetActive(false);
        }

        UnityEngine.Random.InitState(DateTime.Now.Minute + Mathf.FloorToInt(this.transform.position.magnitude * 4)); // ensures that every enemy has a completley randomly generated number for it's initialization
        StartCoroutine(IdleSequence());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        alert = GoOnAlert();
    }

    private bool GoOnAlert()
    {
        Vector3 direction = (player.transform.position - this.transform.position).normalized;
        //RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction, 10.0f);
        int maximumCollisions = 16;
        RaycastHit2D[] hitResults = new RaycastHit2D[maximumCollisions]; // maximum number of detected collisions
        ContactFilter2D filter2D = new ContactFilter2D();
        int hit = Physics2D.Raycast(this.transform.position, direction, filter2D, hitResults, 100.0f);

        Debug.DrawLine(this.transform.position, player.transform.position, Color.red, 1.0f);
        int count = 0;
        while(hitResults[count].collider.gameObject.tag == "Enemy" && (count < maximumCollisions - 1))
        {
            count++;
        }
        if (hitResults[count].collider.gameObject.tag == "Player")
        {
            StopCoroutine("IdleAction");
            waitForEvent = false;
            this.transform.position += direction * moveSpeed * Time.deltaTime;
            return true;
        }
        return false;
    }

    private void IdleStuff()
    {
        waitForEvent = true;
        StartCoroutine(IdleAction(Mathf.FloorToInt(UnityEngine.Random.Range(0.0f, 3.0f)), UnityEngine.Random.Range(1.0f, 3.0f)));
    }

    private IEnumerator IdleSequence()
    {
        float timer = 1.5f;
        float i = 0.0f;
        while(true)
        {
            if(!alert && !waitForEvent)
            {
                if (i > timer)
                {
                    IdleStuff();
                    i = 0.0f;
                }
                i += Time.deltaTime;
            }

            yield return null;
        }

        yield break;
    }

    private IEnumerator IdleAction(int idleType, float idleLength)
    {
        float timer = (idleType == 0) ? idleLength/3.0f : idleLength;

        Vector3 originalPos = this.transform.position;
        Vector3 originalDir = this.transform.right;
        Vector3 orginialRot = this.transform.eulerAngles;

        int spinDirection = (((Mathf.FloorToInt(idleLength * 11.5f) % 2) * 2) - 1);

        for(float i = 0.0f; i < timer; i += Time.deltaTime) // effectivley creates a timer where we can specify certain tasks to be executed at any given time.
        {
            float smooth = (3 * Mathf.Pow((i / timer), 2)) - (2 * Mathf.Pow((i / timer), 3));
            switch (idleType)
            {
                case 0:
                    this.transform.eulerAngles = new Vector3(orginialRot.x, orginialRot.y, Mathf.Lerp(orginialRot.z, orginialRot.z + (spinDirection * idleLength * 25.0f), smooth)); // rotates in a random direction
                    break;
                case 1:
                    //this.transform.position = Vector3.Lerp(originalPos, originalPos + (originalDir * idleLength * 2.5f), smooth); // moves forward a random ammount of units
                    this.transform.position += this.transform.right * Time.deltaTime * Mathf.Sin((i/timer) * 3.1415f) * 2.5f;
                    break;
                default:
                    //foo
                    break;
            }

            yield return null;
        }

        waitForEvent = false;
        yield break;
    }
}