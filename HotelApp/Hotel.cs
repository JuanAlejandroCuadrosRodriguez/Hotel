namespace HotelApp;

public class Hotel
{
    private readonly List<Reserva> reservas = new();

    public void AgregarReserva(int Habitacion,DateTime Inicio, DateTime Fin)
    {
       if(!EstaDisponible(Habitacion, Inicio, Fin))
       {
           throw new Exception("La habitación no está disponible en esas fechas.");
       }
       if (Inicio >= Fin)
       {
           throw new ArgumentException("El rango de fechas es inválido.");
       }
       reservas.Add(new Reserva(Habitacion,Inicio,Fin));
    }

    public bool EstaDisponible(int Habitacion, DateTime Inicio, DateTime Fin)
    {
        return !reservas.Any(r => r.Habitacion == Habitacion && r.SeSuperpone(Inicio, Fin));
    }

    public IReadOnlyList<Reserva> ObtenerReservas() => reservas.AsReadOnly();
}