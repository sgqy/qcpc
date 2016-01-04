using System;
using System.IO;
using System.Text;
using System.Linq;

namespace qcpc
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.Write(
                    "use command like this:\n" +
                    "echo <scp>[-dcp[-fstr]] | qcpc <input> [output]\n" +
                    "example:\n" +
                    "echo 932 - 1200 | qcpc in.txt out.txt\n" +
                    "echo 932 - 936 - % | qcpc conv.txt"
                    );
                return;
            }

            //==============================================

            string srcFile = null;
            string dstFile = null;

            try { srcFile = args[0]; } catch { }
            try { dstFile = srcFile; } catch { }
            try { dstFile = args[1]; } catch { }

            if (srcFile == null) return;

            string[] CPPair = Console.ReadLine().Split('-');

            var srcCP = Encoding.GetEncoding(0);
            var dstCP = Encoding.GetEncoding(1200);
            var erfb = EncoderFallback.ReplacementFallback;
            var drfb = DecoderFallback.ReplacementFallback;

            try { srcCP = Encoding.GetEncoding(int.Parse(CPPair[0])); } catch { }
            try { erfb = new EncoderReplacementFallback(CPPair[2].Trim()); } catch { }
            try { dstCP = Encoding.GetEncoding(int.Parse(CPPair[1]), erfb, drfb); } catch { }
            
            FileStream fr = null;
            try { fr = new FileStream(srcFile, FileMode.Open); } catch { Console.WriteLine("import file error"); return; }
            var ibuf = new byte[fr.Length];
            fr.Read(ibuf, 0, ibuf.Length);
            fr.Dispose();
                        
            var obuf = Encoding.Convert(srcCP, dstCP, ibuf);
            var bom = new byte[] { };
            switch (dstCP.CodePage)
            {
                case 1200:
                    bom = new byte[] { (byte)'\xFF', (byte)'\xFE' };
                    break;
                case 1201:
                    bom = new byte[] { (byte)'\xFE', (byte)'\xFF' };
                    break;
                case 65001:
                    bom = new byte[] { (byte)'\xEF', (byte)'\xBB', (byte)'\xBF' };
                    break;
                default:
                    break;
            }
            obuf = bom.Concat(obuf).ToArray();

            FileStream fw = null;
            try { fw = new FileStream(dstFile, FileMode.OpenOrCreate); } catch { Console.WriteLine("export file error"); return; }
            fw.SetLength(0);
            fw.Write(obuf, 0, obuf.Length);
            fw.Dispose();
        }
    }
}
