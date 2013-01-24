using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyHelper4Web
{
    /// <summary>
    /// Base64 编码帮助类
    /// </summary>
    public class MyBase64Helper
    {
        #region Public Fields

        protected static MyBase64Helper SB64 = new MyBase64Helper();

        protected string MCodeTable = @"ABCDEFGHIJKLMNOPQRSTUVWXYZbacdefghijklmnopqrstu_wxyz0123456789*-";

        protected string MPad = "v";

        protected Dictionary<int, char> MT1 = new Dictionary<int, char>();

        protected Dictionary<char, int> MT2 = new Dictionary<char, int>();

        #endregion

        #region Preperty

        /// <summary>
        /// 设置并验证密码表合法性
        /// </summary>
        public string CodeTable
        {
            get { return MCodeTable; }
            set
            {
                if (value == null)
                {
                    throw new Exception("密码表不能为null");
                }
                if (value.Length < 64)
                {
                    throw new Exception("密码表长度必须至少为64");
                }
                ValidateRepeat(value);
                ValidateEqualPad(value, MPad);
                MCodeTable = value;
                InitDict();
            }
        }

        /// <summary>
        /// 设置并验证补码合法性
        /// </summary>
        public string Pad
        {
            get { return MPad; }
            set
            {
                if (value == null)
                {
                    throw new Exception("密码表的补码不能为null");
                }
                if (value.Length != 1)
                {
                    throw new Exception("密码表的补码长度必须为1");
                }
                ValidateEqualPad(MCodeTable, value);
                MPad = value;
                InitDict();
            }
        }

        #endregion

        #region Construcor

        /// <summary>
        /// 初始化字典
        /// </summary>
        public MyBase64Helper()
        {
            InitDict();
        }

        #endregion

        #region Public Method

        /// <summary>
        /// 获取具有标准的Base64密码表的加密类
        /// </summary>
        /// <returns>Base64密码表的加密类</returns>
        public static MyBase64Helper GetStandardBase64()
        {
            var b64 = new MyBase64Helper { Pad = "=", CodeTable = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/" };
            return b64;
        }

        /// <summary>
        /// 使用默认的密码表（双向哈西字典）加密字符串
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string input)
        {
            return SB64.Encode(input);
        }

        /// <summary>
        /// 使用默认的密码表（双向哈西字典）解密字符串
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt(string input)
        {
            return SB64.Decode(input);
        }

        #endregion

        #region Protect Method

        protected string Encode(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }
            var sb = new StringBuilder();
            byte[] tmp = Encoding.UTF8.GetBytes(source);
            int remain = tmp.Length % 3;
            int patch = 3 - remain;
            if (remain != 0)
            {
                Array.Resize(ref tmp, tmp.Length + patch);
            }
            var cnt = (int)Math.Ceiling(tmp.Length * 1.0 / 3);
            for (int i = 0; i < cnt; i++)
            {
                sb.Append(EncodeUnit(tmp[i * 3], tmp[i * 3 + 1], tmp[i * 3 + 2]));
            }
            if (remain != 0)
            {
                sb.Remove(sb.Length - patch, patch);
                for (int i = 0; i < patch; i++)
                {
                    sb.Append(MPad);
                }
            }
            return sb.ToString();
        }

        protected string EncodeUnit(params byte[] unit)
        {
            var obj = new int[4];
            obj[0] = (unit[0] & 0xfc) >> 2;
            obj[1] = ((unit[0] & 0x03) << 4) + ((unit[1] & 0xf0) >> 4);
            obj[2] = ((unit[1] & 0x0f) << 2) + ((unit[2] & 0xc0) >> 6);
            obj[3] = unit[2] & 0x3f;
            var sb = new StringBuilder();
            foreach (int t in obj)
            {
                sb.Append(GetEC(t));
            }
            return sb.ToString();
        }

        protected char GetEC(int code)
        {
            return MT1[code]; //m_codeTable[code];
        }

        protected string Decode(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }
            var list = new List<byte>();
            char[] tmp = source.ToCharArray();
            int remain = tmp.Length % 4;
            if (remain != 0)
            {
                Array.Resize(ref tmp, tmp.Length - remain);
            }
            int patch = source.IndexOf(MPad);
            if (patch != -1)
            {
                patch = source.Length - patch;
            }
            int cnt = tmp.Length / 4;
            for (int i = 0; i < cnt; i++)
            {
                DecodeUnit(list, tmp[i * 4], tmp[i * 4 + 1], tmp[i * 4 + 2], tmp[i * 4 + 3]);
            }
            for (int i = 0; i < patch; i++)
            {
                list.RemoveAt(list.Count - 1);
            }
            return Encoding.UTF8.GetString(list.ToArray());
        }

        protected void DecodeUnit(List<byte> byteArr, params char[] chArray)
        {
            var res = new int[3];
            var unit = new byte[chArray.Length];
            for (int i = 0; i < chArray.Length; i++)
            {
                unit[i] = FindChar(chArray[i]);
            }
            res[0] = (unit[0] << 2) + ((unit[1] & 0x30) >> 4);
            res[1] = ((unit[1] & 0xf) << 4) + ((unit[2] & 0x3c) >> 2);
            res[2] = ((unit[2] & 0x3) << 6) + unit[3];
            byteArr.AddRange(res.Select(t => (byte)t));
        }

        protected byte FindChar(char ch)
        {
            int pos = MT2[ch]; //m_codeTable.IndexOf(ch);
            return (byte)pos;
        }

        protected void InitDict()
        {
            MT1.Clear();
            MT2.Clear();
            MT2.Add(MPad[0], -1);
            for (int i = 0; i < MCodeTable.Length; i++)
            {
                MT1.Add(i, MCodeTable[i]);
                MT2.Add(MCodeTable[i], i);
            }
        }

        protected void ValidateRepeat(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input.LastIndexOf(input[i]) > i)
                {
                    throw new Exception("密码表中含有重复字符：" + input[i]);
                }
            }
        }

        protected void ValidateEqualPad(string input, string pad)
        {
            if (input.IndexOf(pad) > -1)
            {
                throw new Exception("密码表中包含了补码字符：" + pad);
            }
        }

        protected void Test()
        {
            //m_codeTable = @"STUVWXYZbacdefghivklABCDEFGHIJKLMNOPQRmnopqrstu!wxyz0123456789+/";
            //m_pad = "j";

            InitDict();

            const string test = "abc ABC 你好！◎＃￥％……!@#$%^";
            string encode = Encode("false");
            string decode = Decode(encode);
            Console.WriteLine(encode);
            Console.WriteLine(test == decode);
        }

        #endregion

    }
}
