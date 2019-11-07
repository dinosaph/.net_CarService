using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CarService;

namespace CarServiceWCF.Object
{
    [ServiceContract]
    public interface InterfaceAuto
    {
        [OperationContract]
        void AddAuto(Auto givenAuto);

        [OperationContract]
        List<Auto> GetCarsList();

        [OperationContract]
        Auto GetAuto(int autoIndex);

        [OperationContract]
        Auto GetAutoByAutoNumber(string givenNumber);

        [OperationContract]
        void UpdateAuto(Auto givenAuto);

        [OperationContract]
        void RemoveAuto(Auto givenAuto);
    }

    [ServiceContract]
    public interface InterfaceClient
    {
        [OperationContract]
        void AddClient(Client givenClient);

        [OperationContract]
        List<Client> GetClientList();

        [OperationContract]
        Client GetClient(int clientIndex);

        [OperationContract]
        List<Client> GetClientByLastName(string lastName);

        [OperationContract]
        List<Client> GetClientsByCounty(string givenCounty);

        [OperationContract]
        void UpdateClient(Client givenClient);

        [OperationContract]
        void RemoveClient(Client givenClient);
    }

    [ServiceContract]
    public interface InterfaceComanda
    {
        [OperationContract]
        void AddCommand(Comanda givenCommand);

        [OperationContract]
        List<Comanda> GetCommands();

        [OperationContract]
        Comanda GetCommand(int commandIndex);

        [OperationContract]
        void UpdateCommand(Comanda givenComanda);

        [OperationContract]
        void RemoveCommand(Comanda givenComanda);
    }

    [ServiceContract]
    public interface InterfaceDetaliuComanda
    {
        [OperationContract]
        void AddCommandDetail(DetaliuComanda givenDetaliuComanda);

        [OperationContract]
        List<DetaliuComanda> GetCommandsDetailsList();

        [OperationContract]
        DetaliuComanda GetCommandDetail(int commandDetailId);

        [OperationContract]
        void RemoveCommandDetail(DetaliuComanda givenDetaliuComanda);
    }

    /*
    [ServiceContract]
    public interface InterfaceImagine
    {
        [OperationContract]
        void AddImage(Imagine givenImagine);

        [OperationContract]
        List<Imagine> GetImageList();

        [OperationContract]
        Imagine GetImagine(int imageIndex);

        [OperationContract]
        void UpdateImage(Imagine givenImagine);

        [OperationContract]
        void RemoveImage(Imagine givenImagine);
    }

    [ServiceContract]
    public interface InterfaceMaterial
    {
        [OperationContract]
        void AddMaterial(Material givenMaterial);

        [OperationContract]
        List<Material> GetMaterialList();

        [OperationContract]
        Material GetMaterial(int materialIndex);

        [OperationContract]
        void UpdateMaterial(Material givenMaterial);

        [OperationContract]
        void RemoveMaterial(Material givenMaterial);
    }

    [ServiceContract]
    public interface InterfaceMecanic
    {
        [OperationContract]
        void AddMechanic(Mecanic givenMecanic);

        [OperationContract]
        List<Mecanic> GetMecanicsList();

        [OperationContract]
        Mecanic GetMecanic(int mecanicIndex);

        [OperationContract]
        void UpdateMechanic(Mecanic givenMecanic);

        [OperationContract]
        void RemoveMechanic(Mecanic givenMecanic);
    }
    */

    /*
    [ServiceContract]
    public interface InterfaceOperatie
    {
        [OperationContract]
        void AddOperation(Operatie giveOperatie);

        [OperationContract]
        List<Operatie> GetOperationsList();

        [OperationContract]
        Operatie GetOperation(int operationIndex);

        [OperationContract]
        void UpdateOperation(Operatie givenOperatie);

        [OperationContract]
        void RemoveOperation(Operatie givenOperatie);
    }*/

    [ServiceContract]
    public interface InterfaceSasiu
    {
        [OperationContract]
        void AddSasiu(Sasiu givenSasiu);

        [OperationContract]
        List<Sasiu> GetSasiuriList();

        [OperationContract]
        Sasiu GetSasiu(int sasiuIndex);

        [OperationContract]
        Sasiu GetSasiuByCode(string codeSasiu);

        [OperationContract]
        void UpdateSasiu(Sasiu givenSasiu);

        [OperationContract]
        void RemoveSasiu(Sasiu givenSasiu);
    }

    [ServiceContract]
    public interface ICarService: InterfaceAuto, InterfaceClient, InterfaceComanda, InterfaceDetaliuComanda, InterfaceSasiu
    { }
}
