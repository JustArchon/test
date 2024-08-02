using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace BlankApp2.ViewModels
{

    public class TCPChatViewModel : BindableBase
    {
        #region 변수
        bool isAlive;                               // 연결 성공, 실패
        private string _serverIP, _dialogName, _sendMessage;      // 서버IP, 전송자 이름 저장
        Socket client;                              // 클라이언트 소켓 지정
        private string _serverPort = "5555";             // 서버 포트
        private readonly ObservableCollection<string> _message = new ObservableCollection<string>();

        public string ServerIP { get => _serverIP; set => _serverIP = value; }
        public string DialogName { get => _dialogName; set => _dialogName = value; }
        public string SendMessage { get => _sendMessage; set => _sendMessage = value; }
        public string ServerPort { get => _serverPort; set => _serverPort = value; }
        public ObservableCollection<string> Message => _message;
        #endregion

        public TCPChatViewModel()
        {
            LoadLocalIPAddress();
        }

        private void LoadLocalIPAddress()
        {
            IPHostEntry hostIP = Dns.GetHostEntry(Dns.GetHostName());         // Host IP 탐색 및 저장
            String ipAddr = string.Empty;
            for (int i = 0; i < hostIP.AddressList.Length; i++)
            {
                if (hostIP.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddr = hostIP.AddressList[i].ToString();
                }
            }

            _serverIP = ipAddr;
        }

        private DelegateCommand _cmdConnect;
        public DelegateCommand CmdConnect =>
            _cmdConnect ?? (_cmdConnect = new DelegateCommand(ExecuteCmdConnect));

        void ExecuteCmdConnect()
        {
            if (string.IsNullOrEmpty(this.DialogName))                   // 이름 무결성검사
            {
                MessageBox.Show("대화명을 입력하세요.");
                return;
            }

            if (string.IsNullOrEmpty(this.ServerIP))                     // IP 무결성 검사
            {
                MessageBox.Show("주소를 입력하세요.");
                return;
            }

            if (string.IsNullOrEmpty(this.ServerPort))                   // 포트 무결성 검사
            {
                MessageBox.Show("포트를 입력하세요.");
                return;
            }

            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);           // 새로운 소켓 생성
            client.Connect(IPAddress.Parse(ServerIP), int.Parse(ServerPort));                                          // 생성된 소켓에 연결 시도
            client.Send(Encoding.UTF8.GetBytes(DialogName));                                                // 서버에 이름 전달
            SocketAsyncEventArgs reciveAsyncEArgs = new SocketAsyncEventArgs();                             // 소켓 이벤트 핸들러 생성
            reciveAsyncEArgs.Completed += new EventHandler<SocketAsyncEventArgs>(RecivedAsync_Completed);   // 수신 이벤트를 이벤트헨들러(내부 델리게이터)를 통한 비동기 활성화
            reciveAsyncEArgs.SetBuffer(new byte[4], 0, 4);                                                  // 수신 버퍼내 버퍼사이즈 설정
            reciveAsyncEArgs.UserToken = client;                                                            // 이벤트 핸들러내 연결된사용자 정보를 현재 연결된 소켓 정보 삽입
            client.ReceiveAsync(reciveAsyncEArgs);                                                          // 소켓 내 수신대기 영역에 설정된 이벤트 핸들러 삽입
        }
        private DelegateCommand<string> _cmdDisConnect;
        public DelegateCommand<string> CmdDisConnect =>
            _cmdDisConnect ?? (_cmdDisConnect = new DelegateCommand<string>(ExecuteCmdDisConnect));

        void ExecuteCmdDisConnect(string parameter)
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        private DelegateCommand<string> _cmdSend;
        public DelegateCommand<string> CmdSend =>
            _cmdSend ?? (_cmdSend = new DelegateCommand<string>(ExecuteCmdSend));



        void ExecuteCmdSend(string parameter)
        {

        }

        #region TCP 메시지 수신
        private void RecivedAsync_Completed(object sender, SocketAsyncEventArgs e)          // 서버에서 메시지 수신시 사용되는 함수
        {
            try
            {
                if (client != null)
                {
                    if (client.Connected && e.BytesTransferred > 0)                             // 클라이언트 연결 및 버퍼 접근이 확인될시 실행
                    {
                        byte[] lengthByte = e.Buffer;                                           // 사전 버퍼길이 설정
                        int length = BitConverter.ToInt32(lengthByte, 0);                       // 버퍼 길이 변환
                        byte[] data = new byte[length];                                         // 수신한 버퍼 길이에 맞춰 버퍼 생성
                        client.Receive(data, length, SocketFlags.None);                         // 소켓으로부터 내용 수신
                        showMessage(DialogName, Encoding.UTF8.GetString(data));           // 수신한 메시지 표시
                        if (Encoding.UTF8.GetString(data).Contains("is Duplicated"))            // 수신된 내용이 이름 중복일시 실행
                        {
                            //SocketDisconnect();                                                 // 소켓 연결해제 함수 실행
                        }
                    }
                    if (isAlive)
                    {
                        client.ReceiveAsync(e);                                                     // 수신 후 다시 대기모드
                    }
                }
                // Socket client = (Socket)sender;

            }
            catch (Exception ex)                                                            // 예외사항 처리
            {
                MessageBox.Show(ex.ToString());
            }

        }
        #endregion

        #region TCP 메시지 전송
        private void sendMessage(string message)                                // 메시지 전송 함수
        {
            try
            {
                if (client.Connected)
                {
                    byte[] buffer = new UTF8Encoding().GetBytes(message);       // 메시지 변환
                    client.Send(BitConverter.GetBytes(buffer.Length));          // 받는 측에 메시지 길이 전송
                    client.Send(buffer, SocketFlags.None);                      // 메시지 전송
                }
            }
            catch (SocketException e)                                           // 소켓 연결 오류발생시
            {
                showMessage("전송에러: ", e.Message + "\r\n");                 // 오류메시지 표시
            }
        }
        #endregion

        #region 메시지 표시 및 특수 메시지 처리
        public void showMessage(string dialogName, string netmessage)              // 메시지 표시
        {
            //if (netmessage.Contains("Order") && netmessage.Contains($"Recieve:{dialogName}"))          // 메시지 내용 확인, "order", 및 본인에게 보낸메시지인지 확인 후 실행
            //{
            //    Order_Message_Progress(netmessage);                                                    // 작업지시 메시지 처리
            //}
            if (this.Message != null)
            {
                Message.Add($"{netmessage} \r\n");                                             // 채팅 칸에 메시지 추가
            }
        }
        #endregion
    }
}
