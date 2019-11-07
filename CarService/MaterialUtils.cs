using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService
{
    class MaterialUtils
    {
        //Context
        //public Model1Container1 Context { get; set; }

        //Set context
        public MaterialUtils()
        {
            //Context = givenContext;
        }

        //ADD material
        public void AddMaterial(Material givenMaterial)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenMaterial != null)
                {
                    Context.Materiale.Add(givenMaterial);
                    Context.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
        }

        //GET full list of materials
        public List<Material> GetMaterialsList()
        {
            using (Model1Container1 Context = new Model1Container1())
            { return Context.Materiale.ToList(); }
        }

        //GET single material by given index
        public Material GetMaterial(int materialIndex)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                Material focusedMaterial = null;
                if (materialIndex > 0)
                {
                    focusedMaterial = Context.Materiale.FirstOrDefault(a => a.Id == materialIndex);
                }
                else
                {
                    throw new ArgumentException("Invalid material ID.");
                }
                return focusedMaterial;
            }
        }

        //UPDATE given material
        public void UpdateMaterial(Material givenMaterial)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenMaterial != null)
                {
                    Material modifiedMaterial = GetMaterial(givenMaterial.Id);

                    //Checking if the material by the given id exists
                    if (modifiedMaterial != null)
                    {
                        modifiedMaterial.Denumire = givenMaterial.Denumire;
                        modifiedMaterial.Cantitate = givenMaterial.Cantitate;
                        modifiedMaterial.Pret = givenMaterial.Pret;
                        modifiedMaterial.DataAprovizionare = givenMaterial.DataAprovizionare;

                        Context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Nothing found at the given ID.");
                    }
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
        }

        //REMOVE given material
        public void RemoveMaterial(Material givenMaterial)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenMaterial != null)
                {
                    Context.Materiale.Remove(givenMaterial);
                    Context.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
        }
    }
}
