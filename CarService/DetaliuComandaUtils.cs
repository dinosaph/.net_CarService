using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService
{
    public class DetaliuComandaUtils
    {
        //Context
        //public Model1Container1 Context { get; set; }

        //Set context
        public DetaliuComandaUtils()
        {
            //Context = givenContext;
        }

        //ADD command detail
        public void AddCommandDetail(DetaliuComanda givenDetaliuComanda)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenDetaliuComanda != null)
                {
                    Context.DetaliiComenzi.Add(givenDetaliuComanda);
                    Context.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
        }

        //GET full list of command details
        public List<DetaliuComanda> GetCommandsDetailsList()
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                return Context.DetaliiComenzi.Include("Mecanici").Include("Materiale").Include("Imagini").Include("Operatie").ToList();
            }
        }

        //GET single command detail by given index
        public DetaliuComanda GetCommandDetail(int commandDetailId)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                DetaliuComanda focuseDetaliuComanda = null;
                if (commandDetailId > 0)
                {
                    focuseDetaliuComanda = Context.DetaliiComenzi.Include("Mecanici").Include("Materiale").Include("Imagini").Include("Operatie").FirstOrDefault(a => a.Id == commandDetailId);
                }
                else
                {
                    throw new ArgumentException("Invalid command detail ID.");
                }
                return focuseDetaliuComanda;
            }
        }

        //REMOVE given command detail
        public void RemoveCommandDetail(DetaliuComanda givenDetaliuComanda)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenDetaliuComanda != null)
                {
                    Context.DetaliiComenzi.Remove(givenDetaliuComanda);
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
