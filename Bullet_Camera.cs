using UnityEngine;

public class BulletCamera : MonoBehaviour
{

    public GameObject bullet;
    public Transform defaultPosition;
    public float smoothTime = 0.1f;
    public float timeScale = 0.1f;

    private Vector3 velocity = Vector3.zero;
    private bool isHit = false;

    void Update()
    {
        if (isHit)
        {
            Time.timeScale = timeScale;
            Vector3 targetPosition = defaultPosition.position;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        else
        {
            Vector3 targetPosition = bullet.transform.position;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    public void OnBulletHit()
    {
        isHit = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            OnBulletHit();
        }
    }

}
