using System;
using System.IO;
using System.Text;

namespace QeydlerDefteri
{
    class Program
    {
        const string FayilAdi = "qeydler.txt";

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("===== QEYDLƏR DƏFTƏRİ =====");
                Console.WriteLine("1) Yeni qeyd");
                Console.WriteLine("2) Bütün qeydlər");
                Console.WriteLine("3) Qeyd axtar");
                Console.WriteLine("4) Bütün qeydləri sil");
                Console.WriteLine("5) Çıxış");
                Console.Write("Seçim: ");
                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1": YeniQeyd(); break;
                    case "2": ButunQeydler(); break;
                    case "3": QeydAxtar(); break;
                    case "4": ButunQeydleriSil(); break;
                    case "5":
                        Console.WriteLine("Proqram bağlandı.");
                        return;
                    default:
                        Console.WriteLine("Yanlış seçim! 1-5 arası daxil edin.");
                        break;
                }
            }
        }

        static void YeniQeyd()
        {
            Console.Write("Qeydi daxil edin: ");
            string metn = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(metn))
            {
                Console.WriteLine("Boş qeyd yazıla bilməz.");
                return;
            }

            try
            {

                using (StreamWriter sw = new StreamWriter(FayilAdi, true, Encoding.UTF8))
                {
                    string tarix = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    sw.WriteLine("[" + tarix + "] " + metn);
                }
                Console.WriteLine("Qeyd əlavə olundu.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Xəta baş verdi: " + ex.Message);
            }
        }

        static void ButunQeydler()
        {
            if (!File.Exists(FayilAdi))
            {
                Console.WriteLine("Hələ heç bir qeyd yoxdur.");
                return;
            }

            try
            {
                int say = 0;
                using (StreamReader sr = new StreamReader(FayilAdi, Encoding.UTF8))
                {
                    string setir;
                    while ((setir = sr.ReadLine()) != null)
                    {
                        say++;
                        Console.WriteLine(say + ". " + setir);
                    }
                }

                if (say == 0)
                    Console.WriteLine("Hələ heç bir qeyd yoxdur.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fayl oxunarkən xəta: " + ex.Message);
            }
        }

        static void QeydAxtar()
        {
            if (!File.Exists(FayilAdi))
            {
                Console.WriteLine("Hələ heç bir qeyd yoxdur.");
                return;
            }

            Console.Write("Açar söz: ");
            string acar = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(acar))
            {
                Console.WriteLine("Açar söz boş ola bilməz.");
                return;
            }

            try
            {
                int tapildi = 0;
                using (StreamReader sr = new StreamReader(FayilAdi, Encoding.UTF8))
                {
                    string setir;
                    while ((setir = sr.ReadLine()) != null)
                    {

                        if (setir.IndexOf(acar, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            Console.WriteLine(setir);
                            tapildi++;
                        }
                    }
                }

                if (tapildi == 0)
                    Console.WriteLine("Heç nə tapılmadı.");
                else
                    Console.WriteLine("Tapılan qeyd sayı: " + tapildi);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Axtarış zamanı xəta: " + ex.Message);
            }
        }

        static void ButunQeydleriSil()
        {
            if (!File.Exists(FayilAdi))
            {
                Console.WriteLine("Silinəcək qeyd yoxdur (fayl mövcud deyil).");
                return;
            }

            Console.Write("Bütün qeydlər silinsin? (y/n): ");
            string cavab = Console.ReadLine();

            if (cavab != null && cavab.Trim().ToLower() == "y")
            {
                try
                {
                    File.Delete(FayilAdi);
                    Console.WriteLine("Bütün qeydlər silindi.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Silinmə zamanı xəta: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Əməliyyat ləğv edildi.");
            }
        }
    }
}