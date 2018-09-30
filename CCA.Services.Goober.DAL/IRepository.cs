using System.Collections.Generic;
using CCA.Services.Goober.DAL.Models;

namespace CCA.Services.Goober.DAL
{
    public interface IRepository
    {
        void CreatePeanutButter(PeanutButter newPeanutButter);
        List<PeanutButter> ReadAllPeanutButters();
        PeanutButter ReadPeanutButter(int GooberId);
        void UpdatePeanutButter(PeanutButter peanutButter);

    }
}