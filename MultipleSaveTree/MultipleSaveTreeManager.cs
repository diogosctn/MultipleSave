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

        public static void SaveItem(string text, double number)
        {
            if (!PetrelProject.IsPrimaryProjectOpen)
            {
                PetrelLogger.ErrorBox("Um projeto Petrel deve estar aberto para criar pastas.");
                return;
            }

            IDataSourceManager dsManager = DataManager.DataSourceManager;
            StructuredArchiveDataSource dataSource = DataSourceTextFactory.Get(dsManager);

            CustomDomainObjectComplete fullObj = new CustomDomainObjectComplete(dataSource);
            CustomDomainObjectText textObj = new CustomDomainObjectText(dataSource);
            CustomDomainObjectNumber numberObj = new CustomDomainObjectNumber(dataSource);

            fullObj.Text = text;
            fullObj.Number = number;

            textObj.Text = text;
            numberObj.Number = number;

            MultipleSaveTreeItem multipleSaveTreeItem = new MultipleSaveTreeItem(fullObj);

            MultipleSaveTextTreeItem multipleSaveTextTreeItem = new MultipleSaveTextTreeItem(textObj);
            MultipleSaveNumberTreeItem multipleSaveNumberTreeItem = new MultipleSaveNumberTreeItem(numberObj);

            using (ITransaction txn = DataManager.NewTransaction())
            {
                try
                {
                    txn.Lock(PetrelProject.PrimaryProject);
                    PetrelProject.PrimaryProject.Extensions.Add(multipleSaveTreeItem);
                    txn.Commit();
                }
                catch (Exception ex)
                {
                    txn.Abandon();
                    PetrelLogger.ErrorBox("Erro ao salvar: " + ex.Message);
                }
            }
        }

        public static void SaveCompleteDataItem(string text, double number)
        {
            if (!PetrelProject.IsPrimaryProjectOpen)
            {
                PetrelLogger.ErrorBox("Um projeto Petrel deve estar aberto para criar pastas.");
                return;
            }

            IDataSourceManager dsManager = DataManager.DataSourceManager;
            StructuredArchiveDataSource dataSource = DataSourceTextFactory.Get(dsManager);

            CustomDomainObjectComplete obj = new CustomDomainObjectComplete(dataSource);

            obj.Text = text;
            obj.Number = number;

            MultipleSaveTreeItem multipleSaveTreeItem = new MultipleSaveTreeItem(obj);

            using (ITransaction txn = DataManager.NewTransaction())
            {
                try
                {
                    txn.Lock(PetrelProject.PrimaryProject);
                    PetrelProject.PrimaryProject.Extensions.Add(multipleSaveTreeItem);
                    txn.Commit();
                }
                catch (Exception ex)
                {
                    txn.Abandon();
                    PetrelLogger.ErrorBox("Erro ao salvar: " + ex.Message);
                }
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

        public static void SaveNumberDataItem(int number)
        {
            if (!PetrelProject.IsPrimaryProjectOpen)
            {
                PetrelLogger.ErrorBox("Um projeto Petrel deve estar aberto para criar pastas.");
                return;
            }

            IDataSourceManager dsManager = DataManager.DataSourceManager;
            StructuredArchiveDataSource dataSource = DataSourceTextFactory.Get(dsManager);

            CustomDomainObjectNumber obj = new CustomDomainObjectNumber(dataSource);

            obj.Number = number;

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

        public static (string, double) GetFullDataFromSelectedObject()
        {
            if (!PetrelProject.IsPrimaryProjectOpen) return (null, double.NaN);

            MultipleSaveTreeItem selectedObject = PetrelProject.ActiveTree?.GetSelected<MultipleSaveTreeItem>().FirstOrDefault();

            if (selectedObject != null)
            {
                string text = selectedObject.data.Text;
                double number = selectedObject.data.Number;

                PetrelLogger.InfoOutputWindow($"Resgatado o objeto '{selectedObject.NameInfo.Name}': {text} e {number}");
                return (text, number);
            }
            else
            {
                PetrelLogger.InfoBox("Por favor, selecione um objeto de texto na árvore do Petrel.");
                return (null, double.NaN);
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

        public static double GetNumberFromSelectedObject()
        {
            if (!PetrelProject.IsPrimaryProjectOpen) return double.NaN;

            MultipleSaveNumberTreeItem selectedObject = PetrelProject.ActiveTree?.GetSelected<MultipleSaveNumberTreeItem>().FirstOrDefault();

            if (selectedObject != null)
            {
                double content = selectedObject.data.Number;

                PetrelLogger.InfoOutputWindow($"Texto resgatado do objeto '{selectedObject.NameInfo.Name}': {content}");
                return content;
            }
            else
            {
                PetrelLogger.InfoBox("Por favor, selecione um objeto de texto na árvore do Petrel.");
                return double.NaN;
            }
        }
    }
}

