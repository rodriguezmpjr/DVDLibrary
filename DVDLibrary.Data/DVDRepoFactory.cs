using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDLibrary.Data
{
    public class DVDRepoFactory
    {
        public static IDVDLibraryRepo GetRepository()
        {
            string repoType = ConfigurationManager.AppSettings["RepoType"].ToString();

            switch (repoType)
            {
                case "Memory":
                    return new DvdRepositoryMock();
                case "DatabaseADO":
                    return new DVDRepoADO();
                case "DatabaseEF":
                    return new DVDRepoEF();
                default:
                    throw new Exception("Could not find a valid repo type");
            }
        }
    }
}
