using Client.Controller.API;
using Client.Model;
using Client.Model_Veiw.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.Model_Veiw
{
    class MainWindowShell: Client.Model_Veiw.Base.Base
    {
        #region Привязка данных

        #region Строка ввода username
        private string _userName = string.Empty;

        public string userName
        {
            get => _userName;
            set => Set(ref _userName, value);
        }

        private bool _userStringAble = true;
        public bool userStringAble
        {
            get => _userStringAble;
            set => Set(ref _userStringAble, value);
        }
        #endregion

        private string _btnText = "Entry";
        public string btnText
        {
            get => _btnText;
            set => Set(ref _btnText, value);
        }


        private ObservableCollection<Message> _messges;
        public ObservableCollection<Message> messges
        {
            get => _messges;
            set => Set(ref _messges, value);
        }

        #region Строка чата
        private bool _chatStringAble = false;
        public bool chatStringAble
        {
            get => _chatStringAble;
            set => Set(ref _chatStringAble, value);
        }

        private string _textMsg = string.Empty;
        public string textMsg
        {
            get => _textMsg;
            set => Set(ref _textMsg, value);
        }
        #endregion

        private DateTime _firstTime = DateTime.Today;
        public DateTime firstTime
        {
            get => _firstTime;
            set => Set(ref _firstTime, value);
        }

        private DateTime _secondTime = DateTime.Today;
        public DateTime secondTime
        {
            get => _secondTime;
            set => Set(ref _secondTime, value);
        }


        private bool _checkState = false;
        public bool checkState
        {
            get => _checkState;
            set
            {
                if (value == true)
                {
                    if ( secondTime < firstTime)
                    {
                        MessageBox.Show("Dates are incorrect");
                        Set(ref _checkState, false);
                        dateAddAble = true;
                    }
                    else
                    {
                        ObservableCollection<Message> sortMsg = new ObservableCollection<Message>();
                        foreach (var x in messges)
                        {
                            if (x.MessageTime > firstTime && x.MessageTime < secondTime) sortMsg.Add(x);
                        }

                        messges = sortMsg;

                        Set(ref _checkState, value);
                        dateAddAble = !value;
                    }
                }
                else
                {
                    getAllMessage();
                    Set(ref _checkState, value);
                    dateAddAble = !value;
                }
                
            }
        }

        private bool _dateAddAble = false;
        public bool dateAddAble
        {
            get => _dateAddAble;
            set => Set(ref _dateAddAble, value);
        }
        #endregion

        #region Команды
        public ICommand ChatEntry { get; }
        private async void OnChatEntryExecutedAsync(object p)
        {
            if (userStringAble == true)
            {
                if (userName == string.Empty) MessageBox.Show("Enter username");
                else
                {
                    await getAllMessage();
                    userStringAble = false;
                    btnText = "Exit";
                    chatStringAble = true;
                    dateAddAble = true;
                }
            }
            else
            {
                chatStringAble = false;
                userStringAble = true;
                btnText = "Entry";
            }
        }
        private bool CanChatEntryExecuted(object p) => true;

        public ICommand SendMsg { get; }
        private async void OnSendMsgExecutedAsync(object p)
        {
            var strWithoutSpaces = textMsg.Replace(" ", "");
            var FreeStr = strWithoutSpaces.Replace("\n", "");

            if (FreeStr != string.Empty)
            {
                APIController client = new APIController();
                await client.APISendMsgAsync(userName, textMsg.Trim());
                textMsg = string.Empty;
                await getAllMessage();
            }
            else
            {
                MessageBox.Show("Unable to send empty string");
                textMsg = string.Empty;
            }
        }
        private bool CanSendMsgExecuted(object p) => true;

        public ICommand RefreshChat { get; }

        private async void OnRefreshChatExecutedAsync(object p)
        {
            await getAllMessage();
        }

        private bool CanRefreshChatExecuted(object p) => true;

        #endregion

        public MainWindowShell()
        {
            ChatEntry = new ActionCommand(OnChatEntryExecutedAsync, CanChatEntryExecuted);

            SendMsg = new ActionCommand(OnSendMsgExecutedAsync, CanSendMsgExecuted);

            RefreshChat = new ActionCommand(OnRefreshChatExecutedAsync, CanRefreshChatExecuted);
        }

        private async Task getAllMessage()
        {
            APIController client = new APIController();
            try
            {
                messges = await client.APIGetMsg();
            }
            catch
            {
                MessageBox.Show("API not available");
                Application.Current.Shutdown();
            }
        }
    }
}
