using System;
using System.Collections.Generic;
using System.Text;
using Test_Razor.Models;
using Xunit;

namespace SiracTest
{
    public class PreferenciaTest
    {
        [Fact]
        public void TestupdtPreferencia()
        {
            Preferencia preferencia = new Preferencia();
            Publicacion pub = new Publicacion()
            {
                color = "Negro",
                especie = "Perro",
                genero = "Macho"
            };
            Preferencia result = preferencia.updtPreferencia(preferencia, pub);
            Assert.Equal(1, result.Negro);
            Assert.Equal(1, result.Perro);
            Assert.Equal(1, result.Macho);

            result = preferencia.updtPreferencia(new Preferencia(), new Publicacion());
            Assert.Equal(0, result.Negro);
            Assert.Equal(0, result.Perro);
            Assert.Equal(0, result.Macho);
        }
        [Fact]
        public void TestnormalizarPreferencia()
        {
            Preferencia preferencia = new Preferencia()
            {
                Negro = 3,
                Perro = 1,
                Gato = 2
            };
            int visitas = 6;
            Preferencia result = preferencia.normalizarPreferencia(preferencia, visitas);
            Assert.Equal((float)3 / 6,result.Negro);
            Assert.Equal((float)1 / 6,result.Perro);
            Assert.Equal((float)2 / 6, result.Gato);
        }
        [Fact]
        public void TestnormalizarPreferenciaNoValido()
        {
            Preferencia preferencia = new Preferencia()
            {
                Negro = 3,
                Perro = 1,
                Gato = 2
            };
            int visitas = 0;
            Preferencia result = preferencia.normalizarPreferencia(preferencia, visitas);
            Assert.Equal(3, result.Negro);
            Assert.Equal(1, result.Perro);
            Assert.Equal(2, result.Gato);

            visitas = -3;
            result = preferencia.normalizarPreferencia(preferencia, visitas);
            Assert.Equal(3, result.Negro);
            Assert.Equal(1, result.Perro);
            Assert.Equal(2, result.Gato);
        }

        [Fact]
        public void TestcalcularScore()
        {
            Preferencia preferencia = new Preferencia()
            {
                Negro = 3,
                Perro = 1,
                Gato = 2
            };
            Publicacion pub = new Publicacion
            {
                especie = "Gato",
                color = "Negro"
            };

            double result = preferencia.calcularScore(pub);
            Assert.Equal(5.0,result);

            result = preferencia.calcularScore(new Publicacion());
            Assert.Equal(0, result);
        }

        [Fact]
        public void TestgetTotalPuntos()
        {
            Preferencia preferencia = new Preferencia()
            {
                Negro = 3,
                Perro = 1,
                Gato = 2
            };
            
            double result = preferencia.getTotalPuntos();
            Assert.Equal(6, result);

            Preferencia vacio = new Preferencia();
            result = vacio.getTotalPuntos();
            Assert.Equal(0, result);
        }

    }
}
