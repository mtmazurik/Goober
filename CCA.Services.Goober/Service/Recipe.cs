using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCA.Services.Goober.Exceptions;
using CCA.Services.Goober.Config;

namespace CCA.Services.Goober.Service
{
    public class Recipe : IRecipe
    {
        IJsonConfiguration _config;
        public Recipe( IJsonConfiguration config)       // ctor
        {
            _config = config;
        }

        public void GetPeanutButterRecipe()
        {
            throw new NYIException("recipe code & document format not yet known.");
        }
    }
}
