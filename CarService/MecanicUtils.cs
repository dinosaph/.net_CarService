using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService
{
    class MecanicUtils
    {
        //Context
        //public Model1Container1 Context { get; set; }

        //Set context
        public MecanicUtils()
        {
            //Context = givenContext;
        }

        //ADD mechanic
        public void AddMechanic(Mecanic givenMecanic)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenMecanic != null)
                {
                    Context.Mecanici.Add(givenMecanic);
                    Context.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
        }

        //GET full list of mechanics
        public List<Mecanic> GetMecanicsList()
        {
            using (Model1Container1 Context = new Model1Container1())
            { return Context.Mecanici.ToList(); }
        }

        //GET single mechanic by given index
        public Mecanic GetMecanic(int mecanicIndex)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                Mecanic focusedMecanic = null;
                if (mecanicIndex > 0)
                {
                    focusedMecanic = Context.Mecanici.FirstOrDefault(a => a.Id == mecanicIndex);
                }
                else
                {
                    throw new ArgumentException("Invalid mechanic ID.");
                }
                return focusedMecanic;
            }
        }

        //UPDATE given mechanic
        public void UpdateMechanic(Mecanic givenMecanic)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenMecanic != null)
                {
                    Mecanic modifiedMecanic = GetMecanic(givenMecanic.Id);

                    //Checking if the mechanic by the given id exists
                    if (modifiedMecanic != null)
                    {
                        modifiedMecanic.Nume = givenMecanic.Nume;
                        modifiedMecanic.Prenume = givenMecanic.Prenume;
                        modifiedMecanic.DetaliuComandaId = givenMecanic.DetaliuComandaId;

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

        //REMOVE given mechanic
        public void RemoveMechanic(Mecanic givenMecanic)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenMecanic != null)
                {
                    Context.Mecanici.Remove(givenMecanic);
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
