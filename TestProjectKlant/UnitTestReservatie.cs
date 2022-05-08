using System;
using Xunit;
using BLKlant.Domein;
using BLKlant.Exceptions;
using BLKlant;
using System.Collections.Generic;

namespace TestProjectKlant
{
    public class UnitTestReservatie
    {
        private SortedDictionary<string, string> vol = new SortedDictionary<string, string>() { {"8-9","14" }, { "9-10", "14" }, { "12-13", "18" }, { "13-14", "18" } };
        private SortedDictionary<string, string> PlaatsOver = new SortedDictionary<string, string>() { { "8-9", "14" }, { "9-10", "14" }, { "12-13", "18" } };

        public UnitTestReservatie()
        {
        }
        [Fact]
        public void ZetReservatienummer_valid()
        {
            Reservatie r= new Reservatie(48,15,"Jef.desmet@gmail.com","Jef","De Smet",DateTime.Today.AddDays(3));

            Assert.Equal(48, r.ReservatieNummer);
            r.ZetReservatienummer(16);
            Assert.Equal(16, r.ReservatieNummer);

        }
        [Fact]
        public void ZetReservatieNummer_invalid()
        {
            Reservatie r = new Reservatie(48, 15, "Jef.desmet@gmail.com", "Jef", "De Smet", DateTime.Today.AddDays(3));

            Assert.Equal(48, r.ReservatieNummer);
            Assert.Throws<ReservatieExeption>(()=>r.ZetReservatienummer(0));
            Assert.Equal(48, r.ReservatieNummer);

        }
        [Fact]
        public void ZetKlantNummer_valid()
        {
            Reservatie r = new Reservatie(48, 15, "Jef.desmet@gmail.com", "Jef", "De Smet", DateTime.Today.AddDays(3));

            Assert.Equal(15, r.KlantNummer);
            r.ZetKlantnummer(16);
            Assert.Equal(16, r.KlantNummer);

        }
        [Fact]
        public void ZetKlantNummer_invalid()
        {
            Reservatie r = new Reservatie(48, 15, "Jef.desmet@gmail.com", "Jef", "De Smet", DateTime.Today.AddDays(3));

            Assert.Equal(15, r.KlantNummer);
            Assert.Throws<ReservatieExeption>(() => r.ZetKlantnummer(0));
            Assert.Equal(15, r.KlantNummer);

        }
        [Theory]
        [InlineData("Jef.desmet@gmail.com", "Jef.desmet@gmail.com")]
        [InlineData("Jef.desmet@gmail.com       ", "Jef.desmet@gmail.com")]
        [InlineData("     Jef.desmet@gmail.com     ","Jef.desmet@gmail.com")]
        [InlineData("JaneDoe123@hotmail.be", "JaneDoe123@hotmail.be")]
        public void ZetEmail_valid(string emailIn, string emailUit)
        {
            Reservatie r = new Reservatie(48, 15, "Jef.desmet@gmail.com", "Jef", "De Smet", DateTime.Today.AddDays(3));
            r.ZetEmail(emailIn);
            Assert.Equal(emailUit, r.Emailadres);
        }
        [Theory]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("   \n")]
        [InlineData("     \r  ")]
        [InlineData("")]
        [InlineData("Jef.desmetgmail.com")]
        public void ZetEmail_invalid(string email)
        {
            Reservatie r = new Reservatie(48, 15, "Jef.desmet@gmail.com", "Jef", "De Smet", DateTime.Today.AddDays(3));
            Assert.Throws<ReservatieExeption>(() => r.ZetEmail(email));
        }
        [Theory]
        [InlineData("Jane", "Jane")]
        [InlineData("Jane      ", "Jane")]
        [InlineData("   Jane       ", "Jane")]
        [InlineData("John", "John")]
        public void ZetVoornaam_valid(string naamIn, string naamUit)
        {
            Reservatie r = new Reservatie(48, 15, "Jef.desmet@gmail.com", "Jef", "De Smet", DateTime.Today.AddDays(3));
            r.ZetVoornaam(naamIn);
            Assert.Equal(naamUit, r.Voornaam);

        }
        [Theory]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("   \n")]
        [InlineData("     \r  ")]
        [InlineData("")]
        public void ZetVoornaam_invalid(string naam)
        {
            Reservatie r = new Reservatie(48, 15, "Jef.desmet@gmail.com", "Jef", "De Smet", DateTime.Today.AddDays(3));
            Assert.Throws<ReservatieExeption>(() => r.ZetVoornaam(naam));
        }
        [Theory]
        [InlineData("Doe", "Doe")]
        [InlineData("Doe      ", "Doe")]
        [InlineData("   Doe       ", "Doe")]
        [InlineData("Ford", "Ford")]
        public void ZetAchternaam_valid(string naamIn, string naamUit)
        {
            Reservatie r = new Reservatie(48, 15, "Jef.desmet@gmail.com", "Jef", "De Smet", DateTime.Today.AddDays(3));
            r.ZetAchternaam(naamIn);
            Assert.Equal(naamUit, r.Achternaam);

        }
        [Theory]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("   \n")]
        [InlineData("     \r  ")]
        [InlineData("")]
        public void ZetAchternaam_invalid(string naam)
        {
            Reservatie r = new Reservatie(48, 15, "Jef.desmet@gmail.com", "Jef", "De Smet", DateTime.Today.AddDays(3));
            Assert.Throws<ReservatieExeption>(() => r.ZetAchternaam(naam));
        }

        DateTime vandaag = DateTime.Today;
        [Theory]
        [InlineData(vandaag)]
        [InlineData(DateTime.Today.AddDays(7))]
        public void ZetDatum_valid(DateTime datum)
        {

        }
        [Theory]
        [InlineData(vandaag)]
        [InlineData(DateTime.Today.AddDays(7))]
        public void ZetDatum_valid(DateTime datum)
        {

        }

    }
}
