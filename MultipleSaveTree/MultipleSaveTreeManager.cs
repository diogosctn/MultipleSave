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
            StructuredArchiveDataSource dataSourceComplete = DataSourceCompleteFactory.Get(dsManager);
            StructuredArchiveDataSource dataSourceText = DataSourceTextFactory.Get(dsManager);
            StructuredArchiveDataSource dataSourceNumber = DataSourceNumberFactory.Get(dsManager);

            CustomDomainObjectComplete fullObj = new CustomDomainObjectComplete(dataSourceComplete);
            CustomDomainObjectText textObj = new CustomDomainObjectText(dataSourceText);
            CustomDomainObjectNumber numberObj = new CustomDomainObjectNumber(dataSourceNumber);

            fullObj.Text = text;
            fullObj.Number = number;

            textObj.Text = text;
            numberObj.Number = number;

            MultipleSaveTreeItem multipleSaveTreeItem = new MultipleSaveTreeItem(fullObj);

            MultipleSaveTextTreeItem multipleSaveTextTreeItem = new MultipleSaveTextTreeItem(textObj);
            MultipleSaveNumberTreeItem multipleSaveNumberTreeItem = new MultipleSaveNumberTreeItem(numberObj);

            multipleSaveTreeItem.AddElement(multipleSaveTextTreeItem, new Slb.Ocean.Petrel.Basics.AddElementContext());
            multipleSaveTreeItem.AddElement(multipleSaveNumberTreeItem, new Slb.Ocean.Petrel.Basics.AddElementContext());

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

        public static (string, double) GetDataFromSelectedObject()
        {
            if (!PetrelProject.IsPrimaryProjectOpen) return (null, double.NaN);

            MultipleSaveTreeItem selectedObject1 = PetrelProject.ActiveTree?.GetSelected<MultipleSaveTreeItem>()?.FirstOrDefault();
            MultipleSaveTextTreeItem selectedObject2 = PetrelProject.ActiveTree?.GetSelected<MultipleSaveTextTreeItem>()?.FirstOrDefault();
            MultipleSaveNumberTreeItem selectedObject3 = PetrelProject.ActiveTree?.GetSelected<MultipleSaveNumberTreeItem>()?.FirstOrDefault();

            if (selectedObject1 != null)
            {
                string text = selectedObject1.data.Text;
                double number = selectedObject1.data.Number;

                PetrelLogger.InfoOutputWindow($"Resgatado o objeto '{selectedObject1.NameInfo.Name}': {text} e {number}");
                return (text, number);
            }
            if (selectedObject2 != null)
            {
                string text = selectedObject2.data.Text;

                PetrelLogger.InfoOutputWindow($"Resgatado o objeto '{selectedObject2.NameInfo.Name}': {text}");
                return (text, double.NaN);
            }
            if (selectedObject3 != null)
            {
                double number = selectedObject3.data.Number;

                PetrelLogger.InfoOutputWindow($"Resgatado o objeto '{selectedObject3.NameInfo.Name}': {number}");
                return (null, number);
            }
            else
            {
                PetrelLogger.InfoBox("Por favor, selecione um objeto de texto na árvore do Petrel.");
                return (null, double.NaN);
            }
        }
    }
}

