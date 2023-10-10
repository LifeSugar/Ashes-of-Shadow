using System;
using System.Collections;
using System.Collections.Generic;
using Edgar.Unity;
using UnityEngine;

public class ManualGenerate : MonoBehaviour
{
    private PlatformerGeneratorGrid2D _generatorGrid2D;

    private void Start()
    {
        _generatorGrid2D = GetComponent<PlatformerGeneratorGrid2D>();
    }

    public void Regenerate()
    {
        _generatorGrid2D.Generate();
    }
}
