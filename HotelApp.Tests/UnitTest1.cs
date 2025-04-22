namespace HotelApp.Tests;
using NUnit.Framework;
using Moq;
using System;
using HotelApp;

public class HotelTests
{
    [Test]
    public void ReservaExitosaCuandoEstaDisponible()
    {
        var hotel = new Hotel();
        hotel.AgregarReserva(101, new DateTime(2023, 10, 1), new DateTime(2023, 10, 5));
        Assert.That(hotel.ObtenerReservas().Count, Is.EqualTo(1));
    }

    [Test]
    public void LanzarExcepcionCuandoNoEstaDisponible()
    {
        var hotel = new Hotel();
        hotel.AgregarReserva(101, new DateTime(2023, 10, 1), new DateTime(2023, 10, 5));

        Assert.Throws<Exception>(() =>
        {
            hotel.AgregarReserva(101, new DateTime(2023, 10, 3), new DateTime(2023, 10, 7));
        });
    }

    [Test]
    public void PermitirReservaEnDiferentesHabitaciones()
    {
        var hotel = new Hotel();
        hotel.AgregarReserva(101, new DateTime(2023, 10, 1), new DateTime(2023, 10, 5));
        hotel.AgregarReserva(102, new DateTime(2023, 10, 3), new DateTime(2023, 10, 7));
        Assert.That(hotel.ObtenerReservas().Count, Is.EqualTo(2));
    }

    [Test]
    public void LanzarExcepcionCuandoReservasSeSobreponenEnLaMismaHabitacion()
    {
        var hotel = new Hotel();
        hotel.AgregarReserva(101, new DateTime(2023, 10, 1), new DateTime(2023, 10, 5));

        Assert.Throws<Exception>(() =>
        {
            hotel.AgregarReserva(101, new DateTime(2023, 10, 4), new DateTime(2023, 10, 6));
        });
    }

    [Test]
    public void PermitirReservasNoSobrepuestasEnLaMismaHabitacion()
    {
        var hotel = new Hotel();
        hotel.AgregarReserva(101, new DateTime(2023, 10, 1), new DateTime(2023, 10, 5));
        hotel.AgregarReserva(101, new DateTime(2023, 10, 6), new DateTime(2023, 10, 10));

        Assert.That(hotel.ObtenerReservas().Count, Is.EqualTo(2));
    }

    [Test]
    public void LanzarExcepcionCuandoRangoDeFechasEsInvalido()
    {
        var hotel = new Hotel();

        Assert.Throws<ArgumentException>(() =>
        {
            hotel.AgregarReserva(101, new DateTime(2023, 10, 5), new DateTime(2023, 10, 1));
        });
    }

    [Test]
    public void PruebaIntegral_ReservaSeGuardaYRecuperaCorrectamente()
    {
        // Crear un mock del repositorio de reservas
        var mockRepositorio = new Mock<IReservaRepository>();
        var reservasGuardadas = new List<Reserva>();

        // Configurar el mock para guardar y recuperar reservas
        mockRepositorio.Setup(repo => repo.Guardar(It.IsAny<Reserva>()))
                       .Callback<Reserva>(reserva => reservasGuardadas.Add(reserva));
        mockRepositorio.Setup(repo => repo.ObtenerTodas())
                       .Returns(reservasGuardadas);

        // Crear instancia de Hotel con el repositorio simulado
        var hotel = new Hotel(mockRepositorio.Object);

        // Agregar una reserva
        hotel.AgregarReserva(101, new DateTime(2023, 10, 1), new DateTime(2023, 10, 5));

        // Verificar que la reserva se guardó correctamente
        Assert.That(reservasGuardadas.Count, Is.EqualTo(1));
        Assert.That(reservasGuardadas[0].Habitacion, Is.EqualTo(101));

        // Verificar que la reserva se puede recuperar
        var reservasRecuperadas = hotel.ObtenerReservas();
        Assert.That(reservasRecuperadas.Count, Is.EqualTo(1));
        Assert.That(reservasRecuperadas[0].Habitacion, Is.EqualTo(101));
    }

    [Test]
    public void PruebaIntegral_MultiplesReservasSinConflictos()
    {
        // Crear un mock del repositorio de reservas
        var mockRepositorio = new Mock<IReservaRepository>();
        var reservasGuardadas = new List<Reserva>();

        // Configurar el mock para guardar y recuperar reservas
        mockRepositorio.Setup(repo => repo.Guardar(It.IsAny<Reserva>()))
                       .Callback<Reserva>(reserva => reservasGuardadas.Add(reserva));
        mockRepositorio.Setup(repo => repo.ObtenerTodas())
                       .Returns(reservasGuardadas);

        // Crear instancia de Hotel con el repositorio simulado
        var hotel = new Hotel(mockRepositorio.Object);

        // Agregar múltiples reservas
        hotel.AgregarReserva(101, new DateTime(2023, 10, 1), new DateTime(2023, 10, 5));
        hotel.AgregarReserva(102, new DateTime(2023, 10, 6), new DateTime(2023, 10, 10));
        hotel.AgregarReserva(103, new DateTime(2023, 10, 11), new DateTime(2023, 10, 15));

        // Verificar que todas las reservas se guardaron correctamente
        Assert.That(reservasGuardadas.Count, Is.EqualTo(3));
        Assert.That(reservasGuardadas[0].Habitacion, Is.EqualTo(101));
        Assert.That(reservasGuardadas[1].Habitacion, Is.EqualTo(102));
        Assert.That(reservasGuardadas[2].Habitacion, Is.EqualTo(103));

        // Verificar que las reservas se pueden recuperar sin conflictos
        var reservasRecuperadas = hotel.ObtenerReservas();
        Assert.That(reservasRecuperadas.Count, Is.EqualTo(3));
        Assert.That(reservasRecuperadas[0].Habitacion, Is.EqualTo(101));
        Assert.That(reservasRecuperadas[1].Habitacion, Is.EqualTo(102));
        Assert.That(reservasRecuperadas[2].Habitacion, Is.EqualTo(103));
    }
}
