using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGamesStore.Interfaces
{
    interface IInsert
    {
        void Insert(string table, object[] args);
    }
}
