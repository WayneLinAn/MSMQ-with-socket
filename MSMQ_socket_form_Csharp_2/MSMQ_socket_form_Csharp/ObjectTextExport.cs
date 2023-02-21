/**********************************************************************
 <Program name - title>
   ObjectTextExport   - Text Export
 * 
+---------------------------------------------------------------------+
I ** (C) COPYRIGHT 2012 BY DRAGONSTEEL CO. LTD ALL RIGHTS RESERVED ** I
I **         Y6P2                                                  ** I
+---------------------------------------------------------------------+

 <Change history>
    Designed by :  YP CHEN  ,   2012-02-02
    Coded by    :  YP CHEN  ,   2012-02-02
    Modified by :  _.________ (__.__) _______________________________

 <Function>
    Reading and wirting in text file
***********************************************************************/


using System;
using System.IO;

namespace MSMQ_socket_form_Csharp
{
    public class ObjectTextExport
    {
        public byte[] readFile(string strFileName)
        {
            byte[] buffer = null;

            try
            {
                FileStream fs = new FileStream(strFileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                int len = System.Convert.ToInt32(fs.Length);

                buffer = br.ReadBytes(len);
                fs.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n{0} FILE read ERROR  \n{1}\n", strFileName, e.Message);
            }
            return buffer;
        }

        public bool writeFile(string strFileName, byte[] strData)
        {

            try
            {
                FileStream fs = new FileStream(strFileName, FileMode.Append, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fs);
                int len = System.Convert.ToInt32(strData.Length);
                bw.Write(strData);
                bw.Close();
                fs.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n{0} FILE write ERROR  \n{1}\n", strFileName, e.Message);
                return false;
            }
            return true;
        }
    }
}

