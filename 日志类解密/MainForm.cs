using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 日志类解密
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void RSAjiemi_Click(object sender, EventArgs e)
        {
            this.dakai.ShowDialog();
            string lujing = this.dakai.FileName;
            string[] rizhi = File.ReadAllLines(lujing, Encoding.UTF8);
            this.jindu.Maximum = rizhi.Length;
            this.zhuangtai.Text = "正在解密";
            this.RSAjiemi.Enabled = false;
            Thread th = new Thread(new ParameterizedThreadStart(RSAjiemixiancheng));
            th.Start(rizhi);
        }
        private void RSAjiemixiancheng(object txt)
        {
            string[] rizhi = (string[])txt;
            for (int i = 0; i < rizhi.Length; i++)
            {
                this.jiemixianshi.Text += RSADecrypt(rizhi[i]);
                if (this.jindu.Maximum >= i)
                {
                    this.jindu.Value = i;
                }
            }
            this.jindu.Value = this.jindu.Maximum;
            this.zhuangtai.Text = "解密完成";
            this.RSAjiemi.Enabled = true; ;
        }
        public static string RSADecrypt(string content)
        {
            string privatekey = @"<RSAKeyValue><Modulus>52GQU56PtvChynAEvOSRV8tzprgW4dE3fCRFVSpcaxcypY2+i4jEPDXM+YahYMXLUbsat7Q3la0WWyb7BDB5Qjxvsfg5a2QhYzU46Is0dXMktI3oJV05w7dOYh/tv6QIlB+Mmy0/oKJqG7aDECPikQJlXXN+Z1vsAjqC5s2058s=</Modulus><Exponent>AQAB</Exponent><P>/CoDTrchwKQMMva00HqOJ2gXUYvvaDPmAb6ic5jKfcO2xJyBRf8mAmSrUY6OMwR5I7xkkMQd4h0PmOpoBaDJBQ==</P><Q>6uad7HmCGSGZBJdszBCxb+MLYa8hcUPPgSd8xbMkeTBcrK+UwvGgFGM0MuEYK6ykY2UaWcQPI0+gMWYUYLyGjw==</Q><DP>b++mcDbTANTRpX3mXfVJTSzjzQs1RyhinbumGXJl0NYkN7rRl+TfVoihlB6X+QWvULG8YpbsPQdxeQv1CSqKkQ==</DP><DQ>5O7d+xpxMQ0NlOv/nOKoC+O7s/h7H2h3U+ioMgXmBjacG7EDyvqyiSwmM3AtnmMj07s1c0checig14QlWCJlAQ==</DQ><InverseQ>IpPcf6EgFd6u5ouWXj95nErLdmAWTbTEi26BsIOjKT2jquRz2ivV6duSKXXDNjQbH/vNXhsB/OZfc4vjHc4Fdw==</InverseQ><D>Fl9/tiYx3U2dR1bXjnBHgYbBsxA/3iv9wno7XgXPk+dVVV7EWvepiezl2gbjmdjmVQvThInfihCHhd7aDMinIMpge9zbeXfucQuTNNMk93k/q3PKop4u6HK21ik44lWqEttGj9k36rfvwwRQjjysEG+N23nA3Vk5zH3e0h0WAjk=</D></RSAKeyValue>";
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(privatekey);
            cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), false);

            return Encoding.UTF8.GetString(cipherbytes);
        }

        private void AESjiemi_Click(object sender, EventArgs e)
        {
            this.dakai.ShowDialog();
            string lujing = this.dakai.FileName;
            string[] rizhi = File.ReadAllLines(lujing, Encoding.UTF8);
            this.jindu.Maximum = rizhi.Length;
            this.zhuangtai.Text = "正在解密";
            this.AESjiemi.Enabled = false;
            Thread th = new Thread(new ParameterizedThreadStart(AESjiemixiancheng));
            th.Start(rizhi);
        }
        private void AESjiemixiancheng(object txt)
        {
            string[] rizhi = (string[])txt;
            for (int i = 0; i < rizhi.Length; i++)
            {
                this.jiemixianshi.Text += AESDecode(rizhi[i], "jinchigongsiNIU!");
                if (this.jindu.Maximum>=i)
                {
                    this.jindu.Value = i;
                }
            }
            this.jindu.Value = this.jindu.Maximum;
            this.zhuangtai.Text = "解密完成";
            this.AESjiemi.Enabled = true;
        }
        /// <summary>
        ///  AES 解密
        /// </summary>
        /// <param name="content">密文</param>
        /// <param name="key">密钥,注意密码长度必须是16位</param>
        /// <returns></returns>
        public static string AESDecode(string content, string key)
        {
            if (string.IsNullOrEmpty(content)) return null;
            Byte[] toEncryptArray = Convert.FromBase64String(content);
            Byte[] keyByte = Encoding.UTF8.GetBytes(key);

            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            {
                Key = keyByte,
                IV = keyByte,//CBC模式时使用
                Mode = System.Security.Cryptography.CipherMode.CBC,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            };

            System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }
    }
}
