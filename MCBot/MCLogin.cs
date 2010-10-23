using System.Net;
using System.Text;

namespace Org.Jonyleeson.MCBot
{
    static class MCLogin
    {
        public static MCAccountInfo GetInfo(string username, string password)
        {
            MCAccountInfo accountInfo = new MCAccountInfo();
            StringBuilder sb = new StringBuilder();
            byte[] buf = new byte[256]; // should be largely sufficient for the string it's going to send us

            HttpWebRequest request;
            HttpWebResponse response;

            try
            {
                request = (HttpWebRequest)WebRequest.Create(string.Format("http://www.minecraft.net/game/getversion.jsp?user={0}&password={1}&version=255", username, password));
                response = (HttpWebResponse)request.GetResponse();
            }
            catch
            {
                return null;
            }

            string tempString;
            int count = 0;

            do
            {
                count = response.GetResponseStream().Read(buf, 0, buf.Length);

                if (count != 0)
                {
                    tempString = Encoding.ASCII.GetString(buf, 0, count);
                    sb.Append(tempString);
                }
            }
            while (count > 0);

            string[] split = sb.ToString().Split(":".ToCharArray());

            if (split.Length == 5)
            {
                accountInfo.GameVersion = split[0];
                accountInfo.DownloadTicket = split[1];
                accountInfo.Name = split[2];
                accountInfo.Session = split[3];

                return accountInfo;
            }

            return null;
        }

        public static string JoinServer(string username, string session, string serverhash)
        {
            StringBuilder sb = new StringBuilder();
            byte[] buf = new byte[256]; // should be largely sufficient for the string it's going to send us

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://www.minecraft.net/game/joinserver.jsp?user={0}&sessionId={1}&serverId={2}", username, session, serverhash));
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string tempString;
            int count = 0;

            do
            {
                count = response.GetResponseStream().Read(buf, 0, buf.Length);

                if (count != 0)
                {
                    tempString = Encoding.ASCII.GetString(buf, 0, count);
                    sb.Append(tempString);
                }
            }
            while (count > 0);

            return sb.ToString();
        }
    }
}
