﻿using Zenject;
using UnityEngine;
using System.IO;

namespace IdleMiner
{
    public class IdleMinerGameController : IInitializable
    {
        [Inject] private MineController.Factory _mineFactory;
        [Inject] private MineParameters _mineParameters;

        public void Initialize()
        {
            _mineFactory.Create(_mineParameters);
        }

    }
}
