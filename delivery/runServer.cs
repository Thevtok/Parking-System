using ParkingSystem.Service;
using ParkingSystem.Model;

namespace ParkingSystem
{
    class RunServer
    {
        private IParkingService parkingService;

        public RunServer()
        {
            parkingService = new ParkingService(5,20000,5000);
        }

        public void Start()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("Selamat datang di Sistem Parkir");
                Console.WriteLine("==============================");
                Console.WriteLine("1. Check-In");
                 Console.WriteLine("2. Check-Out");
        Console.WriteLine("3. Lihat Jumlah Lot Terisi");
        Console.WriteLine("4. Lihat Jumlah Lot Tersedia");
        Console.WriteLine("5. Lihat Jumlah Kendaraan Ganjil");
        Console.WriteLine("6. Lihat Jumlah Kendaraan Genap");
        Console.WriteLine("7. Lihat Jumlah Kendaraan per Jenis");
        Console.WriteLine("8. Lihat Jumlah Kendaraan Berdasarkan Warna");
        
        Console.WriteLine("9. Tampilkan Semua Kendaraan");
Console.WriteLine("0. Keluar");
Console.WriteLine("==============================");
Console.Write("Masukkan pilihan (0-9): ");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                       
                        Console.WriteLine("Masukkan nomor polisi: ");
                       string? nomorPolisi = Console.ReadLine()?.ToUpper();
                        Console.WriteLine("Masukkan jenis kendaraan (1. Mobil, 2. Motor): ");
                        string? jenisKendaraanInput = Console.ReadLine();
                        JenisKendaraan jenisKendaraan;
                        if (jenisKendaraanInput == "1")
                            jenisKendaraan = JenisKendaraan.Mobil;
                        else if (jenisKendaraanInput == "2")
                            jenisKendaraan = JenisKendaraan.Motor;
                        else
                        {
                            Console.WriteLine("Jenis kendaraan tidak valid.");
                            continue;
                        }
                        Console.WriteLine("Masukkan warna: ");
                        string? warna = Console.ReadLine()?.ToUpper();
                        parkingService.CheckIn(nomorPolisi!, jenisKendaraan,warna!);
                        break;
                         case "2":
                Console.Write("Masukkan nomor polisi: ");
                string? nomorPolisiCheckOut = Console.ReadLine()?.ToUpper();
                parkingService.CheckOut(nomorPolisiCheckOut!);
                break;
                   
                  case "3":
    
    var jumlahLotTerisi = parkingService.GetJumlahLotTerisi();
    Console.WriteLine("Jumlah lot terisi untuk mobil: " + jumlahLotTerisi.mobil);
    Console.WriteLine("Jumlah lot terisi untuk motor: " + jumlahLotTerisi.motor);
    break;

                    case "4":
   
    var jumlahLotTersedia = parkingService.GetJumlahLotTersedia();
    Console.WriteLine("Jumlah lot tersedia untuk mobil: " + jumlahLotTersedia.mobil);
    Console.WriteLine("Jumlah lot tersedia untuk motor: " + jumlahLotTersedia.motor);
    break;
                         case "5":
                int jumlahKendaraanGanjil = parkingService.GetJumlahKendaraanGanjil();
                Console.WriteLine($"Jumlah Kendaraan Ganjil: {jumlahKendaraanGanjil}");
                break;
            case "6":
                int jumlahKendaraanGenap = parkingService.GetJumlahKendaraanGenap();
                Console.WriteLine($"Jumlah Kendaraan Genap: {jumlahKendaraanGenap}");
                break;
                 case "7":
                Dictionary<JenisKendaraan, int> jumlahKendaraanPerJenis = parkingService.GetJumlahKendaraanPerJenis();
                Console.WriteLine("Jumlah kendaraan per jenis:");
                foreach (var kvp in jumlahKendaraanPerJenis)
                {
                    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                }
                break;
                case "8":
   
    Console.WriteLine("Masukkan warna kendaraan: ");
    string? Warna = Console.ReadLine()?.ToUpper();
    int jumlahKendaraanWarna = parkingService.GetJumlahKendaraanByWarna(Warna!);
    Console.WriteLine($"Jumlah kendaraan dengan warna {Warna}: {jumlahKendaraanWarna}");
    break;
    case "9":
    Console.WriteLine("Daftar Kendaraan");
    Console.WriteLine("================");
    parkingService.GetAll();
    Console.WriteLine("================");
    break;

                
                    case "0":
                       
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}
