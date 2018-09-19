using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECA.Services.Goober.Exceptions;
using ECA.Services.Goober.Config;

namespace ECA.Services.Goober.Service
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
            throw new NYIException("recipe code & document format not yet known");
        }
    }
}
