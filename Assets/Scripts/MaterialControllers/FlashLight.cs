using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MaterialControllers
{
    public class FlashLight : MaterialController
    {
        private static readonly int EmissiveExposureWeight = Shader.PropertyToID("_EmissiveExposureWeight");
        [SerializeField] private MaterialControllerType type = MaterialControllerType.Swing;
        [SerializeField] private float period = 1.5f;
        private float _timer;
        private bool _reversed;
        // Update is called once per frame
        private void Update()
        {
            if (_reversed)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0) _reversed = false;
            }
            else
            {
                _timer += Time.deltaTime;
                if (_timer >= period) _reversed = true;
            }
            
            switch (type)
            {
                case MaterialControllerType.Random:
                    material.SetFloat(EmissiveExposureWeight, Random.Range(0.0f, 1.0f));
                    break;
                case MaterialControllerType.Swing:
                    material.SetFloat(EmissiveExposureWeight, _timer / period);
                    break;
                default:
                    break;
            }
        }
    }
}
