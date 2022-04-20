using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutliLineStepTimings : MonoBehaviour
{
    LineStepTiming[] _timings;
    private int current = 0;
    public bool simultan = false;
    // Start is called before the first frame update
    void Start()
    {
        _timings = GetComponentsInChildren<LineStepTiming>();
        
    }

    public void Animate()
    {
        if (simultan)
        {
            current = 0;
            for(int i = 0; i < _timings.Length; i++)
            {
                _timings[i].Animate();
            }

        }
        else
        {
            _timings[current].Animate(() =>
            {
                current++;

                _timings[current].Animate();
            });

        }

    }
}
