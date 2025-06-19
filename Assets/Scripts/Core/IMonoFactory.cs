using UnityEngine;

public interface IMonoFactory<T> where T : MonoBehaviour
{
    T Create(Vector3 position);
    T Create(Vector3 position, Quaternion rotation);
}