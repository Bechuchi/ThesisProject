using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisProject.StrategyPattern
{
    public interface IDownloadFileBehaviour
    {
        void Download(int id, string cmd);
    }
}
