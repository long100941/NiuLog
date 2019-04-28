using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Niu
{
    /// <summary>
    /// <para>版本号：2.4</para>
    /// <para>增加处理意外情况的方式(临时)</para>
    /// <para>版本号：2.3</para>
    /// <para>修复清理硬盘方法BUG。</para>
    /// <para>版本号：2.2</para>
    /// <para>添加了日志聚合功能，超过2次的日志不再写文件，只记录次数次数。添加了文件前缀功能可以区分谁创建的日志。</para>
    /// <para>版本号：2.1</para>
    /// <para>细节修改，提示更人性化。</para>
    /// <para>版本号：2.0</para>
    /// <para>全新改版，从本来依附与其他应用程序改为独立dll。并增加了加密功能。</para>
    /// <para>版本号：1.10</para>
    /// <para>添加了硬盘剩余空间配置信息，可以根据个人爱好或者实际需求修改。</para>
    /// <para>版本号：1.9</para>
    /// <para>更改了生成日志文件名称，看起来更顺眼。</para>
    /// <para>版本号：1.8</para>
    /// <para>添加了配置信息，可以根据个人爱好或者实际需求修改。</para>
    /// <para>版本号：1.7</para>
    /// <para>鉴于WPS和OFFICE对csv格式支持的稀烂，这个版本彻底抛弃这个格式，并针对txt记事本做浏览优化。</para>
    /// <para>版本号：1.6</para>
    /// <para>时间精确到毫秒</para>
    /// <para>版本号：1.5</para>
    /// <para>解决文件乱码</para>
    /// <para>优化代码,把逗号替换成中文逗号</para>
    /// <para>版本号：1.4</para>
    /// <para>解决多线程写入文件时冲突</para>
    /// <para>增加硬盘空间不足时自动删除旧日志</para>
    /// <para>增加缓存机制，优化性能</para>
    /// <para>版本号：1.3</para>
    /// <para>精简代码</para>
    /// <para>版本号：1.2</para>
    /// <para>增加分类选项，多了一个重载方法</para>
    /// <para>版本号：1.1</para>
    /// <para>解决BUG</para>
    /// <para>版本号：1.0</para>
    /// </summary>
    public static class NiuLog
    { 
        //配置日志路径（默认）
        private static string lujingq = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "niulog\\";
        //单个文件最大大小（单位M）
        private static int danwenjianq = 20;
        //是否每启动一次就生成一个日志文件
        private static bool meiciqidongq = true;
        //硬盘剩余空间最小大小（单位M）
        private static int yingpankongjianq = 2000;
        //是否启用加密
        private static bool qiyongjiamiq = false;
        //加密类型
        private static jiamileixing jiamifangshiq = jiamileixing.RSA;
        //是否启用日志聚合
        private static bool qiyongjuheq = false;
        //文件前缀
        private static string wenjianqianzuiq = "";

        /// <summary>
        /// 选择加密类型
        /// </summary>
        public enum jiamileixing { AES, RSA };
        private static string rizhiluijng = "";
        private static string huanchun = "";
        private static string RSApublickey = @"<RSAKeyValue><Modulus>52GQU56PtvChynAEvOSRV8tzprgW4dE3fCRFVSpcaxcypY2+i4jEPDXM+YahYMXLUbsat7Q3la0WWyb7BDB5Qjxvsfg5a2QhYzU46Is0dXMktI3oJV05w7dOYh/tv6QIlB+Mmy0/oKJqG7aDECPikQJlXXN+Z1vsAjqC5s2058s=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        private static string AESkey = "jinchigongsiNIU!";

        private static string chongfurizhi = "";
        private static int chongfurizhijishu = 0;
        #region 属性
        /// <summary>
        /// 配置路径
        /// </summary>
        public static string lujing
        {
            get
            {
                return lujingq;
            }
            set
            {
                lujingq = value;
            }
        }
        /// <summary>
        /// 单个文件最大大小（单位M）
        /// </summary>
        public static int danwenjian
        {
            get
            {
                return danwenjianq;
            }
            set
            {
                danwenjianq = value;
            }
        }
        /// <summary>
        /// 是否每启动一次就生成一个日志文件
        /// </summary>
        public static bool meiciqidong
        {
            get
            {
                return meiciqidongq;
            }
            set
            {
                meiciqidongq = value;
            }
        }
        /// <summary>
        /// 硬盘剩余空间最小大小（单位M）
        /// </summary>
        public static int yingpankongjian
        {
            get
            {
                return yingpankongjianq;
            }
            set
            {
                yingpankongjianq = value;
            }
        }
        /// <summary>
        /// 是否启用加密
        /// </summary>
        public static bool qiyongjiami
        {
            get
            {
                return qiyongjiamiq;
            }
            set
            {
                qiyongjiamiq = value;
            }
        }
        /// <summary>
        /// 加密类型
        /// </summary>
        public static jiamileixing jiamifangshi
        {
            get
            {
                return jiamifangshiq;
            }
            set
            {
                jiamifangshiq = value;
            }
        }

        /// <summary>
        /// 是否启用日志聚合
        /// </summary>
        public static bool qiyongjuhe
        {
            get
            {
                return qiyongjuheq;
            }
            set
            {
                qiyongjuheq = value;
            }
        }
        /// <summary>
        /// 文件前缀
        /// </summary>
        public static string wenjianqianzui
        {
            get
            {
                return wenjianqianzuiq;
            }
            set
            {
                wenjianqianzuiq = value;
            }
        }
        #endregion

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="neirong">写入的内容</param>
        /// <returns></returns>
        public static void rizhi(string neirong)
        {
            rizhi("", neirong);
        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="fenlei">分类</param>
        /// <param name="neirong">写入的内容</param>
        /// <returns></returns>
        public static void rizhi(string fenlei, string neirong)
        {
            try
            {
                DateTime shijian = DateTime.Now;
                string xieruneirong = "";
                if (qiyongjuheq)
                {
                    if (chongfurizhi != "[" + fenlei + "]," + neirong)
                    {
                        if (chongfurizhijishu > 0)
                        {
                            xieruneirong += "[" + shijian.ToString("yyyy-MM-dd H:mm:ss") + "." + sanweishu(shijian) + "]:";
                            xieruneirong += chongfurizhi.TrimStart('[', ']', ',') + "[" + chongfurizhijishu + "]" + "\r\n";
                        }
                        chongfurizhi = "[" + fenlei + "]," + neirong;
                        chongfurizhijishu = 0;
                    }
                    else
                    {
                        chongfurizhijishu++;
                        return;
                    }
                }
                xieruneirong += "[" + shijian.ToString("yyyy-MM-dd H:mm:ss") + "." + sanweishu(shijian) + "]:";
                if (!string.IsNullOrEmpty(fenlei))
                {
                    xieruneirong += "[" + fenlei + "],";
                }
                xieruneirong += neirong + "\r\n";
                if (meiciqidongq)
                {
                    if (string.IsNullOrEmpty(rizhiluijng))
                    {
                        rizhiluijng = lujingq + wenjianqianzuiq + DateTime.Now.ToString("yyyy-MM-dd H.mm.ss") + ".txt";
                    }
                }
                else
                {
                    rizhiluijng = lujingq + wenjianqianzuiq + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                }
                if (!File.Exists(rizhiluijng))
                {
                    qingliyingpan(lujingq);
                    Directory.CreateDirectory(lujingq);
                    File.AppendAllText(rizhiluijng, "", System.Text.Encoding.UTF8);
                }
                FileInfo fi = new FileInfo(rizhiluijng);
                if (fi.Length / 1024 / 1024 > danwenjianq)
                {
                    qingliyingpan(lujingq);
                    if (meiciqidongq)
                    {
                        rizhiluijng = lujingq + wenjianqianzuiq + DateTime.Now.ToString("yyyy-MM-dd H.mm.ss") + ".txt";
                    }
                    else
                    {
                        File.Move(rizhiluijng, lujingq + wenjianqianzuiq + DateTime.Now.ToString("yyyy-MM-dd H.mm.ss") + ".txt");
                    }
                }

                string jiamineirong = "";
                if (qiyongjiamiq)
                {
                    if (jiamifangshiq == jiamileixing.RSA)
                    {
                        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                        rsa.FromXmlString(RSApublickey);
                        byte[] cipherbytes;
                        while (xieruneirong.Length > 30)
                        {
                            string quan100 = xieruneirong.Remove(30);
                            xieruneirong = xieruneirong.Remove(0, 30);

                            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(quan100), false);
                            jiamineirong += Convert.ToBase64String(cipherbytes) + "\r\n";
                        }
                        cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(xieruneirong), false);
                        jiamineirong += Convert.ToBase64String(cipherbytes) + "\r\n";
                    }
                    else
                    {
                        if (jiamifangshiq == jiamileixing.AES)
                        {
                            jiamineirong = AESEncode(xieruneirong, AESkey) + "\r\n";
                        }
                    }
                }
                else
                {
                    jiamineirong = xieruneirong;
                }

                try
                {
                    File.AppendAllText(rizhiluijng, huanchun + jiamineirong, System.Text.Encoding.UTF8);
                    huanchun = "";
                }
                catch (Exception e)
                {
                    string cuowurizhi = "日志错误:" + e.Message.ToString() + "\r\n";
                    if (qiyongjiamiq)
                    {
                        if (jiamifangshiq == jiamileixing.RSA)
                        {
                            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                            rsa.FromXmlString(RSApublickey);

                            byte[] cipherbytes;
                            while (cuowurizhi.Length > 30)
                            {
                                string quan100 = cuowurizhi.Remove(30);
                                cuowurizhi = cuowurizhi.Remove(0, 30);

                                cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(quan100), false);
                                jiamineirong += Convert.ToBase64String(cipherbytes) + "\r\n";
                            }
                            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(cuowurizhi), false);
                            jiamineirong += Convert.ToBase64String(cipherbytes) + "\r\n";
                            huanchun += jiamineirong;
                        }
                        else
                        {
                            if (jiamifangshiq == jiamileixing.AES)
                            {
                                jiamineirong += AESEncode(cuowurizhi, AESkey);
                                huanchun += jiamineirong;
                            }
                        }
                    }
                    else
                    {
                        huanchun += jiamineirong + "日志错误:" + e.Message.ToString() + "\r\n";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
           
        }
        /// <summary>
        /// 清理硬盘
        /// </summary>
        /// <param name="lujing">要清理的日志路径</param>
        private static void qingliyingpan(string lujing)
        {
            long yingpan = GetHardDiskFreeSpace(lujing.Remove(3));
            if (yingpan < yingpankongjianq)
            {
                string[] wenjianrizhi = Directory.GetFiles(lujing);
                if (wenjianrizhi.Length==0)
                {
                    return;
                }
                string shenchulujing = "";
                DateTime zuilaoshijian = DateTime.Now;
                foreach (string wenjian in wenjianrizhi)
                {
                    FileInfo fi = new FileInfo(wenjian);
                    if (zuilaoshijian > fi.CreationTime)
                    {
                        zuilaoshijian = fi.CreationTime;
                        shenchulujing = wenjian;
                    }
                }
                File.Delete(shenchulujing);

                if (yingpan < yingpankongjianq - danwenjianq)
                {
                    string[] wenjianrizhi100 = Directory.GetFiles(lujing);
                    string shenchulujing100 = "";
                    DateTime zuilaoshijian100 = DateTime.Now;
                    foreach (string wenjian in wenjianrizhi100)
                    {
                        FileInfo fi = new FileInfo(wenjian);
                        if (zuilaoshijian100 > fi.CreationTime)
                        {
                            zuilaoshijian100 = fi.CreationTime;
                            shenchulujing100 = wenjian;
                        }
                    }
                    File.Delete(shenchulujing100);
                }
            }
        }
        ///    
        /// 获取指定驱动器的剩余空间总大小(单位为B)  
        ///    
        ///  只需输入代表驱动器的字母即可   
        ///     
        private static long GetHardDiskFreeSpace(string str_HardDiskName)
        {
            long freeSpace = new long();
            //str_HardDiskName = str_HardDiskName + ":\\";
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    freeSpace = drive.TotalFreeSpace / 1024 / 1024;
                }
            }
            return freeSpace;
        }
        /// <summary>
        ///  AES 加密
        /// </summary>
        /// <param name="content">原文</param>
        /// <param name="key">密钥,注意密码长度必须是16位</param>
        /// <returns></returns>
        private static string AESEncode(string content, string key)
        {
            if (string.IsNullOrEmpty(content)) return null;
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(content);
            Byte[] keyByte = Encoding.UTF8.GetBytes(key);

            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            {
                Key = keyByte,
                IV = keyByte,//CBC模式时使用
                Mode = System.Security.Cryptography.CipherMode.CBC,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            };

            System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        /// 三位数毫秒
        /// </summary>
        /// <param name="shijian">时间</param>
        private static string sanweishu(DateTime shijian)
        {
            int haomiao = shijian.Millisecond;
            if (haomiao<10)
            {
                return "00" + haomiao;
            }
            else if (haomiao<100)
            {
                return "0" + haomiao;
            }
            else
            {
                return "" + haomiao;
            }
        }
    }
}
