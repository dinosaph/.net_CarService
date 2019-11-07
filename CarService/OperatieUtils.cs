using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService
{
    class OperatieUtils
    {
        //Context
        //public Model1Container1 Context { get; set; }

        //Set context
        public OperatieUtils()
        {
            //Context = givenContext;
        }

        //ADD operation
        public void AddOperation(Operatie givenOperatie)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenOperatie != null)
                {
                    Context.Operatii.Add(givenOperatie);
                    Context.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
        }

        //GET full list of operations
        public List<Operatie> GetOperationsList()
        {
            using (Model1Container1 Context = new Model1Container1())
            { return Context.Operatii.ToList(); }
        }

        //GET sigle operation by given index
        public Operatie GetOperation(int operationIndex)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                Operatie focusedOperatie = null;
                if (operationIndex > 0)
                {
                    focusedOperatie = Context.Operatii.FirstOrDefault(a => a.Id == operationIndex);
                }
                else
                {
                    throw new ArgumentException("Invalid operation ID.");
                }
                return focusedOperatie;
            }
        }

        //UPDATE given operation
        public void UpdateOperation(Operatie givenOperatie)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenOperatie != null)
                {
                    Operatie modifiedOperatie = GetOperation(givenOperatie.Id);

                    //Checking if the operation by the given id exists
                    if (modifiedOperatie != null)
                    {
                        modifiedOperatie.Denumire = givenOperatie.Denumire;
                        modifiedOperatie.TimpExecutie = givenOperatie.TimpExecutie;

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

        //REMOVE given operation
        public void RemoveOperation(Operatie givenOperatie)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenOperatie != null)
                {
                    Context.Operatii.Remove(givenOperatie);
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
