using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService
{
    public class SasiuUtils
    {
        //Context
        //public Model1Container1 Context { get; set; }

        //Set Context
        public SasiuUtils()
        {
            //Context = givenContext;
        }

        //ADD sasiu
        public void AddSasiu(Sasiu givenSasiu)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenSasiu != null)
                {
                    Context.Sasiuri.Add(givenSasiu);
                    Context.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
        }

        //GET full list of sasiuri
        public List<Sasiu> GetSasiuriList()
        {
            using (Model1Container1 Context = new Model1Container1())
            { return Context.Sasiuri.ToList(); }
        }

        //GET single sasiu by given index
        public Sasiu GetSasiu(int sasiuIndex)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                Sasiu focusedSasiu = null;
                if (sasiuIndex > 0)
                {
                    focusedSasiu = Context.Sasiuri.FirstOrDefault(a => a.Id == sasiuIndex);
                }
                else
                {
                    throw new ArgumentException("Invalid sasiu ID.");
                }
                return focusedSasiu;
            }
        }

        //GET sasiu by given code
        public Sasiu GetSasiuByCode(string codeSasiu)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                Sasiu foundSasiu;
                if (codeSasiu != null)
                {
                    foundSasiu = Context.Sasiuri.FirstOrDefault(sasiu => sasiu.CodSasiu == codeSasiu);
                }
                else
                {
                    throw new ArgumentNullException();
                }
                return foundSasiu;
            }
        }

        //UPDATE given sasiu
        public void UpdateSasiu(Sasiu givenSasiu)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenSasiu != null)
                {
                    Sasiu modifiedSasiu = GetSasiu(givenSasiu.Id);

                    //Checking if the sasiu by the given id exists
                    if (modifiedSasiu != null)
                    {
                        modifiedSasiu.CodSasiu = givenSasiu.CodSasiu;
                        modifiedSasiu.Denumire = givenSasiu.Denumire;

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

        //REMOVE given sasiu
        public void RemoveSasiu(Sasiu givenSasiu)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenSasiu != null)
                {
                    Context.Sasiuri.Remove(givenSasiu);
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
