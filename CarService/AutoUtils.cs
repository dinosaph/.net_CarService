using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService
{
    public class AutoUtils
    {
        //Context
        //public Model1Container1 Context { get; set; }

        //Context set
        public AutoUtils()
        {
            //Context = givenContext;
        }

        //ADD auto
        public void AddAuto(Auto givenAuto)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenAuto != null)
                {
                    try
                    {
                        // Your code...
                        // Could also be before try if you know the exception occurs in SaveChanges

                        Context.Masini.Add(givenAuto);
                        Context.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }
                }
            }
        }

        //GET all autos
        public List<Auto> GetCarsList()
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                return Context.Masini.Include("Comenzi.DetaliuComenzi.Operatie").Include("Comenzi.DetaliuComenzi.Imagini").Include("Sasiu").ToList();
            }
        }

        //GET single car by given index
        public Auto GetAuto(int autoIndex)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                Auto focusedAuto = null;
                if (autoIndex > 0)
                {
                    focusedAuto = Context.Masini.Include("Comenzi.DetaliuComenzi.Operatie").Include("Comenzi.DetaliuComenzi.Imagini").Include("Sasiu").FirstOrDefault(a => a.Id == autoIndex);
                }
                else
                {
                    throw new ArgumentException("Invalid auto ID.");
                }
                return focusedAuto;
            }
        }

        //GET single car by given number
        public Auto GetAutoByAutoNumber(string givenNumber)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                Auto focusedAuto = null;
                if (givenNumber != null)
                {
                    focusedAuto = Context.Masini.Include("Comenzi.DetaliuComenzi.Operatie").Include("Comenzi.DetaliuComenzi.Imagini").Include("Sasiu").FirstOrDefault(a => a.NumarAuto == givenNumber);
                }
                else
                {
                    throw new ArgumentNullException();
                }
                return focusedAuto;
            }
        }

        //UPDATE given car
        public void UpdateAuto(Auto givenAuto)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenAuto != null)
                {
                    Auto modifiedAuto = GetAuto(givenAuto.Id);

                    //Checking if the auto by the given id exists
                    if (modifiedAuto != null)
                    {
                        modifiedAuto.NumarAuto = givenAuto.NumarAuto;
                        modifiedAuto.SerieSasiu = givenAuto.SerieSasiu;

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

        //REMOVE given auto
        public void RemoveAuto(Auto givenAuto)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenAuto != null)
                {
                    Context.Masini.Attach(givenAuto);
                    Context.Masini.Remove(givenAuto);
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
