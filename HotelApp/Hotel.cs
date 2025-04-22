namespace HotelApp;

public class Hotel
{
    private readonly List<Reserva> reservas = new();
    private readonly IReservaRepository? repositorio;

    public Hotel() {}

    public Hotel(IReservaRepository repositorio)
    {
        this.repositorio = repositorio;
    }

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
       var reserva = new Reserva(Habitacion, Inicio, Fin);
       reservas.Add(reserva);
         repositorio?.Guardar(reserva);
    }

    public bool EstaDisponible(int Habitacion, DateTime Inicio, DateTime Fin)
    {
        return !reservas.Any(r => r.Habitacion == Habitacion && r.SeSuperpone(Inicio, Fin));
    }

    public IReadOnlyList<Reserva> ObtenerReservas() => reservas.AsReadOnly();
}