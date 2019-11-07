using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService
{
    public class ComandaUtils
    {
        //Context
        //public Model1Container1 Context { get; set; }

        //Set context
        public ComandaUtils()
        {
            //Context = givenContext;
        }

        //ADD command
        public void AddCommand(Comanda givenCommand)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenCommand != null)
                {
                    Context.Comenzi.Add(givenCommand);
                    Context.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
        }

        //GET full list of commands
        public List<Comanda> GetCommands()
        {
            using (Model1Container1 Context = new Model1Container1())
            { return Context.Comenzi.Include("DetaliuComenzi").ToList(); }
        }

        //GET single command by given index
        public Comanda GetCommand(int commandIndex)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                Comanda focusedComanda = null;
                if (commandIndex > 0)
                {
                    focusedComanda = Context.Comenzi.Include("DetaliuComenzi").FirstOrDefault(a => a.Id == commandIndex);
                }
                else
                {
                    throw new ArgumentException("Invalid command ID.");
                }
                return focusedComanda;
            }
        }

        //UPDATE given command
        public void UpdateCommand(Comanda givenComanda)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenComanda != null)
                {
                    Comanda modifiedComanda = GetCommand(givenComanda.Id);

                    //Checking if the command by the given index is null
                    if (modifiedComanda != null)
                    {
                        modifiedComanda.StareComanda = givenComanda.StareComanda;
                        modifiedComanda.DataProgramare = givenComanda.DataProgramare;
                        modifiedComanda.DataFinalizare = givenComanda.DataFinalizare;
                        modifiedComanda.KmBord = givenComanda.KmBord;
                        modifiedComanda.Descriere = givenComanda.Descriere;
                        modifiedComanda.ValoarePiese = givenComanda.ValoarePiese;

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

        //REMOVE given command
        public void RemoveCommand(Comanda givenComanda)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenComanda != null)
                {
                    Context.Comenzi.Remove(givenComanda);
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
