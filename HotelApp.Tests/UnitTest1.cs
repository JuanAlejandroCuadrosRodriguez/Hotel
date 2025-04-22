namespace HotelApp.Tests;
using NUnit.Framework;
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

}
