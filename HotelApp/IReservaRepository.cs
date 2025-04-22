namespace HotelApp
{
    public interface IReservaRepository
    {
        void Guardar(Reserva reserva);
        IReadOnlyList<Reserva> ObtenerTodas();
    }
}