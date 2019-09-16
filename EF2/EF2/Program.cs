using EF2.Context;
using EF2.Model;
using EF2.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2
{
    class Program
    {
        static void Main(string[] args)
        {
            //InertService.Test();
            //JoinDeleteService.Test();
            //TransactionService.Test();
            //JoinTransactionService.Test();
            //ProceService.Test();
            //ProceCmdService.Test();
            //MySqlProceService.Test();
            //MySqlProceCmdService.Test();
            //MsSqlInsertService.Test();
            //BFDbContextService.Test();
            //BFDbContextDelService.Test();
            //FaceService.Test();
            FaceDelService.Test();
            Console.ReadKey();
        }
    }
}
