
using UnityEngine;

public class MouseHitDetector : MonoBehaviour
{
    // Update is called once per frame
    public float damage = 10f;
    public float range = 100f;

    public Camera cam;
    void Update()
    {
        // Check if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the mouse cursor's position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Perform the raycast and store the result
            RaycastHit hit;
            /* if (Physics.Raycast(ray, out hit))
             {
                 // The raycast hit an object!
                 // You can access information about the hit object through the "hit" variable
                 Debug.Log("Raycast hit object: " + hit.transform.name);
             }*/
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }
        }
    }
}
