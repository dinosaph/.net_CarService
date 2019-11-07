using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarService.WinForms
{
    public partial class CarServiceForm : Form
    {/*
        private Model1Container1 _context;

        public Model1Container1 Context
        {
            get
            {
                if (_context == null)
                {
                    _context = new Model1Container1();
                }

                return this._context;
            }
            private set => _context = value;
        }*/

        private int lastClient;
        private List<Client> clientsList = null;
        private List<Auto> clientCarsList = null;
        private List<Comanda> CarCommandsList = null;
        private List<DetaliuComanda> CommandOperations = null;
        private bool itsNewClient = false;
        private bool itsNewCar = false;
        private bool itsNewCommand = false;
        private bool itsNewOperation = false;

        public CarServiceForm()
        {
            InitializeComponent();
            InitializeTabClients();
            InitializeTabAuto();
            InitializeCarCommands();
            InitializeCommandOperations();
            serviceTabs.TabPages.Remove(MechanicsTab);
            serviceTabs.TabPages.Remove(MaterialsTab);
        }

        private void InitializeTabClients()
        {
            clientDataGroupBox.Hide();
            saveClientBtn.Hide();
            clientCancelBtn.Hide();
            deleteClientBtn.Hide();

            clientCarsGroupBox.Hide();
            clientCommandsGroupBox.Hide();

            RefreshClientsList();

            serviceTabs.TabPages.Remove(EditCommandTab);
        }

        private void RefreshClientsList()
        {
            var clientUtils = new ClientUtils();
            List<string> clients = new List<string>();
            clientsList = clientUtils.GetClientsList();
            foreach (var client in clientUtils.GetClientsList())
            {
                clients.Add("(#" + client.Id + ") -- " + client.Nume + " " + client.Prenume + " , " + client.Judet +
                            " , " + client.Localitate + " , " + client.Adresa);
            }

            clientiListBox.DataSource = clients;
        }

        private void RefreshClientCarsList()
        {
            if (clientiListBox.SelectedIndex > -1)
            {
                var clientUtils = new ClientUtils();
                List<string> autos = new List<string>();
                var clientAutos = clientUtils.GetClient(clientsList.ElementAt(clientiListBox.SelectedIndex).Id).Masini
                    .ToList();
                clientCarsList = clientUtils.GetClient(clientsList.ElementAt(clientiListBox.SelectedIndex).Id).Masini
                    .ToList();
                foreach (var auto in clientAutos)
                {
                    autos.Add("(#" + auto.Id + ") -- " + auto.NumarAuto + " , " + auto.SerieSasiu + " [" +
                              auto.Client.Nume + " " + auto.Client.Prenume + "]");
                }

                clientMasiniPreviewListBox.DataSource = autos;
                masiniListBox.DataSource = autos;
            }
        }

        private void ResetClientData()
        {
            numeTxtBox.Clear();
            prenumeTxtBox.Clear();
            adresaTxtBox.Clear();
            locatieTxtBox.Clear();
            judetTxtBox.Clear();
            telefonTxtBox.Clear();
            emailTxtBox.Clear();
        }

        private void LockClientData()
        {
            numeTxtBox.ReadOnly = true;
            prenumeTxtBox.ReadOnly = true;
            adresaTxtBox.ReadOnly = true;
            locatieTxtBox.ReadOnly = true;
            judetTxtBox.ReadOnly = true;
            telefonTxtBox.ReadOnly = true;
            emailTxtBox.ReadOnly = true;
        }

        private void UnlockClientData()
        {
            numeTxtBox.ReadOnly = false;
            prenumeTxtBox.ReadOnly = false;
            adresaTxtBox.ReadOnly = false;
            locatieTxtBox.ReadOnly = false;
            judetTxtBox.ReadOnly = false;
            telefonTxtBox.ReadOnly = false;
            emailTxtBox.ReadOnly = false;
        }

        private void SaveClientBtn_Click(object sender, EventArgs e)
        {
            var clientUtils = new ClientUtils();
            if (itsNewClient)
            {
                clientUtils.AddClient(new Client
                {
                    Nume = numeTxtBox.Text,
                    Prenume = prenumeTxtBox.Text,
                    Adresa = adresaTxtBox.Text,
                    Localitate = locatieTxtBox.Text,
                    Judet = judetTxtBox.Text,
                    Telefon = Convert.ToInt32(telefonTxtBox.Text),
                    Email = emailTxtBox.Text
                });

                clientCarsGroupBox.Show();
                clientCommandsGroupBox.Show();
                itsNewClient = false;
            }
            else
            {
                clientUtils.UpdateClient(new Client
                {
                    Id = Convert.ToInt32(clientsList.ElementAt(clientiListBox.SelectedIndex).Id),
                    Nume = numeTxtBox.Text,
                    Prenume = prenumeTxtBox.Text,
                    Adresa = adresaTxtBox.Text,
                    Localitate = locatieTxtBox.Text,
                    Judet = judetTxtBox.Text,
                    Telefon = Convert.ToInt32(telefonTxtBox.Text),
                    Email = emailTxtBox.Text
                });
            }

            LockClientData();

            clientCancelBtn.Hide();
            saveClientBtn.Hide();
            clientEditBtn.Show();

            RefreshClientsList();
            //ResetClientData();
        }

        private void DeleteClientBtn_Click(object sender, EventArgs e)
        {
            if (clientiListBox.SelectedIndex > -1)
            {
                ResetClientData();
                var clientUtils = new ClientUtils();
                clientUtils.RemoveClient(clientsList.ElementAt(clientiListBox.SelectedIndex));
                clientsList.Remove(clientsList.ElementAt(clientiListBox.SelectedIndex));
                RefreshClientsList();
            }
        }

        private void ClientiListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clientiListBox.SelectedIndex > -1)
            {
                clientDataGroupBox.Show();
                deleteClientBtn.Show();

                var currentClient = clientsList.ElementAt(clientiListBox.SelectedIndex);
                numeTxtBox.Text = currentClient.Nume;
                prenumeTxtBox.Text = currentClient.Prenume;
                adresaTxtBox.Text = currentClient.Adresa;
                locatieTxtBox.Text = currentClient.Localitate;
                judetTxtBox.Text = currentClient.Judet;
                telefonTxtBox.Text = Convert.ToString(currentClient.Telefon);
                emailTxtBox.Text = currentClient.Email;

                clientCarsGroupBox.Show();
                clientCommandsGroupBox.Show();

                RefreshClientCarsList();
                masiniListBox.ClearSelected();
                clientMasiniPreviewListBox.ClearSelected();
            }
            else
            {
                deleteClientBtn.Hide();
                ResetClientData();
                clientDataGroupBox.Hide();
                clientCarsGroupBox.Hide();
                clientCommandsGroupBox.Hide();
            }

            LockClientData();
            clientCancelBtn.Hide();
            saveClientBtn.Hide();
            clientEditBtn.Show();
        }

        private void NewClientBtn_Click(object sender, EventArgs e)
        {
            clientiListBox.ClearSelected();

            clientDataGroupBox.Show();
            clientCancelBtn.Show();
            saveClientBtn.Show();
            clientEditBtn.Hide();

            clientCarsGroupBox.Hide();
            clientCommandsGroupBox.Hide();

            itsNewClient = true;
            ResetClientData();
            UnlockClientData();

        }

        private void ClientEditBtn_Click(object sender, EventArgs e)
        {
            saveClientBtn.Show();
            clientCancelBtn.Show();
            clientEditBtn.Hide();
            UnlockClientData();
        }

        private void ClientCancelBtn_Click(object sender, EventArgs e)
        {
            if (itsNewClient)
            {
                clientiListBox.ClearSelected();
                clientDataGroupBox.Hide();
                clientCarsGroupBox.Hide();
                clientCommandsGroupBox.Hide();
            }
            else
            {
                clientEditBtn.Show();
                saveClientBtn.Hide();
                clientCancelBtn.Hide();
            }

            LockClientData();
        }

        //AUTO TAB CONTROLS

        private void InitializeTabAuto()
        {
            carCommandGroupBox.Hide();
            carDataGroupBox.Hide();
            sasiuDataGroupBox.Hide();

            cancelCarBtn.Hide();
            saveCarBtn.Hide();

            RefreshClientCarsList();
        }

        private void ResetCarData()
        {
            numarAutoTxtBox.Clear();
            denumireSasiuTxtBox.Clear();
            serieSasiuTxtBox.Clear();
            codSasiuTxtBox.Clear();
        }

        private void ShowAutoTabData(int chosenIndexCar)
        {
            if (chosenIndexCar > -1)
            {
                carCommandGroupBox.Show();
                carDataGroupBox.Show();
                sasiuDataGroupBox.Show();

                var currentCar = clientCarsList.ElementAt(chosenIndexCar);
                numarAutoTxtBox.Text = currentCar.NumarAuto;
                serieSasiuTxtBox.Text = currentCar.SerieSasiu;
                codSasiuTxtBox.Text = currentCar.Sasiu.CodSasiu;
                denumireSasiuTxtBox.Text = currentCar.Sasiu.Denumire;

                RefreshCarCommandsList();
            }
            else
            {
                ResetCarData();
                carCommandGroupBox.Hide();
                carDataGroupBox.Hide();
                sasiuDataGroupBox.Hide();
            }

            LockCarAndSasiuData();
            //RefreshClientCarsList();
        }

        private void CarTabBtn_Click(object sender, EventArgs e)
        {
            serviceTabs.SelectedTab = AutoTab;
            ShowAutoTabData(clientMasiniPreviewListBox.SelectedIndex);
        }

        private void RefreshCarCommandsList()
        {
            if (masiniListBox.SelectedIndex > -1)
            {
                List<string> autoCommands = new List<string>();
                CarCommandsList = clientCarsList.ElementAt(masiniListBox.SelectedIndex).Comenzi.ToList();
                foreach (var command in clientCarsList.ElementAt(masiniListBox.SelectedIndex).Comenzi.ToList())
                {
                    autoCommands.Add("(#" + command.Id + ") -- " + command.DataProgramare + " " + command.Descriere);
                }

                masinaComenziListBox.DataSource = autoCommands;
                clientComenziPreviewListBox.DataSource = autoCommands;
                comenziListBox.DataSource = autoCommands;
            }
        }

        private void MasiniListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowAutoTabData(masiniListBox.SelectedIndex);
        }

        private void LockCarAndSasiuData()
        {
            numarAutoTxtBox.ReadOnly = true;
            serieSasiuTxtBox.ReadOnly = true;

            codSasiuTxtBox.ReadOnly = true;
            denumireSasiuTxtBox.ReadOnly = true;
        }

        private void UnlockCarData()
        {
            numarAutoTxtBox.ReadOnly = false;
            serieSasiuTxtBox.ReadOnly = false;
        }

        private void NewCarBtn_Click(object sender, EventArgs e)
        {
            masiniListBox.ClearSelected();

            carCommandGroupBox.Hide();
            carDataGroupBox.Show();
            sasiuDataGroupBox.Show();

            editCarBtn.Hide();
            saveCarBtn.Show();
            cancelCarBtn.Show();

            itsNewCar = true;
            ResetCarData();
            UnlockCarData();
        }

        private List<string> GeneratedSasiu()
        {
            var sasiuUtils = new SasiuUtils();
            List<string> sasiuData = new List<string>();

            string sasiuDenumire;

            string inputCode = serieSasiuTxtBox.Text;
            string sasiuCode = inputCode.Substring(6, 2);

            var foundSasiu = sasiuUtils.GetSasiuByCode(sasiuCode);

            if (foundSasiu == null)
            {
                if (sasiuCode == "4F")
                {
                    sasiuDenumire = "Audi";
                }
                else
                {
                    sasiuDenumire = "default";
                }

                sasiuData.Add(sasiuCode);
                sasiuData.Add(sasiuDenumire);
            }
            else
            {
                sasiuData.Add(foundSasiu.CodSasiu);
                sasiuData.Add(foundSasiu.Denumire);
            }

            return sasiuData;
        }

        private void SaveCarBtn_Click(object sender, EventArgs e)
        {
            var carUtils = new AutoUtils();
            if (itsNewCar)
            {
                itsNewCar = false;

                var sasiuUtils = new SasiuUtils();

                string sasiuDenumire = "default";
                string inputCode = serieSasiuTxtBox.Text;
                string sasiuCode = inputCode.Substring(6, 2);

                var foundSasiu = sasiuUtils.GetSasiuByCode(sasiuCode);

                if (foundSasiu == null)
                {
                    carUtils.AddAuto(new Auto
                    {
                        NumarAuto = numarAutoTxtBox.Text,
                        SerieSasiu = serieSasiuTxtBox.Text,
                        Client = clientsList.ElementAt(clientiListBox.SelectedIndex),
                        Sasiu = new Sasiu
                        {
                            CodSasiu = sasiuCode,
                            Denumire = sasiuDenumire
                        }
                    });
                }
                else
                {
                    carUtils.AddAuto(new Auto
                    {
                        NumarAuto = numarAutoTxtBox.Text,
                        SerieSasiu = serieSasiuTxtBox.Text,
                        Client = clientsList.ElementAt(clientiListBox.SelectedIndex),
                        Sasiu = foundSasiu
                    });
                }
            }
            else
            {
                carUtils.UpdateAuto(new Auto
                {
                    Id = clientCarsList.ElementAt(masiniListBox.SelectedIndex).Id,
                    NumarAuto = numarAutoTxtBox.Text,
                    SerieSasiu = serieSasiuTxtBox.Text
                });
            }

            cancelCarBtn.Hide();
            saveCarBtn.Hide();
            editCarBtn.Show();

            RefreshClientCarsList();
            LockCarAndSasiuData();
        }

        private void DeleteCarBtn_Click(object sender, EventArgs e)
        {
            if (masiniListBox.SelectedIndex > -1)
            {
                var carUtils = new AutoUtils();
                carUtils.RemoveAuto(clientCarsList.ElementAt(masiniListBox.SelectedIndex));
                clientCarsList.Remove(clientCarsList.ElementAt(masiniListBox.SelectedIndex));
                RefreshClientCarsList();
                RefreshCarCommandsList();
                ResetCarData();
            }
        }

        private void CancelCarBtn_Click(object sender, EventArgs e)
        {
            if (itsNewCar)
            {
                masiniListBox.ClearSelected();
                carDataGroupBox.Hide();
                sasiuDataGroupBox.Hide();
                carCommandGroupBox.Hide();
                itsNewCar = false;
            }
            else
            {
                //ResetCarData();
                editCarBtn.Show();
                saveCarBtn.Hide();
                cancelCarBtn.Hide();
            }

            LockCarAndSasiuData();
        }

        private void CarNewCommandBtn_Click(object sender, EventArgs e)
        {
            serviceTabs.SelectedTab = CommandsTab;
            NewCommandBtn_Click(sender, e);
        }

        private void CarToCommandBtn_Click(object sender, EventArgs e)
        {
            serviceTabs.SelectedTab = CommandsTab;
            ShowCommandsTabData(masinaComenziListBox.SelectedIndex);
        }

        private void EditCarBtn_Click(object sender, EventArgs e)
        {
            editCarBtn.Hide();
            cancelCarBtn.Show();
            saveCarBtn.Show();
            UnlockCarData();
        }

        //COMMANDS TAB
        private void InitializeCarCommands()
        {
            stareComandaCBox.Items.Add("In asteptare");
            stareComandaCBox.Items.Add("Executata");
            stareComandaCBox.Items.Add("Refuzata la executie");
            deleteCommandBtn.Hide();
            cancelCommandBtn.Hide();
            saveCommandBtn.Hide();

            commandDataGroupBox.Hide();
            commandOtherInfoGroupBox.Hide();

            valoarePieseTxtBox.ReadOnly = true;
        }

        private void ResetCommandData()
        {
            stareComandaCBox.Text = "Default";
            programareDateBox.Text = Convert.ToString(System.DateTime.Today);
            finalizareDateBox.Text = Convert.ToString(System.DateTime.Today);
            kmBordTxtBox.Clear();
            descriereComandaTxtBox.Clear();
            //valoarePieseTxtBox.Clear();
        }
        
        private void EditCommandBtn_Click(object sender, EventArgs e)
        {
            if (comenziListBox.SelectedIndex > -1)
            {
                editCommandBtn.Hide();
                UnlockCommandData();
                cancelCommandBtn.Show();
                saveCommandBtn.Show();
            }
        }

        private void LockCommandData()
        {
            stareComandaCBox.AllowDrop = false;
            programareDateBox.AllowDrop = false;
            finalizareDateBox.AllowDrop = false;
            kmBordTxtBox.ReadOnly = true;
            descriereComandaTxtBox.ReadOnly = true;
            valoarePieseTxtBox.ReadOnly = true;
        }

        private void UnlockCommandData()
        {
            stareComandaCBox.AllowDrop = true;
            programareDateBox.AllowDrop = true;
            finalizareDateBox.AllowDrop = true;
            kmBordTxtBox.ReadOnly = false;
            descriereComandaTxtBox.ReadOnly = false;
        }

        private void ShowCommandsTabData(int chosenIndexCommand)
        {
            if (chosenIndexCommand > -1)
            {
                deleteCommandBtn.Show();
                commandDataGroupBox.Show();
                commandOtherInfoGroupBox.Show();

                var currentCommand = CarCommandsList.ElementAt(chosenIndexCommand);

                stareComandaCBox.Text = currentCommand.StareComanda;
                programareDateBox.Text = Convert.ToString(currentCommand.DataProgramare);
                finalizareDateBox.Text = Convert.ToString(currentCommand.DataFinalizare);
                kmBordTxtBox.Text = Convert.ToString(currentCommand.KmBord);
                descriereComandaTxtBox.Text = currentCommand.Descriere;
                valoarePieseTxtBox.Text = Convert.ToString(currentCommand.ValoarePiese);

                RefreshCarCommandsList();
            }
            else
            {
                deleteCommandBtn.Hide();
                ResetCommandData();
                commandDataGroupBox.Hide();
                commandOtherInfoGroupBox.Hide();
            }

            LockCommandData();
            //RefreshClientCarsList();
        }

        private void CommandsTabBtn_Click(object sender, EventArgs e)
        {
            serviceTabs.SelectedTab = CommandsTab;
            ShowCommandsTabData(clientComenziPreviewListBox.SelectedIndex);
        }

        private void NewCommandBtn_Click(object sender, EventArgs e)
        {
            commandDataGroupBox.Show();

            saveCommandBtn.Show();
            cancelCommandBtn.Show();
            editCommandBtn.Hide();

            ResetCommandData();
            UnlockCommandData();
            itsNewCommand = true;
        }

        private void SaveCommandBtn_Click(object sender, EventArgs e)
        {
            var commandUtils = new ComandaUtils();
            if (itsNewCommand)
            {
                stareComandaCBox.SelectedIndex = 0;

                commandUtils.AddCommand(new Comanda
                {
                    StareComanda = stareComandaCBox.Text,
                    DataProgramare = programareDateBox.Value,
                    DataFinalizare = finalizareDateBox.Value,
                    DataSystem = DateTime.Now,
                    Descriere = descriereComandaTxtBox.Text,
                    ValoarePiese = Convert.ToDecimal(valoarePieseTxtBox.Text),
                    KmBord = Convert.ToInt32(kmBordTxtBox.Text),
                    Auto = clientCarsList.ElementAt(masiniListBox.SelectedIndex)
                });
                itsNewCommand = false;
            }
            else
            {
                commandUtils.UpdateCommand(new Comanda
                {
                    Id = CarCommandsList.ElementAt(comenziListBox.SelectedIndex).Id,
                    StareComanda = stareComandaCBox.Text,
                    DataProgramare = programareDateBox.Value,
                    DataFinalizare = finalizareDateBox.Value,
                    Descriere = descriereComandaTxtBox.Text,
                    ValoarePiese = Convert.ToDecimal(valoarePieseTxtBox.Text),
                    KmBord = Convert.ToInt32(kmBordTxtBox.Text)
                });
            }
            saveCommandBtn.Hide();
            cancelCommandBtn.Hide();
            editCommandBtn.Show();
            RefreshCarCommandsList();
        }

        private void CancelCommandBtn_Click(object sender, EventArgs e)
        {
            if (itsNewCommand)
            {
                comenziListBox.ClearSelected();
                commandDataGroupBox.Hide();
                commandOtherInfoGroupBox.Hide();
                itsNewCommand = false;
            }
            else
            {
                //ResetCommandData();
                editCommandBtn.Show();
                saveCommandBtn.Hide();
                cancelCommandBtn.Hide();
            }
            LockCommandData();
        }

        private void ComenziListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowCommandsTabData(comenziListBox.SelectedIndex);
            RefreshCommandOperations();
        }

        private void DeleteCommandBtn_Click(object sender, EventArgs e)
        {
            if (comenziListBox.SelectedIndex > -1)
            {
                ResetCommandData();
                var commandUtils = new ComandaUtils();
                commandUtils.RemoveCommand(CarCommandsList.ElementAt(comenziListBox.SelectedIndex));
                CarCommandsList.Remove(CarCommandsList.ElementAt(comenziListBox.SelectedIndex));
                RefreshCarCommandsList();
            }
        }

        //OPERATIONS TAB
        private void InitializeCommandOperations()
        {   
            operationDetailsGroupBox.Hide();
            //LockCommandOperationsData();

            RefreshCommandOperations();
        }

        private void RefreshCommandOperations()
        {
            if (comenziListBox.SelectedIndex > -1)
            {
                List<string> commandOperations = new List<string>();
                CommandOperations = CarCommandsList.ElementAt(comenziListBox.SelectedIndex).DetaliuComenzi.ToList();
                foreach (var detaliu in CarCommandsList.ElementAt(comenziListBox.SelectedIndex).DetaliuComenzi.ToList())
                {
                    commandOperations.Add("(#" + detaliu.Id + ") -- " + detaliu.Operatie.Denumire + " , " +
                                          detaliu.Operatie.TimpExecutie);
                }

                operatiuniListBox.DataSource = commandOperations;
                operatiuniTxtBox.Text = String.Join("| ", commandOperations.ToArray());
            }
        }

        private void LockCommandOperationsData()
        {
            //clientOperatiuneTxtBox.ReadOnly = true;
            //comandaOperatiuneTxtBox.ReadOnly = true;
            //denumireOperatieTxtBox.ReadOnly = true;
            timpExecutieOperatieTxtBox.ReadOnly = true;
            mecaniciOperatiuniTxtBox.ReadOnly = true;
        }

        private void ShowCommandOperationsTabData()
        {
            if (operatiuniListBox.SelectedIndex > -1)
            {
                operationDetailsGroupBox.Show();

                var currentOperatiune = CommandOperations.ElementAt(operatiuniListBox.SelectedIndex);
                comandaOperatiuneTxtBox.Text = CarCommandsList.ElementAt(comenziListBox.SelectedIndex).Descriere;
                numeOperatiuneTxtBox.Text = currentOperatiune.Operatie.Denumire;
                timpExecutieOperatiuneTxtBox.Text = Convert.ToString(currentOperatiune.Operatie.TimpExecutie);

                var mecaniciNames = "";
                foreach (var mecanic in currentOperatiune.Mecanici)
                {
                    mecaniciNames += mecanic.Nume + " " + mecanic.Prenume + " | ";
                }

                var materiale = "";
                foreach (var material in currentOperatiune.Materiale)
                {
                    materiale += material.Denumire + " " + material.Cantitate + " buc (" + material.Pret + " lei) |";
                }

                mecaniciOperatiuniTxtBox.Text = mecaniciNames;
                materialeOperatiuneTxtBox.Text = materiale;

                poza.Show();
                poza.Image = Image.FromStream(new MemoryStream(currentOperatiune.Imagini.ElementAt(0).Foto));
            }
            else
            {
                ResetOperationData();
            }
        }

        private void ResetOperationData()
        {
            operationDetailsGroupBox.Hide();
            mecaniciOperatiuniTxtBox.Clear();
            materialeOperatiuneTxtBox.Clear();
            timpExecutieOperatieTxtBox.Clear();
            comandaOperatiuneTxtBox.Clear();
            numeOperatiuneTxtBox.Clear();
            poza.Hide();
        }

        private void OperatiuniListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowCommandOperationsTabData();
        }


        private void OperationCommandDeleteBtn_Click(object sender, EventArgs e)
        {
            if (operatiuniListBox.SelectedIndex > -1)
            {
                var detaliuUtils = new DetaliuComandaUtils();
                detaliuUtils.RemoveCommandDetail(CommandOperations.ElementAt(operatiuniListBox.SelectedIndex));
                CommandOperations.Remove(CommandOperations.ElementAt(operatiuniListBox.SelectedIndex));
                RefreshCommandOperations();
            }
        }

        private void NewOperationBtn_Click(object sender, EventArgs e)
        {
            serviceTabs.TabPages.Add(EditCommandTab);
            serviceTabs.SelectedTab = EditCommandTab;
            itsNewOperation = true;
        }

        private void SaveOperationBtn_Click(object sender, EventArgs e)
        {
            var detaliuUtils = new DetaliuComandaUtils();
            if (itsNewOperation)
            {
                var mecanici = new List<Mecanic>();
                var materiale = new List<Material>();

                var parsedMecanici = comandaEditMecaniciTxtBox.Text.Split(',').ToList<string>();
                foreach (var mecanic in parsedMecanici)
                {
                    var numePrenume = mecanic.Split(' ').ToList();
                    mecanici.Add(new Mecanic
                    {
                        Nume = numePrenume[0],
                        Prenume = numePrenume[1]
                    });
                }

                var parsedMateriale = comandaEditMaterialeTxtBox.Text.Split('-').ToList();
                foreach (var material in parsedMateriale)
                {
                    var numeCantitatePret = material.Split(',').ToList();
                    materiale.Add(new Material
                    {
                        Denumire = numeCantitatePret[0],
                        Cantitate = Convert.ToDecimal(numeCantitatePret[1]),
                        Pret = Convert.ToDecimal(numeCantitatePret[2]),
                        DataAprovizionare = aprovizionareDateBox.Value
                    });
                }

                detaliuUtils.AddCommandDetail(new DetaliuComanda
                {
                    Operatie = new Operatie
                    {
                        Denumire = denumireOperatieTxtBox.Text,
                        TimpExecutie = Convert.ToDecimal(timpExecutieOperatieTxtBox.Text)
                    },
                    Comanda = CarCommandsList.ElementAt(comenziListBox.SelectedIndex),
                    Mecanici = mecanici,
                    Materiale = materiale,
                    Imagini = new List<Imagine>
                    {
                        new Imagine
                        {
                            Titlu = titluImagineTxtBox.Text,
                            Descriere = descriereImagineTxtBox.Text,
                            Data = dataImagineDateBox.Value,
                            Foto = File.ReadAllBytes(imagineTxtBox.Text)
                        }
                    }
                });
            }
            RefreshCommandOperations();
            serviceTabs.SelectedTab = OperationsTab;
        }

        private void operationsTabBtn_Click(object sender, EventArgs e)
        {
            serviceTabs.SelectedTab = OperationsTab;
        }

        private void CancelOperationBtn_Click(object sender, EventArgs e)
        {
            serviceTabs.SelectedTab = OperationsTab;
            RefreshCommandOperations();
            ResetOperationData();
        }

        //MISC TAB

        private void ServiceTabs_Selected(object sender, TabControlEventArgs e)
        {
            if (serviceTabs.SelectedTab == AutoTab)
            {
                CarTabBtn_Click(sender, e);
            }

            if (serviceTabs.SelectedTab == CommandsTab)
            {
                CommandsTabBtn_Click(sender, e);
            }

            if (serviceTabs.SelectedTab != EditCommandTab)
            {
                serviceTabs.TabPages.Remove(EditCommandTab);
            }
        }

    }
}
