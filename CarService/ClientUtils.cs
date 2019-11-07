using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace CarService
{
    public class ClientUtils
    {
        //Context
        //public Model1Container1 Context { get; set; }

        //Context set
        public ClientUtils()
        {
            //Context = givenContext;
        }

        //ADD client
        public void AddClient(Client givenClient)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenClient != null)
                {
                    Context.Clients.Add(givenClient);
                    Context.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
        }

        //GET full list of clients
        public List<Client> GetClientsList()
        {
            using (Model1Container1 Context = new Model1Container1())
            { return Context.Clients.Include("Masini.Comenzi.DetaliuComenzi.Operatie").Include("Masini.Comenzi.DetaliuComenzi.Imagini").Include("Masini.Sasiu").ToList(); }
        }

        //GET single client by given index
        public Client GetClient(int clientIndex)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                Client focusedClient;
                if (clientIndex > 0)
                {
                    focusedClient = Context.Clients.Include("Masini.Comenzi.DetaliuComenzi.Operatie").Include("Masini.Comenzi.DetaliuComenzi.Imagini").Include("Masini.Sasiu").FirstOrDefault(a => a.Id == clientIndex);
                }
                else
                {
                    throw new ArgumentException("Invalid client ID.");
                }
                return focusedClient;
            }
        }

        //GET clients by last name
        public List<Client> GetClientsByLastName(string lastName)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                List<Client> people;
                if (lastName != null)
                {
                    people = Context.Clients.Where(p => p.Nume == lastName).Include("Masini.Comenzi.DetaliuComenzi.Operatie").Include("Masini.Comenzi.DetaliuComenzi.Imagini").Include("Masini.Sasiu").ToList();
                }
                else
                {
                    throw new ArgumentNullException();
                }
                return people;
            }
        }

        //GET clients by county
        public List<Client> GetClientsByCounty(string givenCounty)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                List<Client> people;
                if (givenCounty != null)
                {
                    people = Context.Clients.Where(p => p.Nume == givenCounty).Include("Masini.Comenzi.DetaliuComenzi.Operatie").Include("Masini.Comenzi.DetaliuComenzi.Imagini").Include("Masini.Sasiu").ToList();
                }
                else
                {
                    throw new ArgumentNullException();
                }
                return people;
            }
        }

        //UPDATE given client
        public void UpdateClient(Client givenClient)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenClient != null)
                {
                    Client modifiedClient = GetClient(givenClient.Id);

                    //Checking if the client by the given id exists
                    if (modifiedClient != null)
                    {
                        modifiedClient.Nume = givenClient.Nume;
                        modifiedClient.Prenume = givenClient.Prenume;
                        modifiedClient.Adresa = givenClient.Adresa;
                        modifiedClient.Localitate = givenClient.Localitate;
                        modifiedClient.Judet = givenClient.Judet;
                        modifiedClient.Telefon = givenClient.Telefon;
                        modifiedClient.Email = givenClient.Email;

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
        
        //REMOVE given client
        public void RemoveClient(Client givenClient)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenClient != null)
                {
                    Context.Clients.Attach(givenClient);
                    Context.Clients.Remove(givenClient);
                    //Context.Entry(givenClient).State = EntityState.Modified;
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
