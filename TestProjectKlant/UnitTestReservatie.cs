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
        private SortedDictionary<string, string> Vol = new SortedDictionary<string, string>() { {"8-9","14" }, { "9-10", "14" }, { "12-13", "18" }, { "13-14", "18" } };
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


        [Fact]
        public void ZetDatum_valid()
        {
            DateTime overmorgen = DateTime.Today.AddDays(2);
            DateTime vandaag = DateTime.Today.AddHours(1);
            DateTime eindeWeek = DateTime.Today.AddDays(7);
            Reservatie r = new Reservatie(48, 15, "Jef.desmet@gmail.com", "Jef", "De Smet", overmorgen);

            Assert.Equal(overmorgen, r.Datum);
            r.ZetDatum(vandaag);
            Assert.Equal(vandaag, r.Datum);
            
            Assert.Equal(vandaag, r.Datum);
            r.ZetDatum(eindeWeek);
            Assert.Equal(eindeWeek, r.Datum);

        }
        [Fact]
        public void ZetDatum_invalid()
        {
            DateTime overmorgen = DateTime.Today.AddDays(2);
            DateTime gisteren = DateTime.Today.AddDays(-1);
            DateTime vlgdWeek = DateTime.Today.AddDays(8);

            Reservatie r = new Reservatie(48, 15, "Jef.desmet@gmail.com", "Jef", "De Smet", overmorgen);

            Assert.Equal(overmorgen, r.Datum);
            Assert.Throws<ReservatieExeption>(() => r.ZetDatum(gisteren));
            Assert.Equal(overmorgen, r.Datum);

            Assert.Equal(overmorgen, r.Datum);
            Assert.Throws<ReservatieExeption>(() => r.ZetDatum(vlgdWeek));
            Assert.Equal(overmorgen, r.Datum);

        }

        [Theory]
        [InlineData("20-21","5","20-21", "5")]
        [InlineData("20-21       ","5","20-21", "5")]
        [InlineData("     20-21     ","5","20-21", "5")]
        [InlineData("22-23","5","22-23","5")]
        [InlineData("20-21", "5        ", "20-21", "5")]
        [InlineData("20-21", "    5    ", "20-21", "5")]
        [InlineData("20-21", "2", "20-21", "2")]
        public void ZetSlotEnToestel_valid(string slotIn, string toestelIn,string slotUit, string toestelUit)
        {
            SortedDictionary<string, string> ST = new SortedDictionary<string, string>();
            Reservatie r = new Reservatie(48, 15, "Jef.desmet@gmail.com", "Jef", "De Smet", DateTime.Today.AddDays(3));

            //Assert.Equal(ST, r.gereserveerdeSlotenEnToestellen);
            //r.ZetSlotEnToestel(slotIn, toestelIn);
            //ST.Add(slotUit, toestelUit);
            //Assert.Equal(ST, r.gereserveerdeSlotenEnToestellen);
        }
        [Theory]
        [InlineData(" ","4")]
        [InlineData(null, "4")]
        [InlineData("   \n", "4")]
        [InlineData("     \r  ", "4")]
        [InlineData("","4")]
        [InlineData("21-22"," ")]
        [InlineData("21-22", null)]
        [InlineData("21-22", "   \n")]
        [InlineData("21-22", "     \r  ")]
        [InlineData("21-22", "")]
        // zitten er al eens in
        [InlineData("8-9", "14")]
        [InlineData("9-10", "14")]
        [InlineData("12-13", "18")]

        public void ZetSlotEnToestel_invalid(string slot,string toestel)
        {
            Reservatie r = new Reservatie(48, 15, "Jef.desmet@gmail.com", "Jef", "De Smet", DateTime.Today.AddDays(3));
            //r.gereserveerdeSlotenEnToestellen = PlaatsOver;
            //Assert.Equal(PlaatsOver, r.gereserveerdeSlotenEnToestellen);
            //Assert.Throws<ReservatieExeption>(() => r.ZetSlotEnToestel(slot,toestel));
            //Assert.Equal(PlaatsOver, r.gereserveerdeSlotenEnToestellen);


        }
        [Fact]
        public void ZetSlotEnToestel_invalid_TeVeelSloten()
        {
            string slot = "19-20";
            string toestel = "3";
            Reservatie r = new Reservatie(48, 15, "Jef.desmet@gmail.com", "Jef", "De Smet", DateTime.Today.AddDays(3));
            //r.gereserveerdeSlotenEnToestellen = Vol;

            //Assert.Equal(Vol, r.gereserveerdeSlotenEnToestellen);
            //Assert.Throws<ReservatieExeption>(() => r.ZetSlotEnToestel(slot, toestel));
            //Assert.Equal(Vol, r.gereserveerdeSlotenEnToestellen);

        }
    }
}
