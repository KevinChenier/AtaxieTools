﻿using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    public class ToolConfigs
    {
        public PegboardConfig PegboardConfig;

        public FingerNoseConfig FingerNoseConfig;

        public SimpleToolConfig SampleConfig;

        public SimpleToolConfig MenuConfig;

        public IEnumerable<IToolConfig> All()
        {
            return new List<IToolConfig> 
            {
                PegboardConfig,
                FingerNoseConfig,
                SampleConfig,
                MenuConfig
            };
        }
    }
}