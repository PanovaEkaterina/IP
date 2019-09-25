using Katalog_v_2.Models.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Katalog_v_2.Service.FileService
{
    public class AbstractFileService : IModelService
    {

        public XmlSerializer xsSubmit { get; set; }
        public string currentPath { get; set; }
        public String Name { get; set; }

        public void DelElement(int id)
        {
            File.Delete(currentPath + "/" + Name + id + ".txt");
        }

        public void UpdateElement(AModel model)
        {
            StringWriter txtWriter = new StringWriter();
            xsSubmit.Serialize(txtWriter, model);
            File.WriteAllText(currentPath + "/" + Name + model.Id + ".txt", txtWriter.ToString());
            txtWriter.Close();
        }

        public void AddElement(AModel model)
        {
            int max = 0;
            foreach (var path in Directory.GetFiles(currentPath, "*", SearchOption.TopDirectoryOnly))
            {
                Match m = Regex.Match(path, @"" + Name + @"\d+");
                int currentId = Convert.ToInt32(m.Value.Replace(Name, ""));
                if (currentId > max)
                {
                    max = currentId;
                }
            }
            int id = max + 1;
            model.Id = id;
            string newFilePath = currentPath + "/" + Name + id + ".txt";
            StringWriter txtWriter = new StringWriter();
            xsSubmit.Serialize(txtWriter, model);
            File.WriteAllText(newFilePath, txtWriter.ToString());
            txtWriter.Close();
        }

        public AModel GetElement(int id)
        {
            AModel model;
            using (StreamReader stream = new StreamReader(currentPath + "/" + Name + id + ".txt", true))
            {
                model = (AModel)xsSubmit.Deserialize(stream);
                stream.Close();
            }
            return model;
        }

        public List<AModel> GetList()
        {
            string[] filesPaths = Directory.GetFiles(currentPath, "*", SearchOption.TopDirectoryOnly);
            List<AModel> aModels = new List<AModel>();
            foreach (string item in filesPaths)
            {
                StreamReader stream = new StreamReader(item, true);
                AModel model = (AModel)xsSubmit.Deserialize(stream);
                aModels.Add(model);
                stream.Close();
            }
            return aModels;
        }
    }
}