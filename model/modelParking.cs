namespace ParkingSystem.Model
{
  public class Lot
{
    public bool Kosong { get; set; }
    public Kendaraan? KendaraanMobil { get; set; }
    public Kendaraan? KendaraanMotor { get; set; }

    public JenisKendaraan JenisKendaraan { get; set; }
    public DateTime? WaktuCheckIn { get; set; }
    public decimal BiayaParkirPerMenit { get; set; } 

    public Lot(decimal biayaParkirPerMenit)
    {
        Kosong = true;
        KendaraanMobil = null;
        KendaraanMotor = null;
        BiayaParkirPerMenit = biayaParkirPerMenit; 
    }

// Biaya parkir per 1 menit saja agar mudah untuk mengetestnya
 public decimal HitungBiayaParkir()
{
    TimeSpan durasiParkir = DateTime.Now - WaktuCheckIn!.Value;
    int menitParkir = (int)Math.Ceiling(durasiParkir.TotalMinutes);

    return BiayaParkirPerMenit * menitParkir;
}

}


    public enum JenisKendaraan
    {
        Mobil,
        Motor
    }

    public class Kendaraan
    {
        public string? NomorPolisi { get; set; }
        public JenisKendaraan Jenis { get; set; }
        public DateTime WaktuCheckIn { get; set; }
        public DateTime WaktuCheckOut { get; set; }

        public string? Warna { get; set; }
        
        
    }
}
