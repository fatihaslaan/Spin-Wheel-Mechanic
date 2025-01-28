using UnityEngine;

namespace Util
{
    public class ObjectRotator : MonoBehaviour
    {
        public Vector3 rotationSpeed = new Vector3(0, 0, 100);
        private void Update()
        {
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }
    }
}
