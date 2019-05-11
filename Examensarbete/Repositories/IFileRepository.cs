using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisProject.Repositories
{
    public interface IFileRepository
    {
        byte[] GetFile(int id, string storedProcedure);
    }
}
