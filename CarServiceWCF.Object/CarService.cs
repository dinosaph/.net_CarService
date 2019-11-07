using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CarService;

namespace CarServiceWCF.Object
{
    public class CarService : ICarService
    {
        //AUTO IMPLEMENTATION
        void InterfaceAuto.AddAuto(Auto giveAuto)
        {
            var autoUtil = new AutoUtils();
            autoUtil.AddAuto(giveAuto);
        }

        List<Auto> InterfaceAuto.GetCarsList()
        {
            var autoUtil = new AutoUtils();
            return autoUtil.GetCarsList();
        }

        Auto InterfaceAuto.GetAuto(int autoIndex)
        {
            var autoUtil = new AutoUtils();
            return autoUtil.GetAuto(autoIndex);
        }

        Auto InterfaceAuto.GetAutoByAutoNumber(string givenNumber)
        {
            var autoUtil = new AutoUtils();
            return autoUtil.GetAutoByAutoNumber(givenNumber);
        }

        void InterfaceAuto.UpdateAuto(Auto givenAuto)
        {
            var autoUtil = new AutoUtils();
            autoUtil.UpdateAuto(givenAuto);
        }

        void InterfaceAuto.RemoveAuto(Auto givenAuto)
        {
            var autoUtil = new AutoUtils();
            autoUtil.RemoveAuto(givenAuto);
        }

        //CLIENT IMPLEMENTATION
        void InterfaceClient.AddClient(Client givenClient)
        {
            var clientUtil = new ClientUtils();
            clientUtil.AddClient(givenClient);
        }

        List<Client> InterfaceClient.GetClientList()
        {
            var clientUtil = new ClientUtils();
            return clientUtil.GetClientsList();
        }

        Client InterfaceClient.GetClient(int clientIndex)
        {
            var clientUtil = new ClientUtils();
            return clientUtil.GetClient(clientIndex);
        }

        List<Client> InterfaceClient.GetClientByLastName(string lastName)
        {
            var clientUtil = new ClientUtils();
            return clientUtil.GetClientsByLastName(lastName);
        }

        List<Client> InterfaceClient.GetClientsByCounty(string givenCounty)
        {
            var clientUtil = new ClientUtils();
            return clientUtil.GetClientsByCounty(givenCounty);
        }

        void InterfaceClient.UpdateClient(Client givenClient)
        {
            var clientUtil = new ClientUtils();
            clientUtil.UpdateClient(givenClient);
        }

        void InterfaceClient.RemoveClient(Client givenClient)
        {
            var clientUtil = new ClientUtils();
            clientUtil.RemoveClient(givenClient);
        }

        //COMMAND IMPLEMENTATION
        void InterfaceComanda.AddCommand(Comanda givenCommand)
        {
            var commandUtil = new ComandaUtils();
            commandUtil.AddCommand(givenCommand);
        }

        List<Comanda> InterfaceComanda.GetCommands()
        {
            var commandUtil = new ComandaUtils();
            return commandUtil.GetCommands();
        }

        Comanda InterfaceComanda.GetCommand(int commandIndex)
        {
            var commandUtil = new ComandaUtils();
            return commandUtil.GetCommand(commandIndex);
        }

        void InterfaceComanda.UpdateCommand(Comanda givenComanda)
        {
            var commandUtil = new ComandaUtils();
            commandUtil.UpdateCommand(givenComanda);
        }

        void InterfaceComanda.RemoveCommand(Comanda givenComanda)
        {
            var commandUtil = new ComandaUtils();
            commandUtil.RemoveCommand(givenComanda);
        }

        //COMMAND DETAIL IMPLEMENTATION
        void InterfaceDetaliuComanda.AddCommandDetail(DetaliuComanda givenDetaliuComanda)
        {
            var detaliuUtil = new DetaliuComandaUtils();
            detaliuUtil.AddCommandDetail(givenDetaliuComanda);
        }

        List<DetaliuComanda> InterfaceDetaliuComanda.GetCommandsDetailsList()
        {
            var detaliuUtil = new DetaliuComandaUtils();
            return detaliuUtil.GetCommandsDetailsList();
        }

        DetaliuComanda InterfaceDetaliuComanda.GetCommandDetail(int commandDetailId)
        {
            var detaliuUtil = new DetaliuComandaUtils();
            return detaliuUtil.GetCommandDetail(commandDetailId);
        }

        void InterfaceDetaliuComanda.RemoveCommandDetail(DetaliuComanda givenDetaliuComanda)
        {
            var detaliuUtil = new DetaliuComandaUtils();
            detaliuUtil.RemoveCommandDetail(givenDetaliuComanda);
        }

        //SASIU IMPLEMENTATION
        void InterfaceSasiu.AddSasiu(Sasiu givenSasiu)
        {
            var sasiuUtil = new SasiuUtils();
            sasiuUtil.AddSasiu(givenSasiu);
        }

        List<Sasiu> InterfaceSasiu.GetSasiuriList()
        {
            var sasiuUtil = new SasiuUtils();
            return sasiuUtil.GetSasiuriList();
        }

        Sasiu InterfaceSasiu.GetSasiu(int sasiuIndex)
        {
            var sasiuUtil = new SasiuUtils();
            return sasiuUtil.GetSasiu(sasiuIndex);
        }

        Sasiu InterfaceSasiu.GetSasiuByCode(string codeSasiu)
        {
            var sasiuUtil = new SasiuUtils();
            return sasiuUtil.GetSasiuByCode(codeSasiu);
        }

        void InterfaceSasiu.UpdateSasiu(Sasiu givenSasiu)
        {
            var sasiuUtil = new SasiuUtils();
            sasiuUtil.UpdateSasiu(givenSasiu);
        }

        void InterfaceSasiu.RemoveSasiu(Sasiu givenSasiu)
        {
            var sasiuUtil = new SasiuUtils();
            sasiuUtil.RemoveSasiu(givenSasiu);
        }
    }
}
