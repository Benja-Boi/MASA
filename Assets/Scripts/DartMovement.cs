using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartMovement : MonoBehaviour
{
    bool isShot = false;
    public bool IsShot { get { return isShot; } set { isShot = value; } }

    public float airTime = 0.25f;
    public float minSpeed = .5f;
    public float speed;
    public ParticleSystem ps;

    [SerializeField]
    float tranquilTime = 3f;
    float timeShotFired;
    bool hitOccured = false;

    private void Awake()
    {
        ps.Stop();
    }

    void Start()
    {
        timeShotFired = Time.time;
    }

    void Update()
    {
        if (isShot && speed >= minSpeed)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            speed *= SpeedReducerFunction(Time.time - timeShotFired);
            if (speed < minSpeed && !hitOccured)
            {
                StartCoroutine(DestroyDart());
            }
        }
    }

    float SpeedReducerFunction(float x)
    {
        float airTimeScalar = 1 / airTime;
        return Mathf.Max((-airTimeScalar * Mathf.Pow(x, 6)) + 1, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Animal"))
        {
            Rbit rbit = collision.GetComponent<Rbit>();
            rbit.Tranquilize(tranquilTime);
            StartCoroutine(DestroyDart());
        }
    }

    IEnumerator DestroyDart()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        hitOccured = true;
        ps.Play();
        yield return new WaitForSeconds(1);
        yield return null;
        Destroy(this.gameObject);
    }

}
