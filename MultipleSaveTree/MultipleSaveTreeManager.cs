using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.Data;
using Slb.Ocean.Petrel.DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MultipleSave
{
    public class MultipleSaveTreeManager
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

        public static void SaveTextDataItem(string text)
        {
            if (!PetrelProject.IsPrimaryProjectOpen)
            {
                PetrelLogger.ErrorBox("Um projeto Petrel deve estar aberto para criar pastas.");
                return;
            }

            IDataSourceManager dsManager = DataManager.DataSourceManager;
            StructuredArchiveDataSource dataSource = DataSourceTextFactory.Get(dsManager);

            CustomDomainObjectText obj = new CustomDomainObjectText(dataSource);

            obj.Text = text;

            MultipleSaveTextTreeItem multipleSaveTextTreeItem = new MultipleSaveTextTreeItem(obj);

            using (ITransaction txn = DataManager.NewTransaction())
            {
                try {
                    txn.Lock(PetrelProject.PrimaryProject);
                    PetrelProject.PrimaryProject.Extensions.Add(multipleSaveTextTreeItem);
                    txn.Commit();
                }
                catch (Exception ex)
                {
                    txn.Abandon();
                    PetrelLogger.ErrorBox("Erro ao salvar: " + ex.Message);
                }
            }
        }

        public static void SaveNumberDataItem(string text)
        {
            if (!PetrelProject.IsPrimaryProjectOpen)
            {
                PetrelLogger.ErrorBox("Um projeto Petrel deve estar aberto para criar pastas.");
                return;
            }

            IDataSourceManager dsManager = DataManager.DataSourceManager;
            StructuredArchiveDataSource dataSource = DataSourceTextFactory.Get(dsManager);

            CustomDomainObjectText obj = new CustomDomainObjectText(dataSource);

            obj.Text = text;

            MultipleSaveNumberTreeItem multipleSaveNumberTreeItem = new MultipleSaveNumberTreeItem(obj);

            using (ITransaction txn = DataManager.NewTransaction())
            {
                try
                {
                    txn.Lock(PetrelProject.PrimaryProject);
                    PetrelProject.PrimaryProject.Extensions.Add(multipleSaveNumberTreeItem);
                    txn.Commit();
                }
                catch (Exception ex)
                {
                    txn.Abandon();
                    PetrelLogger.ErrorBox("Erro ao salvar: " + ex.Message);
                }
            }
        }

        public static string GetTextFromSelectedObject()
        {
            if (!PetrelProject.IsPrimaryProjectOpen) return null;

            MultipleSaveTextTreeItem selectedObject = PetrelProject.ActiveTree?.GetSelected<MultipleSaveTextTreeItem>().FirstOrDefault();

            if (selectedObject != null)
            {
                string content = selectedObject.data.Text;

                PetrelLogger.InfoOutputWindow($"Texto resgatado do objeto '{selectedObject.NameInfo.Name}': {content}");
                return content;
            }
            else
            {
                PetrelLogger.InfoBox("Por favor, selecione um objeto de texto na árvore do Petrel.");
                return null;
            }
        }

        public static string GetNumberFromSelectedObject()
        {
            if (!PetrelProject.IsPrimaryProjectOpen) return null;

            MultipleSaveNumberTreeItem selectedObject = PetrelProject.ActiveTree?.GetSelected<MultipleSaveNumberTreeItem>().FirstOrDefault();

            if (selectedObject != null)
            {
                string content = selectedObject.data.Text;

                PetrelLogger.InfoOutputWindow($"Texto resgatado do objeto '{selectedObject.NameInfo.Name}': {content}");
                return content;
            }
            else
            {
                PetrelLogger.InfoBox("Por favor, selecione um objeto de texto na árvore do Petrel.");
                return null;
            }
        }
    }
}

