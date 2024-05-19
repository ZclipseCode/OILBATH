using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarObjectiveFinder : MonoBehaviour
{
    [SerializeField] float scaler = 1f;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] CarMovement movement;
    [SerializeField] int maxVisited = 3;
    List<Transform> visited = new List<Transform>();
    Collider2D col;
    bool searching = true;
    Vector3 startingScale;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        startingScale = col.transform.localScale;
    }

    private void Update()
    {
        if (searching)
        {
            col.transform.localScale += new Vector3(scaler, scaler, 0);
        }

        if (visited.Count >= maxVisited)
        {
            ResetVisited();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Objective") && searching && col != null)
        {
            Vector3 direction = collision.transform.position - transform.position;
            float distance = direction.magnitude;
            direction.Normalize();

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, obstacleLayer);

            if (hit.collider == null)
            {
                foreach (Transform objective in visited)
                {
                    if (collision.transform == objective)
                    {
                        return;
                    }
                }

                searching = false;
                col.transform.localScale = startingScale;
                visited.Add(collision.transform);
                movement.SetObjective(collision.transform);
            }
        }
    }
    
    void ResetVisited()
    {
        Transform lastVisited = visited[visited.Count - 1];
        visited = new List<Transform> { lastVisited };
    }

    public bool GetSearching() => searching;
    public void SetSearching(bool value) => searching = value;
}
