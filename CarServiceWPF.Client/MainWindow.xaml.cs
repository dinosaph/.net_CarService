using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Serialization;
using CarService;

namespace CarServiceWPF.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private int lastClient;
        private List<CarService.Client> clientsList = null;
        private List<Auto> clientCarsList = null;
        private List<Comanda> CarCommandsList = null;
        private List<DetaliuComanda> CommandOperations = null;
        private bool itsNewClient = false;
        private bool itsNewCar = false;
        private bool itsNewCommand = false;
        private bool itsNewOperation = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTabClients();
            InitializeTabAuto();
            InitializeCarCommands();
            InitializeCommandOperations();
        }

        private void InitializeTabClients()
        {
            clientDataGroupBox.Visibility = Visibility.Hidden;
            saveClientBtn.Visibility = Visibility.Hidden;
            clientCancelBtn.Visibility = Visibility.Hidden;
            deleteClientBtn.Visibility = Visibility.Hidden;

            clientCarsGroupBox.Visibility = Visibility.Hidden;
            clientCommandsGroupBox.Visibility = Visibility.Hidden;

            RefreshClientsList();
        }
        
        private void RefreshClientsList()
        {
            var clientUtils = new CarServiceClient();
            List<string> clients = new List<string>();
            clientsList = clientUtils.GetClientList().ToList();
            foreach (var client in clientUtils.GetClientList())
            {
                clients.Add("(#" + client.Id + ") -- " + client.Nume + " " + client.Prenume + " , " + client.Judet +
                            " , " + client.Localitate + " , " + client.Adresa);
            }

            clientiListBox.ItemsSource = clients;
        }

        private void RefreshClientCarsList()
        {
            if (clientiListBox.SelectedIndex > -1)
            {
                var clientUtils = new CarServiceClient();
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

                clientMasiniPreviewListBox.ItemsSource = autos;
                masiniListBox.ItemsSource = autos;
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
            numeTxtBox.IsReadOnly = true;
            prenumeTxtBox.IsReadOnly = true;
            adresaTxtBox.IsReadOnly = true;
            locatieTxtBox.IsReadOnly = true;
            judetTxtBox.IsReadOnly = true;
            telefonTxtBox.IsReadOnly = true;
            emailTxtBox.IsReadOnly = true;
        }
        
        private void UnlockClientData()
        {
            numeTxtBox.IsReadOnly = false;
            prenumeTxtBox.IsReadOnly = false;
            adresaTxtBox.IsReadOnly = false;
            locatieTxtBox.IsReadOnly = false;
            judetTxtBox.IsReadOnly = false;
            telefonTxtBox.IsReadOnly = false;
            emailTxtBox.IsReadOnly = false;
        }

        private void SaveClientBtn_Click(object sender, RoutedEventArgs e)
        {
            var clientUtils = new CarServiceClient();
            if (itsNewClient)
            {
                clientUtils.AddClient(new CarService.Client()
                {
                    Nume = numeTxtBox.Text,
                    Prenume = prenumeTxtBox.Text,
                    Adresa = adresaTxtBox.Text,
                    Localitate = locatieTxtBox.Text,
                    Judet = judetTxtBox.Text,
                    Telefon = Convert.ToInt32(telefonTxtBox.Text),
                    Email = emailTxtBox.Text
                });


                clientCarsGroupBox.Visibility = Visibility.Visible;
                clientCommandsGroupBox.Visibility = Visibility.Visible;

                itsNewClient = false;
            }
            else
            {
                clientUtils.UpdateClient(new CarService.Client
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
            
            clientCancelBtn.Visibility = Visibility.Hidden;
            saveClientBtn.Visibility = Visibility.Hidden;
            clientEditBtn.Visibility = Visibility.Visible;
            
            LockClientData();
            RefreshClientsList();
            //ResetClientData();
        }

        private void DeleteClientBtn_Click(object sender, RoutedEventArgs e)
        {
            if (clientiListBox.SelectedIndex > -1)
            {
                ResetClientData();
                var clientUtils = new CarServiceClient();
                clientUtils.RemoveClient(clientsList.ElementAt(clientiListBox.SelectedIndex));
                clientsList.Remove(clientsList.ElementAt(clientiListBox.SelectedIndex));
                RefreshClientsList();
            }
        }

        private void ClientiListBox_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            if (clientiListBox.SelectedIndex > -1)
            {
                clientDataGroupBox.Visibility = Visibility.Visible;
                deleteClientBtn.Visibility = Visibility.Visible;

                var currentClient = clientsList.ElementAt(clientiListBox.SelectedIndex);
                numeTxtBox.Text = currentClient.Nume;
                prenumeTxtBox.Text = currentClient.Prenume;
                adresaTxtBox.Text = currentClient.Adresa;
                locatieTxtBox.Text = currentClient.Localitate;
                judetTxtBox.Text = currentClient.Judet;
                telefonTxtBox.Text = Convert.ToString(currentClient.Telefon);
                emailTxtBox.Text = currentClient.Email;

                clientCarsGroupBox.Visibility = Visibility.Visible;
                clientCommandsGroupBox.Visibility = Visibility.Visible;

                RefreshClientCarsList();
                
                masiniListBox.UnselectAll();
                clientMasiniPreviewListBox.UnselectAll();
            }
            else
            {
                deleteClientBtn.Visibility = Visibility.Hidden;
                ResetClientData();
                clientDataGroupBox.Visibility = Visibility.Hidden;
                clientCarsGroupBox.Visibility = Visibility.Hidden;
                clientCommandsGroupBox.Visibility = Visibility.Hidden;
            }

            LockClientData();
            
            clientCancelBtn.Visibility = Visibility.Hidden;
            saveClientBtn.Visibility = Visibility.Hidden;
            clientEditBtn.Visibility = Visibility.Visible;
        }

        private void NewClientBtn_Click(object sender, RoutedEventArgs e)
        {
            clientiListBox.UnselectAll();

            clientDataGroupBox.Visibility = Visibility.Visible;
            clientCancelBtn.Visibility = Visibility.Visible;
            saveClientBtn.Visibility = Visibility.Visible;
            clientEditBtn.Visibility = Visibility.Hidden;

            clientCarsGroupBox.Visibility = Visibility.Hidden;
            clientCommandsGroupBox.Visibility = Visibility.Hidden;

            itsNewClient = true;
            ResetClientData();
            UnlockClientData();
        }

        private void ClientEditBtn_Click(object sender, RoutedEventArgs e)
        {
            saveClientBtn.Visibility = Visibility.Visible;
            clientCancelBtn.Visibility = Visibility.Visible;
            clientEditBtn.Visibility = Visibility.Hidden;

            UnlockClientData();
        }
        
        private void ClientCancelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (itsNewClient)
            {
                clientiListBox.UnselectAll();
                clientDataGroupBox.Visibility = Visibility.Hidden;
                clientCarsGroupBox.Visibility = Visibility.Hidden;
                clientCommandsGroupBox.Visibility = Visibility.Hidden;
            }
            else
            {
                clientEditBtn.Visibility = Visibility.Visible;
                saveClientBtn.Visibility = Visibility.Hidden;
                clientCancelBtn.Visibility = Visibility.Hidden;
            }

            LockClientData();
        }

        //AUTO TAB CONTROLS
        private void InitializeTabAuto()
        {
            carCommandGroupBox.Visibility = Visibility.Hidden;
            carDataGroupBox.Visibility = Visibility.Hidden;
            sasiuDataGroupBox.Visibility = Visibility.Hidden;

            cancelCarBtn.Visibility = Visibility.Hidden;
            saveCarBtn.Visibility = Visibility.Hidden;

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
                carCommandGroupBox.Visibility = Visibility.Visible;
                carDataGroupBox.Visibility = Visibility.Visible;
                sasiuDataGroupBox.Visibility = Visibility.Visible;

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
                carCommandGroupBox.Visibility = Visibility.Hidden;
                carDataGroupBox.Visibility = Visibility.Hidden;
                sasiuDataGroupBox.Visibility = Visibility.Hidden;
            }

            LockCarAndSasiuData();
            RefreshClientCarsList();
        }

        private void CarTabBtn_Click(object sender, RoutedEventArgs e)
        {
            serviceTabs.SelectedIndex = 1;
            serviceTabs.SelectedItem = AutoTab;
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

                masinaComenziListBox.ItemsSource = autoCommands;
                clientComenziPreviewListBox.ItemsSource = autoCommands;
                comenziListBox.ItemsSource = autoCommands;
            }
        }

        private void MasiniListBox_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowAutoTabData(masiniListBox.SelectedIndex);
        }

        private void LockCarAndSasiuData()
        {
            numarAutoTxtBox.IsReadOnly = true;
            serieSasiuTxtBox.IsReadOnly = true;

            codSasiuTxtBox.IsReadOnly = true;
            denumireSasiuTxtBox.IsReadOnly = true;
        }

        private void UnlockCarData()
        {
            numarAutoTxtBox.IsReadOnly = false;
            serieSasiuTxtBox.IsReadOnly = false;
        }

        private void NewCarBtn_Click(object sender, RoutedEventArgs e)
        {
            masiniListBox.UnselectAll();
            
            carCommandGroupBox.Visibility = Visibility.Hidden;
            carDataGroupBox.Visibility = Visibility.Visible;
            sasiuDataGroupBox.Visibility = Visibility.Visible;

            editCarBtn.Visibility = Visibility.Hidden;
            saveCarBtn.Visibility = Visibility.Visible;
            cancelCarBtn.Visibility = Visibility.Visible;

            itsNewCar = true;
            ResetCarData();
            UnlockCarData();
        }

        private List<string> GeneratedSasiu()
        {
            var sasiuUtils = new CarServiceClient();
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

        private void SaveCarBtn_Click(object sender, RoutedEventArgs e)
        {
            var carUtils = new CarServiceClient();
            if (itsNewCar)
            {
                itsNewCar = false;

                var sasiuUtils = new CarServiceClient();

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

            cancelCarBtn.Visibility = Visibility.Hidden;
            saveCarBtn.Visibility = Visibility.Hidden;
            editCarBtn.Visibility = Visibility.Visible;

            RefreshClientCarsList();
            LockCarAndSasiuData();
        }

        private void DeleteCarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (masiniListBox.SelectedIndex > -1)
            {
                var carUtils = new CarServiceClient();
                carUtils.RemoveAuto(clientCarsList.ElementAt(masiniListBox.SelectedIndex));
                clientCarsList.Remove(clientCarsList.ElementAt(masiniListBox.SelectedIndex));
                RefreshClientCarsList();
                RefreshCarCommandsList();
                ResetCarData();
            }
        }

        private void CancelCarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (itsNewCar)
            {
                masiniListBox.UnselectAll();
                
                carDataGroupBox.Visibility = Visibility.Hidden;
                sasiuDataGroupBox.Visibility = Visibility.Hidden;
                carCommandGroupBox.Visibility = Visibility.Hidden;
                itsNewCar = false;
            }
            else
            {
                //ResetCarData();//
                editCarBtn.Visibility = Visibility.Visible;
                saveCarBtn.Visibility = Visibility.Hidden;
                cancelCarBtn.Visibility = Visibility.Hidden;
            }

            LockCarAndSasiuData();
        }

        private void CarNewCommandBtn_Click(object sender, RoutedEventArgs e)
        {
            serviceTabs.SelectedItem = CommandsTab;
            NewCommandBtn_Click(sender, e);
        }

        private void CarToCommandBtn_Click(object sender, RoutedEventArgs e)
        {
            serviceTabs.SelectedItem = CommandsTab;
            ShowCommandsTabData(masinaComenziListBox.SelectedIndex);
        }

        private void EditCarBtn_Click(object sender, RoutedEventArgs e)
        {
            editCarBtn.Visibility = Visibility.Hidden;
            cancelCarBtn.Visibility = Visibility.Visible;
            saveCarBtn.Visibility = Visibility.Visible;
            UnlockCarData();
        }

        //COMMANDS TAB
        private void InitializeCarCommands()
        {
            stareComandaCBox.Items.Add("In asteptare");
            stareComandaCBox.Items.Add("Executata");
            stareComandaCBox.Items.Add("Refuzata la executie");
            deleteCommandBtn.Visibility = Visibility.Hidden;
            cancelCommandBtn.Visibility = Visibility.Hidden;
            saveCommandBtn.Visibility = Visibility.Hidden;

            commandDataGroupBox.Visibility = Visibility.Hidden;
            commandOtherInfoGroupBox.Visibility = Visibility.Hidden;

            valoarePieseTxtBox.IsReadOnly = true;
        }

        private void ResetCommandData()
        {
            stareComandaCBox.Text = "Default";
            programareDateBox.Text = Convert.ToString(System.DateTime.Today);
            finalizareDateBox.Text = Convert.ToString(System.DateTime.Today);
            kmBordTxtBox.Clear();
            descriereComandaTxtBox.Clear();
            valoarePieseTxtBox.Clear();
        }

        private void EditCommandBtn_Click(object sender, RoutedEventArgs e)
        {
            if (comenziListBox.SelectedIndex > -1)
            {
                editCommandBtn.Visibility = Visibility.Hidden;
                UnlockCommandData();
                cancelCommandBtn.Visibility = Visibility.Visible;
                saveCommandBtn.Visibility = Visibility.Visible;
            }
        }

        private void LockCommandData()
        {
            stareComandaCBox.AllowDrop = false;
            programareDateBox.AllowDrop = false;
            finalizareDateBox.AllowDrop = false;
            kmBordTxtBox.IsReadOnly = true;
            descriereComandaTxtBox.IsReadOnly = true;
            valoarePieseTxtBox.IsReadOnly = true;
        }

        private void UnlockCommandData()
        {
            stareComandaCBox.AllowDrop = true;
            programareDateBox.AllowDrop = true;
            finalizareDateBox.AllowDrop = true;
            kmBordTxtBox.IsReadOnly = false;
            descriereComandaTxtBox.IsReadOnly = false;
        }

        private void ShowCommandsTabData(int chosenIndexCommand)
        {
            if (chosenIndexCommand > -1)
            {
                deleteCommandBtn.Visibility = Visibility.Visible;
                commandDataGroupBox.Visibility = Visibility.Visible;
                commandOtherInfoGroupBox.Visibility = Visibility.Visible;

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
                deleteCommandBtn.Visibility = Visibility.Hidden;
                ResetCommandData();
                commandDataGroupBox.Visibility = Visibility.Hidden;
                commandOtherInfoGroupBox.Visibility = Visibility.Hidden;
            }

            LockCommandData();
            RefreshClientCarsList();
        }

        private void CommandsTabBtn_Click(object sender, RoutedEventArgs e)
        {
            serviceTabs.SelectedItem = CommandsTab;
            ShowCommandsTabData(clientComenziPreviewListBox.SelectedIndex);
        }

        private void NewCommandBtn_Click(object sender, RoutedEventArgs e)
        {
            commandDataGroupBox.Visibility = Visibility.Visible;

            saveCommandBtn.Visibility = Visibility.Visible;
            cancelCommandBtn.Visibility = Visibility.Visible;
            editCommandBtn.Visibility = Visibility.Hidden;

            ResetCommandData();
            UnlockCommandData();
            itsNewCommand = true;
        }

        private void SaveCommandBtn_Click(object sender, RoutedEventArgs e)
        {
            var commandUtils = new CarServiceClient();
            if (itsNewCommand)
            {
                stareComandaCBox.SelectedIndex = 0;

                commandUtils.AddCommand(new Comanda
                {
                    StareComanda = stareComandaCBox.Text,
                    DataProgramare = programareDateBox.DisplayDate,
                    DataFinalizare = finalizareDateBox.DisplayDate,
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
                    DataProgramare = programareDateBox.DisplayDate,
                    DataFinalizare = finalizareDateBox.DisplayDate,
                    Descriere = descriereComandaTxtBox.Text,
                    ValoarePiese = Convert.ToDecimal(valoarePieseTxtBox.Text),
                    KmBord = Convert.ToInt32(kmBordTxtBox.Text)
                });
            }
            saveCommandBtn.Visibility = Visibility.Hidden;
            cancelCommandBtn.Visibility = Visibility.Hidden;
            editCommandBtn.Visibility = Visibility.Visible;
            RefreshCarCommandsList();
        }

        private void CancelCommandBtn_Click(object sender, RoutedEventArgs e)
        {
            if (itsNewCommand)
            {
                comenziListBox.UnselectAll();
                
                commandDataGroupBox.Visibility = Visibility.Hidden;
                commandOtherInfoGroupBox.Visibility = Visibility.Hidden;
                itsNewCommand = false;
            }
            else
            {
                //ResetCommandData();//
                editCommandBtn.Visibility = Visibility.Visible;
                saveCommandBtn.Visibility = Visibility.Hidden;
                cancelCommandBtn.Visibility = Visibility.Hidden;
            }
            LockCommandData();
        }

        private void ComenziListBox_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowCommandsTabData(comenziListBox.SelectedIndex);
            RefreshCommandOperations();
        }

        private void DeleteCommandBtn_Click(object sender, RoutedEventArgs e)
        {
            if (comenziListBox.SelectedIndex > -1)
            {
                ResetCommandData();
                var commandUtils = new CarServiceClient();
                commandUtils.RemoveCommand(CarCommandsList.ElementAt(comenziListBox.SelectedIndex));
                CarCommandsList.Remove(CarCommandsList.ElementAt(comenziListBox.SelectedIndex));
                RefreshCarCommandsList();
            }
        }

        //OPERATIONS TAB
        private void InitializeCommandOperations()
        {
            operationDetailsGroupBox.Visibility = Visibility.Hidden;
            //LockCommandOperationsData();//

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

                operatiuniListBox.ItemsSource = commandOperations;
                operatiuniTxtBox.Text = String.Join("| ", commandOperations.ToArray());
            }
        }

        private void LockCommandOperationsData()
        {
            //clientOperatiuneTxtBox.ReadOnly = true;
            //comandaOperatiuneTxtBox.ReadOnly = true;
            //denumireOperatieTxtBox.ReadOnly = true;
            //

            timpExecutieOperatieTxtBox.IsReadOnly = true;
            mecaniciOperatiuniTxtBox.IsReadOnly = true;
        }

        private void ShowCommandOperationsTabData()
        {
            if (operatiuniListBox.SelectedIndex > -1)
            {
                operationDetailsGroupBox.Visibility = Visibility.Visible;

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

                poza.Visibility = Visibility.Visible;
                poza.Source = BitmapFrame.Create(new MemoryStream(currentOperatiune.Imagini.ElementAt(0).Foto), BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
            else
            {
                ResetOperationData();
            }
        }

        private void ResetOperationData()
        {
            operationDetailsGroupBox.Visibility = Visibility.Hidden;
            mecaniciOperatiuniTxtBox.Clear();
            materialeOperatiuneTxtBox.Clear();
            timpExecutieOperatieTxtBox.Clear();
            comandaOperatiuneTxtBox.Clear();
            numeOperatiuneTxtBox.Clear();
            poza.Visibility = Visibility.Hidden;
        }

        private void OperatiuniListBox_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowCommandOperationsTabData();
        }


        private void OperationCommandDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (operatiuniListBox.SelectedIndex > -1)
            {
                var detaliuUtils = new CarServiceClient();
                detaliuUtils.RemoveCommandDetail(CommandOperations.ElementAt(operatiuniListBox.SelectedIndex));
                CommandOperations.Remove(CommandOperations.ElementAt(operatiuniListBox.SelectedIndex));
                RefreshCommandOperations();
            }
        }

        private void NewOperationBtn_Click(object sender, RoutedEventArgs e)
        {
            //serviceTabs.TabPages.Add(EditCommandTab);/
            serviceTabs.SelectedIndex = 4;
            itsNewOperation = true;
        }
        
        private void SaveOperationBtn_Click(object sender, RoutedEventArgs e)
        {
            var detaliuUtils = new CarServiceClient();
            if (itsNewOperation)
            {
                var mecanici = new List<Mecanic>();
                var materiale = new List<Material>();

                var parsedMecanici = comandaEditMecaniciTxtBox.Text.Split(',').ToList();
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
                        DataAprovizionare = aprovizionareDateBox.DisplayDate
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
                    Mecanici = mecanici.ToArray(),
                    Materiale = materiale.ToArray(),
                    Imagini = new List<Imagine>
                    {
                        new Imagine
                        {
                            Titlu = titluImagineTxtBox.Text,
                            Descriere = descriereImagineTxtBox.Text,
                            Data = dataImagineDateBox.DisplayDate,
                            Foto = File.ReadAllBytes(imagineTxtBox.Text)
                        }
                    }.ToArray()
                });
            }
            RefreshCommandOperations();
            serviceTabs.SelectedItem = OperationsTab;
        }

        private void operationsTabBtn_Click(object sender, RoutedEventArgs e)
        {
            serviceTabs.SelectedItem = OperationsTab;
        }

        private void CancelOperationBtn_Click(object sender, RoutedEventArgs e)
        {
            serviceTabs.SelectedItem = OperationsTab;
            RefreshCommandOperations();
            ResetOperationData();
        }

        //MISC TAB
        private void ServiceTabs_Selected(object sender, SelectionChangedEventArgs e)
        {
            if (serviceTabs.SelectedItem == AutoTab)
            {
                CarTabBtn_Click(sender, e);
            }

            if (serviceTabs.SelectedItem == CommandsTab)
            {
                CommandsTabBtn_Click(sender, e);
            }

            if (serviceTabs.SelectedItem != EditCommandTab)
            {
                EditCommandTab.Visibility = Visibility.Hidden;
            }
        }
    }
}
