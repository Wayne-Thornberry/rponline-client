﻿

using CPoolObjects;

namespace RPOnline.Parts
{

    public static partial class EngineAPI
    {

        public static int[] GetAllExistingPoolObjects()
        {
            var api = new CPoolObjectsAPI();
            return api.GetAllExistingPoolObjects();
        }
    }
}
