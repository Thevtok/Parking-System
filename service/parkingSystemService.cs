using System.Text.RegularExpressions;
using ParkingSystem.Model;


namespace ParkingSystem.Service
{
    public interface IParkingService
    {
        void CheckIn(string nomorPolisi, JenisKendaraan jenisKendaraan,string warna);
        void CheckOut(string nomorPolisi);
          int GetJumlahKendaraanGanjil();
    int GetJumlahKendaraanGenap();
        (int mobil, int motor) GetJumlahLotTerisi();
        (int mobil, int motor) GetJumlahLotTersedia();
        Dictionary<JenisKendaraan, int> GetJumlahKendaraanPerJenis();
        int GetJumlahKendaraanByWarna(string warna);
         void GetAll();
    }

    public class ParkingService : IParkingService
    {
        private List<Lot> lotParkir;

   public ParkingService(int jumlahLot, decimal biayaParkirMobil, decimal biayaParkirMotor)
{
    lotParkir = new List<Lot>();

    for (int i = 0; i < jumlahLot; i++)
    {
        if (i % 2 == 0)
        {
            lotParkir.Add(new Lot(biayaParkirMobil) { JenisKendaraan = JenisKendaraan.Mobil });
        }
        else
        {
            lotParkir.Add(new Lot(biayaParkirMotor) { JenisKendaraan = JenisKendaraan.Motor });
        }
    }
}



       public void GetAll()
{
    foreach (var lot in lotParkir)
    {
        if (!lot.Kosong)
        {
            if (lot.KendaraanMobil != null)
            {
                Console.WriteLine($"Jenis: Mobil, No Polisi: {lot.KendaraanMobil.NomorPolisi}, Warna: {lot.KendaraanMobil.Warna}");
            }

            if (lot.KendaraanMotor != null)
            {
                Console.WriteLine($"Jenis: Motor, No Polisi: {lot.KendaraanMotor.NomorPolisi}, Warna: {lot.KendaraanMotor.Warna}");
            }
        }
    }
}

public void CheckIn(string nomorPolisi, JenisKendaraan jenisKendaraan,string warna)
{
    nomorPolisi = nomorPolisi.ToUpper();
    warna = warna.ToUpper();
       if (!Regex.IsMatch(nomorPolisi, "[A-Z]"))
    {
        Console.WriteLine("Tidak dapat melakukan check-in. Nomor polisi tidak valid");
        return;
    }

    
    if (!Regex.IsMatch(nomorPolisi, "[0-9]"))
    {
        Console.WriteLine("Tidak dapat melakukan check-in. Nomor polisi tidak valid");
        return;
    }


    bool lotDitemukan = false;
       foreach (var lot in lotParkir)
    {
        if (!lot.Kosong)
        {
            if (lot.KendaraanMobil?.NomorPolisi == nomorPolisi || lot.KendaraanMotor?.NomorPolisi == nomorPolisi)
            {
                Console.WriteLine("Tidak dapat melakukan check-in. Nomor polisi sudah digunakan.");
                return;
            }
        }
    }

    foreach (var lot in lotParkir)
    {
        if (lot.Kosong)
        {
            if (jenisKendaraan == JenisKendaraan.Mobil)
            {
                if (lot.KendaraanMotor == null)
                {
                    lot.KendaraanMobil = new Kendaraan { NomorPolisi = nomorPolisi, Jenis = jenisKendaraan, Warna=warna, WaktuCheckIn = DateTime.Now };
                    lot.Kosong = false;
                     Console.WriteLine(jenisKendaraan+" berhasil check-in dengan no polisi "+nomorPolisi);
                      lot.WaktuCheckIn = DateTime.Now;
                    return;
                }
            }
            else if (jenisKendaraan == JenisKendaraan.Motor)
            {
                if (lot.KendaraanMobil == null)
                {
                    lot.KendaraanMotor = new Kendaraan { NomorPolisi = nomorPolisi, Jenis = jenisKendaraan,Warna=warna, WaktuCheckIn = DateTime.Now };
                    lot.Kosong = false;
                     Console.WriteLine(jenisKendaraan+" berhasil check-in dengan no polisi "+nomorPolisi);
                      lot.WaktuCheckIn = DateTime.Now;
                    return;
                }
            }
        }
        else if (lot.KendaraanMobil != null && lot.KendaraanMotor != null)
        {
            continue;
        }
        else if (lot.KendaraanMobil != null && jenisKendaraan == JenisKendaraan.Motor)
        {
            lot.KendaraanMotor = new Kendaraan { NomorPolisi = nomorPolisi, Jenis = jenisKendaraan,Warna=warna, WaktuCheckIn = DateTime.Now };
            lot.Kosong = false;
            Console.WriteLine(jenisKendaraan+" berhasil check-in dengan no polisi "+nomorPolisi);
             lot.WaktuCheckIn = DateTime.Now;
            return;
        }
        else if (lot.KendaraanMotor != null && jenisKendaraan == JenisKendaraan.Mobil)
        {
            lot.KendaraanMobil = new Kendaraan { NomorPolisi = nomorPolisi, Jenis = jenisKendaraan,Warna=warna, WaktuCheckIn = DateTime.Now };
            lot.Kosong = false;
             Console.WriteLine(jenisKendaraan+" berhasil check-in dengan no polisi "+nomorPolisi);
              lot.WaktuCheckIn = DateTime.Now;
            return;
        }
        lotDitemukan = true;
    }

 if (lotDitemukan)
{
    if (jenisKendaraan == JenisKendaraan.Mobil)
    {
        Console.WriteLine("Tidak dapat melakukan check-in. Lot untuk mobil sudah penuh.");
    }
    else if (jenisKendaraan == JenisKendaraan.Motor)
    {
        Console.WriteLine("Tidak dapat melakukan check-in. Lot untuk motor sudah penuh.");
    }
}
    else
    {
        Console.WriteLine("Maaf, tidak ada lot parkir yang tersedia.");
    }
}
public void CheckOut(string nomorPolisi)
{
    nomorPolisi = nomorPolisi.ToUpper();

    foreach (var lot in lotParkir)
    {
        if (!lot.Kosong)
        {
            if (lot.KendaraanMobil?.NomorPolisi == nomorPolisi)
            {
                decimal biayaParkir = lot.HitungBiayaParkir();
                lot.KendaraanMobil = null; 
                lot.Kosong = lot.KendaraanMotor == null; 
                Console.WriteLine("Kendaraan mobil dengan nomor polisi " + nomorPolisi + " telah check-out.");
                Console.WriteLine("Biaya parkir: " + biayaParkir);
                return;
            }
            else if (lot.KendaraanMotor?.NomorPolisi == nomorPolisi)
            {
                decimal biayaParkir = lot.HitungBiayaParkir();
                lot.KendaraanMotor = null; 
                lot.Kosong = lot.KendaraanMobil == null;  
                Console.WriteLine("Kendaraan motor dengan nomor polisi " + nomorPolisi + " telah check-out.");
                Console.WriteLine("Biaya parkir: " + biayaParkir);
                return;
            }
        }
    }

    Console.WriteLine("Kendaraan dengan nomor polisi tersebut tidak ditemukan.");
}




public (int mobil, int motor) GetJumlahLotTerisi()
{
    int mobilTerisi = 0;
    int motorTerisi = 0;
    foreach (var lot in lotParkir)
    {
        if (lot.KendaraanMobil != null)
        {
            mobilTerisi++;
        }
        if (lot.KendaraanMotor != null)
        {
            motorTerisi++;
        }
    }
    return (mobilTerisi, motorTerisi);
}


public (int mobil, int motor) GetJumlahLotTersedia()
{
    int jumlahTersediaMobil = 0;
    int jumlahTersediaMotor = 0;
    
    foreach (var lot in lotParkir)
    {
        if (lot.KendaraanMobil == null)
        {
            jumlahTersediaMobil++;
        }
        
        if (lot.KendaraanMotor == null)
        {
            jumlahTersediaMotor++;
        }
    }
    
    return (jumlahTersediaMobil, jumlahTersediaMotor);
}




public int GetJumlahKendaraanGanjil()
{
    int jumlahGanjil = 0;
    foreach (var lot in lotParkir)
    {
        if (!lot.Kosong)
        {
            if (lot.KendaraanMobil != null)
            {
                int lastDigit = int.Parse(lot.KendaraanMobil.NomorPolisi!.Substring(lot.KendaraanMobil.NomorPolisi.Length - 1));
                if (lastDigit % 2 != 0)
                {
                    jumlahGanjil++;
                }
            }
            
            if (lot.KendaraanMotor != null)
            {
                int lastDigit = int.Parse(lot.KendaraanMotor.NomorPolisi!.Substring(lot.KendaraanMotor.NomorPolisi.Length - 1));
                if (lastDigit % 2 != 0)
                {
                    jumlahGanjil++;
                }
            }
        }
    }
    return jumlahGanjil;
}

public int GetJumlahKendaraanGenap()
{
    int jumlahGenap = 0;
    foreach (var lot in lotParkir)
    {
        if (!lot.Kosong)
        {
            if (lot.KendaraanMobil != null)
            {
                int lastDigit = int.Parse(lot.KendaraanMobil.NomorPolisi!.Substring(lot.KendaraanMobil.NomorPolisi.Length - 1));
                if (lastDigit % 2 == 0)
                {
                    jumlahGenap++;
                }
            }
            
            if (lot.KendaraanMotor != null)
            {
                int lastDigit = int.Parse(lot.KendaraanMotor.NomorPolisi!.Substring(lot.KendaraanMotor.NomorPolisi.Length - 1));
                if (lastDigit % 2 == 0)
                {
                    jumlahGenap++;
                }
            }
        }
    }
    return jumlahGenap;
}
public Dictionary<JenisKendaraan, int> GetJumlahKendaraanPerJenis()
{
    Dictionary<JenisKendaraan, int> jumlahPerJenis = new Dictionary<JenisKendaraan, int>();

    foreach (var lot in lotParkir)
    {
        if (!lot.Kosong)
        {
            if (lot.KendaraanMobil != null)
            {
                JenisKendaraan jenis = lot.KendaraanMobil.Jenis;
                if (jumlahPerJenis.ContainsKey(jenis))
                {
                    jumlahPerJenis[jenis]++;
                }
                else
                {
                    jumlahPerJenis[jenis] = 1;
                }
            }

            if (lot.KendaraanMotor != null)
            {
                JenisKendaraan jenis = lot.KendaraanMotor.Jenis;
                if (jumlahPerJenis.ContainsKey(jenis))
                {
                    jumlahPerJenis[jenis]++;
                }
                else
                {
                    jumlahPerJenis[jenis] = 1;
                }
            }
        }
    }

    return jumlahPerJenis;
}
public int GetJumlahKendaraanByWarna(string warna)
{
    warna = warna.ToUpper();
    int jumlahKendaraan = 0;
    foreach (var lot in lotParkir)
    {
        if (!lot.Kosong && lot.KendaraanMobil != null && lot.KendaraanMobil.Warna == warna)
        {
            jumlahKendaraan++;
        }
        if (!lot.Kosong && lot.KendaraanMotor != null && lot.KendaraanMotor.Warna == warna)
        {
            jumlahKendaraan++;
        }
    }
    return jumlahKendaraan;
}




    }}