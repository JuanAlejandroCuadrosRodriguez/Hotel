namespace HotelApp;

public class Reserva
{
    public int Habitacion {get;}
    public DateTime FechaEntrada {get;}
    public DateTime FechaSalida {get;}

    public Reserva(int habitacion, DateTime fechaEntrada, DateTime fechaSalida)
    {
        Habitacion = habitacion;
        FechaEntrada = fechaEntrada;
        FechaSalida = fechaSalida;
    }

    public bool SeSuperpone(DateTime otrafechaEntrada, DateTime otrafechaSalida)
    {
        return !(otrafechaSalida <= FechaEntrada || otrafechaEntrada >= FechaSalida);
    }
}