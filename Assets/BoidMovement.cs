using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMovement : MonoBehaviour
{
    Vector2 currPosition;
    Vector2 forcesX;
    Vector2 forcesY;
    Vector2 center;
    Vector2 matchedVel;
    Vector2 avoidedCol;
    Vector2 ourDirection;
    Vector2 direction;
    float myRadius;
    public Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        direction = Random.insideUnitCircle * 2;
        myRadius = 2f;
        _rb.velocity = direction;
        StartCoroutine(Move());
    }

    void FixedUpdate()
    {
        currPosition = transform.position;
        Debug.DrawRay(currPosition, ourDirection, Color.red);
        Debug.DrawRay(currPosition, center, Color.blue);
        Debug.DrawRay(currPosition, matchedVel, Color.green);


    }

    IEnumerator Move()
    {
        while (true)
        {
            Collider2D[] neighbors = Physics2D.OverlapCircleAll(currPosition, myRadius);
            center = GoToCenter(neighbors);
            matchedVel = MatchVelocity(neighbors);
            avoidedCol = AvoidCollision(neighbors);
            ourDirection = _rb.velocity + center + matchedVel + avoidedCol;
            //look in travel direction
            float angle = Mathf.Atan2(ourDirection.x, ourDirection.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, -Vector3.forward);
            CheckBounds();
            if(ourDirection.x > 4f) {
                ourDirection.x = 4f;
            }
            if(ourDirection.x < -4f) {
                ourDirection.x = -4f;
            }
            if(ourDirection.y > 4f) {
                ourDirection.y = 4f;
            }
            if(ourDirection.y < -4f) {
                ourDirection.y = -4f;
            }
            _rb.velocity = ourDirection;
            yield return new WaitForSeconds(45 / 60);
        }
    }

    void CheckBounds()
    {
        
        Vector2 distanceMax = CameraController.cam.max - currPosition;
        Vector2 distanceMin = CameraController.cam.min - currPosition;

        //MAX X
        if (distanceMax.x < 3 && distanceMax.x > 0)
        {
            //ourDirection.x = Mathf.Abs(ourDirection.x) * -1 / 2;
            ourDirection.x -= (1/distanceMax.x)/50;
        }
        else if(distanceMax.x < 0)
        {
             ourDirection.x += (distanceMax.x)/50;
        }
        //MIN X
        if (distanceMin.x > -3  && distanceMin.x < 0)
        {
            //ourDirection.x = Mathf.Abs(ourDirection.x) / 2;
            ourDirection.x -= (1/distanceMin.x)/50;
        }
        else if(distanceMin.x > 0)
        {
             ourDirection.x += (distanceMin.x)/50;
        }
        //MAX Y
        if (distanceMax.y < 3 && distanceMax.y > 0)
        {
            //ourDirection.y = Mathf.Abs(forcesY.y) * -1 / 2;
            ourDirection.y -= (1/distanceMax.y)/50;
        }
         else if(distanceMax.y < 0)
        {
             ourDirection.y += (distanceMax.y)/50;
        }
        //MIN Y
        if (distanceMin.y > -3  && distanceMin.y < 0)
        {
            //ourDirection.y = Mathf.Abs(ourDirection.y) / 2;
            ourDirection.y -= (1/distanceMin.y)/50;
        }
        else if(distanceMin.y > 0)
        {
             ourDirection.y += (distanceMin.y)/50;
        }
    }

    Vector2 GoToCenter(Collider2D[] n)
    {
        //perceived center
        Vector2 pc = Vector2.zero;
        for (int i = 0; i < n.Length; i++)
        {
            if (n[i].attachedRigidbody != _rb)
                pc += (Vector2)n[i].transform.position;
        }
        if (n.Length > 1)
            pc /= (n.Length - 1);
        pc -= currPosition;
        return pc / 80;
    }

    Vector2 MatchVelocity(Collider2D[] n)
    {
        //perceived velocity
        Vector2 pv = Vector2.zero;
        for (int i = 0; i < n.Length; i++)
        {
            if (n[i].attachedRigidbody != _rb)
            {
                pv += n[i].attachedRigidbody.velocity;
            }
        }

        if (n.Length > 1)
            pv /= (n.Length - 1);
        
        pv -= _rb.velocity;

        return pv / 50;
    }

    Vector2 AvoidCollision(Collider2D[] n) {
        
        //perceived distance
        Vector2 pd = Vector2.zero;
        for (int i = 0; i < n.Length; i++)
        {
            if (n[i].attachedRigidbody != _rb)
            {
                if(Mathf.Abs(n[i].transform.position.x - currPosition.x) < 1.25f || Mathf.Abs(n[i].transform.position.y - currPosition.y) < 1.25f)
                {
                    Vector2 distance = (Vector2)n[i].transform.position - currPosition;
                    Vector2 avoid = new Vector2( 1/distance.x, 1/distance.y);
                    pd -= avoid;
                }
            }
        }

        return pd / 80;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, myRadius);
    }

    IEnumerator PickNewDirection()
    {
        while (true)
        {
            float randFloat = Random.Range(0, 100);
            yield return new WaitForSeconds(3);
            if (randFloat < 50)
            {
                direction = Random.insideUnitCircle;
                _rb.velocity /= 2;
                yield return null;
                _rb.velocity /= 2;
                yield return null;
                _rb.velocity += direction;
            }
        }
    }

}

