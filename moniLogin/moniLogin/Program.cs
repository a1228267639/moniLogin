using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace moniLogin
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpHeader header = new HttpHeader();
            header.accept = "*/*";
            //  header.accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-silverlight, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-silverlight-2-b1, */*";
            header.contentType = "application/x-www-form-urlencoded";
            header.method = "POST";
            header.userAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36";
            header.maxTry = 300;

            string html = HTMLHelper.GetHtml("http://guide.renren.com/guide", HTMLHelper.GetCooKie("http://www.renren.com/PLogin.do",
                "email=13353765387&password=yzj19970519&icode=&origURL=http%3A%2F%2Fwww.renren.com%2Fhome&domain=renren.com&key_id=1&_rtk=90484476", header), header);

            Console.WriteLine(html);


            //  Console.ReadLine();
            //FileStream fs = new FileStream(@"C:\Users\Administrator.USER-20181202MU\Desktop\Images\biaoti1.jpg", FileMode.Open, FileAccess.Read);
            //byte[] bArr = new byte[fs.Length];
            //fs.Read(bArr, 0, bArr.Length);
            //fs.Close();


            //HttpHeader header = new HttpHeader();
            //header.accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-silverlight, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-silverlight-2-b1, */*";
            //header.contentType = "application/x-www-form-urlencoded";
            //header.method = "POST";
            //header.userAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
            //header.maxTry = 300;


            //CookieCollection mycookie = HTMLHelper.GetCookieCollection("http://www.renren.com/PLogin.do",
            //                   "email=13353765387&password=yzj19970519&icode=&origURL=http%3A%2F%2Fwww.renren.com%2Fhome&domain=renren.com&key_id=1&_rtk=90484476", header);


            //foreach (Cookie cookie in mycookie) //将cookie设置为浏览的cookie  
            //{

            //    InternetSetCookie(

            //         "http://" + cookie.Domain.ToString(),

            //         cookie.Name.ToString(),

            //         cookie.Value.ToString() + ";expires=Sun,22-Feb-2099 00:00:00 GMT");

            //}
            //System.Diagnostics.Process.Start("http://guide.renren.com/guide");


        }
        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetCookie(string lpszUrlName, string lbszCookieName, string lpszCookieData);

        //private void btnOcr_Click()
    //{
    //    string url = "http://一串狂拽酷炫掉渣天的网址.com";
    //    OpenFileDialog openfile = new OpenFileDialog();
    //    openfile.Multiselect = false;
    //    openfile.RestoreDirectory = true;
    //    openfile.Filter = "JPG|*.jpg|BMP|*.bmp|PNG|*.png|GIF|*.gif|TIF|*.tif|TIFF|*.tiff|All Pic|*.jpg;*.bmp;*.png;*.gif;*.tif;*.tiff";
    //    openfile.FilterIndex = 0;

    //    if ((bool)openfile.ShowDialog())
    //    {
    //        string pic = ImgToBase64String(openfile.FileName);
    //        string result = request(url, pic);
    //    }
    //}


    public static string request(string url, string param)
    {
        string strURL = url;
        System.Net.HttpWebRequest request;
        request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
        request.Method = "POST";

        string boundary = "---------------------------91811647521344";
        request.ContentType = " multipart/form-data; boundary=" + boundary;
        string filepath = "C:\\Users\\Administrator\\Desktop\\text.png";

        // 构造发送数据
        StringBuilder sb = new StringBuilder();
        // 文件域的数据
        sb.Append("-----------------------------91811647521344");
        sb.Append("\r\n");
        sb.Append("Content-Disposition: form-data; name=\"upfile\"; filename=\"text.png\"");
        sb.Append("\r\n");

        sb.Append("Content-Type: ");
        sb.Append("image/png");
        sb.Append("\r\n\r\n");

        string postHeader = sb.ToString();
        byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);

        //构造尾部数据
        StringBuilder wsb = new StringBuilder();
            wsb.Append("\r\n" + "------WebKitFormBoundaryEuT8ESppM3CD86nU" + "\r\n");
            wsb.Append("Content-Disposition: form-data; name=\"requestToken\"" + "\r\n");
            wsb.Append("1962590969" + "\r\n");
        wsb.Append("\r\n" + "------WebKitFormBoundaryEuT8ESppM3CD86nU" + "\r\n");
        wsb.Append("Content-Disposition: form-data; name=\"_rtk\"" + "\r\n");
        wsb.Append("\r\n" + "40bbc153" + "\r\n");
        wsb.Append("------WebKitFormBoundaryEuT8ESppM3CD86nU--");
        byte[] boundaryBytes = Encoding.UTF8.GetBytes(wsb.ToString());

        FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
        long length = postHeaderBytes.Length + fileStream.Length + boundaryBytes.Length;
        request.ContentLength = length;

        Stream requestStream = request.GetRequestStream();

        // 输入头部数据
        requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

        // 输入文件流数据
        byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)fileStream.Length))];
        int bytesRead = 0;
        int index = 0;
        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
        {
            requestStream.Write(buffer, 0, bytesRead);
            index++;
        }

        // 输入尾部数据
        requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
        WebResponse responce = request.GetResponse();

        System.IO.Stream s;
        s = responce.GetResponseStream();
        string StrDate = "";
        string strValue = "";
        StreamReader Reader = new StreamReader(s, Encoding.Default);
        while ((StrDate = Reader.ReadLine()) != null)
        {
            strValue += StrDate + "\r\n";
        }
        return strValue;
    }

    }
    

}
