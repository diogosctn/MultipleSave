using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.DomainObject;

namespace MultipleSave
{
    public class MultileSaveTreeManager
    {
        public static void CreateFolder()
        {
            if (!PetrelProject.IsPrimaryProjectOpen)
            {
                PetrelLogger.ErrorBox("Um projeto Petrel deve estar aberto para criar pastas.");
                return;
            }

            MultipleSaveTreeFolder multipleSaveTreeFolder = new MultipleSaveTreeFolder();

            using (ITransaction txn = DataManager.NewTransaction())
            {
                txn.Lock(PetrelProject.PrimaryProject);
                PetrelProject.PrimaryProject.Extensions.Add(multipleSaveTreeFolder);
                txn.Commit();
            }
        }

        public static void CreateFullDataItem()
        {
            if (!PetrelProject.IsPrimaryProjectOpen)
            {
                PetrelLogger.ErrorBox("Um projeto Petrel deve estar aberto para criar pastas.");
                return;
            }

            MultipleSaveTreeItem multipleSaveTreeItem = new MultipleSaveTreeItem("");

            using (ITransaction txn = DataManager.NewTransaction())
            {
                txn.Lock(PetrelProject.PrimaryProject);
                PetrelProject.PrimaryProject.Extensions.Add(multipleSaveTreeItem);
                txn.Commit();
            }
        }

        public static void CreateTextDataItem()
        {
            if (!PetrelProject.IsPrimaryProjectOpen)
            {
                PetrelLogger.ErrorBox("Um projeto Petrel deve estar aberto para criar pastas.");
                return;
            }

            MultipleSaveTextTreeItem multipleSaveTextTreeItem = new MultipleSaveTextTreeItem("");

            using (ITransaction txn = DataManager.NewTransaction())
            {
                txn.Lock(PetrelProject.PrimaryProject);
                PetrelProject.PrimaryProject.Extensions.Add(multipleSaveTextTreeItem);
                txn.Commit();
            }
        }

        public static void CreateNumberDataItem()
        {
            if (!PetrelProject.IsPrimaryProjectOpen)
            {
                PetrelLogger.ErrorBox("Um projeto Petrel deve estar aberto para criar pastas.");
                return;
            }

            MultipleSaveNumberTreeItem multipleSaveNumberTreeItem = new MultipleSaveNumberTreeItem("");

            using (ITransaction txn = DataManager.NewTransaction())
            {
                txn.Lock(PetrelProject.PrimaryProject);
                PetrelProject.PrimaryProject.Extensions.Add(multipleSaveNumberTreeItem);
                txn.Commit();
            }
        }
    }
}

