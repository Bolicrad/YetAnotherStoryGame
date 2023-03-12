using UnityEngine;

namespace MaterialControllers
{
    public class MaterialController : MonoBehaviour
    {
        // Start is called before the first frame update
        protected Material material;

        private void Awake()
        {
            material = GetComponent<Renderer>().material;
            Debug.Log($"{GetType()} {name} has the material {material.name}.");
        }
    

        private void OnDestroy()
        {
            Destroy(material);
        }
    }

    public enum MaterialControllerType
    {
        Random,
        Swing
    }

}
