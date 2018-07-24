using DVDLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDLibrary.Data
{
    public interface IDVDLibraryRepo
    {
        DVD GetDvdById(int dvdId);
        List<DVD> GetAllDvds();
        DVD AddDvd(DVD dvdToAdd);
        DVD EditDvd(DVD current, DVD updated);
        DVD DeleteDvd(int dvdId);
    }
}
