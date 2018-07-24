using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DVDLibrary.Models;

namespace DVDLibrary.Data
{
    public class DvdRepositoryMock : IDVDLibraryRepo
    {
        public DVD AddDvd(DVD dvdToAdd)
        {
            int ID = DvdStaticList.MyDvdList.Select(d => d.DVDId).Max() + 1;
            dvdToAdd.DVDId = ID;
            DvdStaticList.MyDvdList.Add(dvdToAdd);
            return dvdToAdd;
        }

        public DVD DeleteDvd(int dvdId)
        {
            DVD toBeDeleted = DvdStaticList.MyDvdList.FirstOrDefault(d => d.DVDId == dvdId);
            if (toBeDeleted != null)
            {
                DvdStaticList.MyDvdList.Remove(toBeDeleted);
            }
            return toBeDeleted;
        }

        public DVD EditDvd(DVD current, DVD updated)
        {
            int index = DvdStaticList.MyDvdList.FindIndex(d => d.DVDId == current.DVDId);
            DvdStaticList.MyDvdList[index] = updated;
            return DvdStaticList.MyDvdList[index];
        }

        public List<DVD> GetAllDvds()
        {
            return DvdStaticList.MyDvdList;
        }

        public DVD GetDvdById(int dvdId)
        {
            return DvdStaticList.MyDvdList.FirstOrDefault(d => d.DVDId == dvdId);
        }
    }
}