using System.Collections.Generic;
using ECA.Services.Goober.DAL.Models;

namespace ECA.Services.Goober.DAL
{
    public interface IRepository
    {
        void CreatePeanutButter(PeanutButter newPeanutButter);
        List<PeanutButter> ReadAllPeanutButters();
        PeanutButter ReadPeanutButter(int GooberId);
        void UpdatePeanutButter(PeanutButter peanutButter);

    }
}